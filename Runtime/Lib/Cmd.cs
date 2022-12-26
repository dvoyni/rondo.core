using System;

namespace Rondo.Core.Lib {
    public class Cmd<T> : ICmd {
        private readonly Func<T> _invoke;
        private readonly Func<T, IMsg> _toMsg;

        public Cmd(Func<T> invoke, Func<T, IMsg> toMsg) {
            _invoke = invoke;
            _toMsg = toMsg;
        }

        public void Execute(IMessageReceiver receiver) {
            receiver.PostMessage(_toMsg(_invoke()));
        }

        public ICmd Next => null;
    }

    public class AsyncCmd<T> : ICmd {
        private readonly Action<Action<T>> _invoke;
        private readonly Func<T, IMsg> _toMsg;

        public AsyncCmd(Action<Action<T>> invoke, Func<T, IMsg> toMsg) {
            _invoke = invoke;
            _toMsg = toMsg;
        }

        public void Execute(IMessageReceiver receiver) {
            _invoke(t => receiver.PostMessage(_toMsg(t)));
        }

        public ICmd Next => null;
    }
}