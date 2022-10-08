using Rondo.Core.Lib.Containers;
using Rondo.Core.Memory;

namespace Rondo.Core.Lib.Platform {
    public readonly unsafe struct Cmd {
        internal readonly Ca<Ptr, Cf<Ptr, Ptr>, PostMessage> Exec;
        internal readonly Cf<Ptr, Ptr> ToMsg;
        internal readonly Ptr Payload;

        private Cmd(Ca<Ptr, Cf<Ptr, Ptr>, PostMessage> exec, Cf<Ptr, Ptr> toMsg, Ptr payload) {
            Exec = exec;
            ToMsg = toMsg;
            Payload = payload;
        }

        public static Cmd New<TMsg, TPayload, TResult>(
            delegate*<Ptr, Cf<Ptr, Ptr>, PostMessage, void> exec,
            Cf<TResult, TMsg> toMsg,
            TPayload payload)
                where TMsg : unmanaged
                where TResult : unmanaged
                where TPayload : unmanaged {
            return new Cmd(Ca.New(exec), F.ToPtrPtr(toMsg), Mem.C.CopyPtr(payload));
        }
    }
}