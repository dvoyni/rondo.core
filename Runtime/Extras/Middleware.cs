using Rondo.Core.Lib;
using Rondo.Core.Lib.Containers;
using Rondo.Core.Lib.Platform;

namespace Rondo.Core.Extras {
    public static unsafe class Middleware {
        public static Xf<TMsg, TModel, (TModel, A<Cmd>)> UpdateLogged<TModel, TMsg>(
            Xf<TMsg, TModel, (TModel, A<Cmd>)> update,
            Xa<TModel, TMsg, TModel, A<Cmd>> log
        ) where TMsg : unmanaged {
            static (TModel, A<Cmd>) Impl(
                TMsg msg,
                TModel model,
                Xf<TMsg, TModel, (TModel, A<Cmd>)>* update,
                Xa<TModel, TMsg, TModel, A<Cmd>>* log
            ) {
                var (next, cmd) = update->Invoke(msg, model);
                log->Invoke(model, msg, next, cmd);
                return (next, cmd);
            }

            return Xf.New<TMsg, TModel, Xf<TMsg, TModel, (TModel, A<Cmd>)>, Xa<TModel, TMsg, TModel, A<Cmd>>, (TModel, A<Cmd>)>(
                &Impl, update, log);
        }
    }
}