using System;
using System.Collections.Generic;
using Rondo.Core.Extras;
using Rondo.Core.Lib;
using Rondo.Core.Lib.Containers;
using Rondo.Core.Lib.Platform;
using Rondo.Core.Memory;

namespace Rondo.Core {
    public interface IMessenger {
        void PostMessage<TMsg>(TMsg msg) where TMsg : unmanaged;
        void PostMessage(Ptr msg);
        void TriggerSub<T>(T payload) where T : unmanaged;
    }

    public interface IPresenter<in TScene> : IDisposable
            where TScene : unmanaged {
        IMessenger Messenger { set; }
        void Present(TScene scene);
    }

    public delegate void PostMessage(L<Cf<Ptr, Ptr>> toMsg, Ptr result);

    public unsafe class Runtime<TModel, TMsg, TScene> : IMessenger
            where TModel : unmanaged
            where TMsg : unmanaged
            where TScene : unmanaged {
        public struct Config : IDisposable {
            public CLf<(TModel, L<Cmd<TMsg>>)> Init;
            public CLf<TMsg, TModel, (TModel, L<Cmd<TMsg>>)> Update;
            public CLf<TModel, L<Sub<TMsg>>> Subscribe;
            public CLf<TModel, TScene> View;
            public Maybe<CLa<Exception, TModel, TMsg>> Fail;
            public Maybe<CLa<TModel>> Reset;

            public void Dispose() {
                Init.Dispose();
                Update.Dispose();
                Subscribe.Dispose();
                View.Dispose();
                if (Fail.Test(out var onFail)) {
                    onFail.Dispose();
                }
            }
        }

        private readonly Config _config;
        private readonly IPresenter<TScene> _presenter;
        private readonly List<TMsg> _messages = new();
        private readonly PostMessage _postMessageDelegate;
        private L<Sub<TMsg>> _sub;
        private TModel _model;
        public TModel Model => _model;

        public Runtime(Config config, IPresenter<TScene> presenter) {
            const int initialMemorySize = 1024 * 1024 * 16;

            _config = config;
            _presenter = presenter;
            _presenter.Messenger = this;
            _postMessageDelegate = PostMessage;
            Mem.C = new Mem(initialMemorySize);
            Mem.Prev = new Mem(initialMemorySize);
        }

        public void Run() {
            L<Cmd<TMsg>> cmds;
            (_model, cmds) = _config.Init.Invoke();
            ProcessCommand(cmds);
            ApplyMessages();
        }

        private void ApplyMessages() {
            var originalModel = _model;
            while (true) {
                try {
                    ApplyMessagesUnsafe();
                }
                catch (MemoryLimitReachedException ex) {
                    var sz = Mem.C.Size;
                    while (sz < ex.RequiredSize) {
                        sz *= 2;
                    }
                    Mem.C.Enlarge(sz);
                    Mem.C.Clear();
                    _model = Serializer.Clone(originalModel);
                    Mem.Prev.Enlarge(sz);
                    continue;
                }
                break;
            }
        }

        private void ApplyMessagesUnsafe() {
            Mem.Swap(); //TODO: Multiple Cmd will cause a memory loss, don't call ApplyMessages immediately

            for (var i = 0; i < _messages.Count; i++) {
                var msg = _messages[i];
                try {
                    L<Cmd<TMsg>> cmds;
                    (_model, cmds) = _config.Update.Invoke(msg, _model);
                    ProcessCommand(cmds);
                }
                catch (MemoryLimitReachedException) {
                    throw;
                }
                catch (Exception ex) {
                    if (_config.Fail.Test(out var onFail)) {
                        onFail.Invoke(ex, _model, msg);
                    }
                }
            }

            _model = Serializer.Clone(_model);
            Subscribe();
            Present();
            _messages.Clear();
        }

        private void PushMessage(TMsg msg) {
            _messages.Add(msg);
        }

        private void ProcessCommand(L<Cmd<TMsg>> cmds) {
            var e = cmds.Enumerator;
            while (e.MoveNext()) {
                var c = e.Current;
                c.Exec.Invoke(c.Payload, c.ToMsg, _postMessageDelegate);
            }
        }

        private void Subscribe() {
            _sub = _config.Subscribe.Invoke(_model);
        }

        private void Present() {
            var scene = _config.View.Invoke(_model);
            _presenter.Present(scene);
        }

        public void PostMessage<TPostMsg>(TPostMsg msg) where TPostMsg : unmanaged {
            Assert.That(
                typeof(TPostMsg) == typeof(TMsg),
                $"Message should have {typeof(TMsg).FullName} type but has {typeof(TPostMsg).FullName}"
            );
            PushMessage(*(TMsg*)&msg);
            ApplyMessages();
        }

        public void PostMessage(Ptr msg) {
            Assert.That(
                (Type)msg.Type == typeof(TMsg),
                $"Message should have {typeof(TMsg).FullName} type but has {((Type)msg.Type).FullName}"
            );
            PushMessage(*msg.Cast<TMsg>());
        }

        private void PostMessage(L<Cf<Ptr, Ptr>> toMsg, Ptr result) {
            PostMessage(ChainCall(toMsg, result));
        }

        public void TriggerSub<T>(T payload) where T : unmanaged {
            var th = (Ts)typeof(T);

            var e = _sub.Enumerator;
            while (e.MoveNext()) {
                var c = e.Current;
                if (c.Type == th) {
                    var maybeMsg = ChainCall(c.ToMsg, Mem.C.CopyPtr(payload)).Cast<Maybe<TMsg>>();
                    if (maybeMsg->Test(out var msg)) {
                        PostMessage(msg);
                    }
                }
            }

            if (_messages.Count > 0) {
                ApplyMessages();
            }
        }

        private static Ptr ChainCall(L<Cf<Ptr, Ptr>> chain, Ptr ptr) {
            var e = chain.Enumerator;
            while (e.MoveNext()) {
                ptr = e.Current.Invoke(ptr);
            }
            return ptr;
        }
    }
}