using Rondo.Core.Lib.Containers;
using Rondo.Core.Memory;

namespace Rondo.Core.Lib.Platform {
    public readonly unsafe struct Sub {
        internal readonly Ts Type;
        internal readonly Cf<Ptr, Ptr> ToMsg;

        private Sub(Ts type, Cf<Ptr, Ptr> toMsg) {
            Type = type;
            ToMsg = toMsg;
        }

        public static Sub New<TMsg, TEvent>(delegate*<TEvent, Maybe<TMsg>> toMsg)
                where TMsg : unmanaged
                where TEvent : unmanaged {
            return New((Ts)typeof(TEvent), F.ToPtrPtr(toMsg));
        }

        public static Sub New(Ts type, Cf<Ptr, Ptr> toMsg) {
            return new Sub(type, toMsg);
        }
    }
}