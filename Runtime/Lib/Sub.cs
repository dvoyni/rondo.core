using System;

namespace Rondo.Core.Lib {
    public class Sub<T> : ISub {
        private readonly Func<T, IMsg> _toMsg;

        public Sub(Func<T, IMsg> toMsg) {
            _toMsg = toMsg;
        }

        public bool CanHandle<T1>() {
            return typeof(T1) == typeof(T);
        }

        public IMsg HandleEvent<T1>(T1 eventData) {
            return _toMsg((T)(object)eventData);
        }
    }
}