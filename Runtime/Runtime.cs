using System;
using System.Collections.Generic;
using Rondo.Core.Lib;

namespace Rondo.Core {
    public interface IMsg { }

    public interface IModel { }

    public interface IFlags { }

    public interface ICmd {
        void Execute(IMessageReceiver receiver);
        ICmd Next { get; }
    }

    public interface ISub {
        bool CanHandle<T>();
        IMsg HandleEvent<T>(T eventData);
    }

    public interface IMessageReceiver {
        void PostMessage(IMsg msg);
        void TriggerSub<T>(T eventData);
    }

    public interface IPresenter<TScene> {
        void Present(TScene scene, IMessageReceiver messageReceiver);
    }

    public class Runtime<TScene> : IMessageReceiver {
        public delegate (IModel, ICmd) InitDelegate(IFlags flags);
        public delegate (IModel, ICmd) UpdateDelegate(IMsg message, IModel model);
        public delegate IEnumerable<ISub> SubscribeDelegate(IModel model);
        public delegate TScene ViewDelegate(IModel model);

        public struct Config {
            public InitDelegate Init;
            public UpdateDelegate Update;
            public SubscribeDelegate Subscribe;
            public ViewDelegate View;
        }

        private readonly Config _config;
        private readonly IPresenter<TScene> _presenter;
        private readonly List<IMsg> _msgs = new();
        private IReadOnlyCollection<ISub> _sub;
        private IModel _model;
        private bool _applyingMessages;

        public Runtime(Config config, IPresenter<TScene> presenter) {
            if (IsMutable<IModel>()) {
                throw new Exception($"Expected immutable data structure as {nameof(IModel)}");
            }

            _config = config;
            _presenter = presenter;
        }

        public void Run(IFlags flags) {
            var (model, cmds) = _config.Init.Invoke(flags);
            _model = model;
            ProcessCommand(cmds);
            ApplyMessages();
        }

        private void ApplyMessages() {
            _applyingMessages = true;
            for (var index = 0; index < _msgs.Count; index++) {
                var msg = _msgs[index];
                var (model, cmd) = _config.Update.Invoke(msg, _model);
                _model = model;
                ProcessCommand(cmd);
            }
            _msgs.Clear();
            _applyingMessages = false;

            Subscribe();
            Present();
        }

        private void PushMessage(IMsg msg) {
            _msgs.Add(msg);
        }

        private void ProcessCommand(ICmd cmd) {
            while (cmd != null) {
                cmd.Execute(this);
                cmd = cmd.Next;
            }
        }

        private void Subscribe() {
            _sub = _config.Subscribe.Invoke(_model).ToArr();
        }

        private void Present() {
            var scene = _config.View.Invoke(_model);
            _presenter.Present(scene, this);
        }

        void IMessageReceiver.PostMessage(IMsg msg) {
            PushMessage(msg);
            if (!_applyingMessages) {
                ApplyMessages();
            }
        }

        void IMessageReceiver.TriggerSub<T>(T payload) {
            foreach (var sub in _sub) {
                if (sub.CanHandle<T>()) {
                    _msgs.Add(sub.HandleEvent(payload));
                    ApplyMessages();
                    break;
                }
            }
        }

        private static bool IsMutable<T>() {
            return false; //todo:
        }
    }
}