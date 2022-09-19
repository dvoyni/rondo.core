using Rondo.Core.Lib.Containers;
using Rondo.Core.Memory;

namespace Rondo.Core.Lib.Platform {
    public readonly struct Cmd<TMsg> where TMsg : unmanaged {
        internal readonly Ca<Ptr, L<Cf<Ptr, Ptr>>, PostMessage> Exec;
        internal readonly L<Cf<Ptr, Ptr>> ToMsg;
        internal readonly Ptr Payload;

        internal Cmd(Ca<Ptr, L<Cf<Ptr, Ptr>>, PostMessage> exec, L<Cf<Ptr, Ptr>> toMsg, Ptr payload) {
            Exec = exec;
            ToMsg = toMsg;
            Payload = payload;
        }
    }

    public static unsafe class Cmd {
        public static Cmd<TMsg> New<TMsg, TPayload, TResult>(
            delegate*<Ptr, L<Cf<Ptr, Ptr>>, PostMessage, void> exec,
            Cf<TResult, TMsg> toMsg,
            TPayload payload)
                where TMsg : unmanaged
                where TResult : unmanaged
                where TPayload : unmanaged {
            return New<TMsg>(Ca.New(exec), F.ToPtrPtr(toMsg), Mem.C.CopyPtr(payload));
        }

        public static Cmd<TMsg> New<TMsg>(Ca<Ptr, L<Cf<Ptr, Ptr>>, PostMessage> exec, Cf<Ptr, Ptr> toMsg, Ptr payload)
                where TMsg : unmanaged {
            return new Cmd<TMsg>(exec, new L<Cf<Ptr, Ptr>>(toMsg), payload);
        }

        public static L<Cmd<TOutMsg>> Map<TInMsg, TOutMsg>(this L<Cmd<TInMsg>> msgs, delegate*<TInMsg, TOutMsg> convert)
                where TInMsg : unmanaged
                where TOutMsg : unmanaged {
            return msgs.Map(Cf.New<Cmd<TInMsg>, Cf<TInMsg, TOutMsg>, Cmd<TOutMsg>>(&Cast, Cf.New(convert)));
        }

        private static Cmd<TOutMsg> Cast<TInMsg, TOutMsg>(Cmd<TInMsg> cmd, Cf<TInMsg, TOutMsg>* convert)
                where TInMsg : unmanaged
                where TOutMsg : unmanaged {
            return new Cmd<TOutMsg>(cmd.Exec, cmd.ToMsg + F.ToPtrPtr(*convert), cmd.Payload);
        }
    }
}