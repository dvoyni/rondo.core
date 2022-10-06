using System.Runtime.InteropServices;
using Rondo.Core.Memory;

namespace Rondo.Core.Lib.Platform {
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe readonly struct Cmd {
        internal readonly delegate*<Ptr, CLf<Ptr, Ptr>, PostMessage, void> Exec;
        internal readonly CLf<Ptr, Ptr> ToMsg;
        internal readonly Ptr Payload;

        internal Cmd(delegate*<Ptr, CLf<Ptr, Ptr>, PostMessage, void> exec, CLf<Ptr, Ptr> toMsg, Ptr payload) {
            Exec = exec;
            ToMsg = toMsg;
            Payload = payload;
        }

        public static Cmd New<TMsg, TPayload, TResult>(
            delegate*<Ptr, CLf<Ptr, Ptr>, PostMessage, void> exec,
            delegate*<TResult, TMsg> toMsg,
            TPayload payload)
                where TMsg : unmanaged
                where TResult : unmanaged
                where TPayload : unmanaged {
            return New(exec, F.ToLPtrPtr(toMsg), Mem.C.CopyPtr(payload));
        }

        private static Cmd New(delegate*<Ptr, CLf<Ptr, Ptr>, PostMessage, void> exec, CLf<Ptr, Ptr> toMsg, Ptr payload) {
            return new Cmd(exec, toMsg, payload);
        }
    }
}