using Rondo.Core.Memory;

namespace Rondo.Core.Lib.Platform {
    public readonly unsafe struct Cmd {
        internal readonly Ca<Ptr, Xf<Ptr, Ptr>, PostMessage> Exec;
        internal readonly Xf<Ptr, Ptr> ToMsg;
        internal readonly Ptr Payload;

        private Cmd(Ca<Ptr, Xf<Ptr, Ptr>, PostMessage> exec, Xf<Ptr, Ptr> toMsg, Ptr payload) {
            Exec = exec;
            ToMsg = toMsg;
            Payload = payload;
        }

        public static Cmd New<TMsg, TPayload, TResult>(
            delegate*<Ptr, Xf<Ptr, Ptr>, PostMessage, void> exec,
            Xf<TResult, TMsg> toMsg,
            TPayload payload)
                where TMsg : unmanaged
                where TResult : unmanaged
                where TPayload : unmanaged {
            return new Cmd(Ca.New(exec), F.ToPtrPtr(toMsg), Mem.C.CopyPtr(payload));
        }
    }
}