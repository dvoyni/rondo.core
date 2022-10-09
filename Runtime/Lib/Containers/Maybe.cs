using System.Runtime.InteropServices;
using Rondo.Core.Memory;

namespace Rondo.Core.Lib.Containers {
    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct Maybe<T> : IStringify where T : unmanaged {
        private readonly bool _assigned;
        private readonly Ts _type;
        private readonly T _value;

        private Maybe(T value) {
            _assigned = true;
            _value = value;
            _type = (Ts)typeof(T);
        }

        public bool Test(out T value) {
            value = _value;
            return _assigned;
        }

        public static Maybe<T> Nothing => new();

        public static Maybe<T> Just(T value) {
            return new Maybe<T>(value);
        }

        public TX Match<TX>(delegate*<T, TX> just, delegate*<TX> nothing) {
            return Test(out var v) ? just(v) : nothing();
        }

        public void Match(delegate*<T, void> just, delegate*<void> nothing) {
            if (Test(out var v)) {
                just(v);
            }
            else {
                nothing();
            }
        }

        public string Stringify(string offset) {
            return Test(out var v) ? $"{offset}Just({v})" : $"{offset}Nothing";
        }

#if DEBUG
        public override string ToString() {
            return Serializer.Stringify(this);
        }
#endif
    }

    public static class Maybe {
        public static bool HasValue<T>(Maybe<T> m) where T : unmanaged {
            return m.Test(out _);
        }
    }
}