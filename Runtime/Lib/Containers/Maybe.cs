using System;
using System.Runtime.InteropServices;
using Rondo.Core.Memory;

namespace Rondo.Core.Lib.Containers {
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly unsafe struct Maybe<T> : IStringify where T : unmanaged {
        private readonly Ts _type;
        private readonly T _value;
        private readonly bool _assigned;

        private Maybe(T value) {
            _assigned = true;
            _value = value;
            _type = (Ts)typeof(T);
        }

        public bool Test(out T value) {
            value = _value;
            return _assigned;
        }

        public T ValueOrDefault => _assigned ? _value : default;

        public static Maybe<T> Nothing => new();

        public static Maybe<T> Just(T value) {
            return new Maybe<T>(value);
        }

        public Ptr PtrOrNull {
            get {
                if (!_assigned) {
                    return Ptr.Null;
                }
                fixed (void* ptr = &_value) {
                    return Mem.C.CopyPtr(_type, (IntPtr)ptr);
                }
            }
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