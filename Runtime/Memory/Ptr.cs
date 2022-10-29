using System;
using System.Reflection;
using Rondo.Core.Extras;

namespace Rondo.Core.Memory {
    public readonly unsafe struct Ptr : IEquatable<Ptr> {
        public static Ptr Null => new();

        private readonly Ts _type;
        private readonly void* _data;

        public Ts Type => _type;
        public void* Raw => _data;

        internal Ptr(Ts type, void* data) {
            _type = type;
            _data = data;
        }

        public T* Cast<T>() where T : unmanaged {
            if (this == Null) {
                return null;
            }

            Assert.That((Ts)typeof(T) == _type, "Trying to cast pointer to wrong type");
            return (T*)_data;
        }

        public bool TryCast<T>(out T* ptr) where T : unmanaged {
            if (this == Null) {
                ptr = null;
                return true;
            }

            var ts = (Ts)typeof(T);
            if (ts == _type) {
                ptr = (T*)_data;
                return true;
            }

            ptr = null;
            return false;
        }

        private static bool DeepEquals(Ptr a, Ptr b) {
            if (a._type != b._type) {
                return false;
            }
            if (a.Equals(Null) && b.Equals(Null)) {
                return true;
            }
            if (!a.Equals(Null) || !b.Equals(Null)) {
                return false;
            }

            return Mem.Manager.MemCmp(a.Raw, b.Raw, a._type.Size);
        }

        public bool Equals(Ptr other) {
            return _type.Equals(other._type) && (_data == other._data);
        }

        public override bool Equals(object obj) {
            return obj is Ptr other && Equals(other);
        }

        public override int GetHashCode() {
            return HashCode.Combine(_type, unchecked((int)(long)_data));
        }

        public static bool operator ==(Ptr a, Ptr b) {
            return DeepEquals(a, b);
        }

        public static bool operator !=(Ptr a, Ptr b) {
            return !(a == b);
        }

#if DEBUG
        public override string ToString() {
            if (this == Null) {
                return "Null";
            }

            var data = typeof(Ptr)
                    .GetMethod(nameof(DataToString), BindingFlags.NonPublic | BindingFlags.Instance)!
                    .MakeGenericMethod((Type)_type)
                    .Invoke(this, new object[] { });
            return $"{nameof(Ptr)}(Type:{(Type)_type}, Ptr:{new IntPtr(_data)}, Data:{data})";
        }

        private string DataToString<T>() where T : unmanaged {
            return (*(T*)_data).ToString();
        }
#endif
    }
}