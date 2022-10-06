using System.Runtime.InteropServices;
using Rondo.Core.Lib.Containers;
using Rondo.Core.Memory;

namespace Rondo.Core.Lib.Platform {
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe readonly struct Sub {
        internal readonly Ts Type;
        internal readonly CLf<Ptr, Ptr> ToMsg;

        internal Sub(Ts type, CLf<Ptr, Ptr> toMsg) {
            Type = type;
            ToMsg = toMsg;
        }

        public static Sub New<TMsg, TEvent>(delegate*<TEvent, Maybe<TMsg>> toMsg)
                where TMsg : unmanaged
                where TEvent : unmanaged {
            return New((Ts)typeof(TEvent), F.ToLPtrPtr(toMsg));
        }

        public static Sub New(Ts type, CLf<Ptr, Ptr> toMsg) {
            return new Sub(type, toMsg);
        }
    }
}