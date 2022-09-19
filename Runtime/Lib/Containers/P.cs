namespace Rondo.Core.Lib.Containers {
    public readonly struct P<TK, TV> where TK : unmanaged where TV : unmanaged {
        public readonly TK Key;
        public readonly TV Value;

        public P(TK key, TV value) {
            Key = key;
            Value = value;
        }
    }

    public static class P {
        public static TK Key<TK, TV>(this P<TK, TV> p) where TK : unmanaged where TV : unmanaged {
            return p.Key;
        }

        public static TV Value<TK, TV>(this P<TK, TV> p) where TK : unmanaged where TV : unmanaged {
            return p.Value;
        }
    }
}