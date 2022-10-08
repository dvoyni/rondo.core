using Rondo.Core.Lib.Containers;
using Rondo.Core.Memory;

namespace Rondo.Core.Lib.Platform {
    public readonly struct Sub<TMsg> where TMsg : unmanaged {
        internal readonly Ts Type;
        internal readonly Cf<Ptr, Ptr> ToMsg;

        internal Sub(Ts type, Cf<Ptr, Ptr> toMsg) {
            Type = type;
            ToMsg = toMsg;
        }
    }

    public static unsafe class Sub {
        public static Sub<TMsg> New<TMsg, TEvent>(delegate*<TEvent, Maybe<TMsg>> toMsg)
                where TMsg : unmanaged
                where TEvent : unmanaged {
            return New<TMsg>((Ts)typeof(TEvent), F.ToPtrPtr(toMsg));
        }

        public static Sub<TMsg> New<TMsg>(Ts type, Cf<Ptr, Ptr> toMsg)
                where TMsg : unmanaged {
            return new Sub<TMsg>(type, toMsg);
        }
    }
}