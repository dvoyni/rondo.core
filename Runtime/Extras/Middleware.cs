using Rondo.Core.Lib;
using Rondo.Core.Lib.Containers;
using Rondo.Core.Lib.Platform;

namespace Rondo.Core.Extras {
    public static unsafe class Middleware {
        public static CLf<TMsg, TModel, (TModel, L<Cmd<TMsg>>)> UpdateLogged<TModel, TMsg>(
            CLf<TMsg, TModel, (TModel, L<Cmd<TMsg>>)> update,
            CLa<TModel, TMsg, TModel, L<Cmd<TMsg>>> log
        ) where TMsg : unmanaged {
            static (TModel, L<Cmd<TMsg>>) Impl(
                TMsg msg,
                TModel model,
                CLf<TMsg, TModel, (TModel, L<Cmd<TMsg>>)>* update,
                CLa<TModel, TMsg, TModel, L<Cmd<TMsg>>>* log
            ) {
                var (next, cmd) = update->Invoke(msg, model);
                log->Invoke(model, msg, next, cmd);
                return (next, cmd);
            }

            return CLf.New<TMsg, TModel, CLf<TMsg, TModel, (TModel, L<Cmd<TMsg>>)>, CLa<TModel, TMsg, TModel, L<Cmd<TMsg>>>, (TModel, L<Cmd<TMsg>>)>(
                &Impl, update, log);
        }
    }
}