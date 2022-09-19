using Rondo.Core.Lib.Containers;
using Rondo.Core.Memory;

namespace Rondo.Core.Lib.Platform {
    public readonly struct Sub<TMsg> where TMsg : unmanaged {
        internal readonly Ts Type;
        internal readonly L<Cf<Ptr, Ptr>> ToMsg;

        internal Sub(Ts type, L<Cf<Ptr, Ptr>> toMsg) {
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
            return new Sub<TMsg>(type, new L<Cf<Ptr, Ptr>>(toMsg));
        }

        public static L<Sub<TOutMsg>> Map<TInMsg, TOutMsg>(this L<Sub<TInMsg>> subs, delegate*<TInMsg, TOutMsg> convert)
                where TInMsg : unmanaged
                where TOutMsg : unmanaged {
            return subs.Map(Cf.New<Sub<TInMsg>, Cf<TInMsg, TOutMsg>, Sub<TOutMsg>>(&Cast, Cf.New(convert)));
        }

        private static Sub<TOutMsg> Cast<TInMsg, TOutMsg>(Sub<TInMsg> sub, Cf<TInMsg, TOutMsg>* convert)
                where TInMsg : unmanaged
                where TOutMsg : unmanaged {
            var toMsg = Cf.New<Ptr, Cf<TInMsg, TOutMsg>, Ptr>(&CastMaybeSub<TInMsg, TOutMsg>, *convert);
            return new Sub<TOutMsg>(sub.Type, sub.ToMsg + toMsg);
        }

        private static Ptr CastMaybeSub<TInMsg, TOutMsg>(Ptr sub, Cf<TInMsg, TOutMsg>* c)
                where TInMsg : unmanaged
                where TOutMsg : unmanaged {
            var maybeMsg = sub.Cast<Maybe<TInMsg>>();
            if (maybeMsg->Test(out var msg)) {
                return Mem.C.CopyPtr(Maybe<TOutMsg>.Just(c->Invoke(msg)));
            }
            return Mem.C.CopyPtr(Maybe<TOutMsg>.Nothing);
        }
    }
}