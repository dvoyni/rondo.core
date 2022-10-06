using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Rondo.Core.Extras;

namespace Rondo.Core.Memory {
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly unsafe struct Ptr : IEquatable<Ptr> {
        public static Ptr Null => new();

        private readonly Ts _type;
        private readonly void* _data;
        private readonly ulong _memId;

        public Ts Type => _type;
        public void* Raw => _data;

        internal Ptr(Ts type, void* data, ulong memId) {
            _memId = memId;
            _type = type;
            _data = data;
        }

        public T* Cast<T>() where T : unmanaged {
            if (this == Null) {
                return null;
            }

            Assert.That((Ts)typeof(T) == _type, "Trying to cast pointer to wrong type");
            Assert.That((_memId == Mem.C.Id) || (_memId == Mem.Prev.Id), "Trying go cast expired pointer");
            return (T*)_data;
        }

        public bool TryCast<T>(out T* ptr) where T : unmanaged {
            if (this == Null) {
                ptr = null;
                return true;
            }

            Assert.That((_memId == Mem.C.Id) || (_memId == Mem.C.Id), "Trying go cast expired pointer");
            var ts = (Ts)typeof(T);
            if (ts == _type) {
                ptr = (T*)_data;
                return true;
            }

            ptr = null;
            return false;
        }

        public void CopyToHeap(ref IntPtr ptr, ref int capacity, out Ts type) {
            type = _type;

            if (ptr == IntPtr.Zero) {
                capacity = _type.Size;
                ptr = Marshal.AllocHGlobal(capacity);
            }
            else {
                if (capacity < _type.Size) {
                    capacity = _type.Size;
                    ptr = Marshal.ReAllocHGlobal(ptr, (IntPtr)capacity);
                }
            }
            Buffer.MemoryCopy(_data, ptr.ToPointer(), capacity, _type.Size);
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

            var sz = a._type.Size - 8;

            var i = 0;
            var lx = (long*)a._data;
            var ly = (long*)b._data;
            for (; i <= sz; i += 8) {
                if (*lx != *ly) {
                    return false;
                }
                lx++;
                ly++;
            }

            sz += 8;
            var bx = (byte*)lx;
            var by = (byte*)ly;
            for (; i < sz; i++) {
                if (*bx != *by) {
                    return false;
                }
                bx++;
                by++;
            }

            return true;
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
            return $"{nameof(Ptr)}(Type:{(Type)_type}, MemId:{_memId}, Ptr:{(IntPtr)_data}, Data:{data})";
        }

        private string DataToString<T>() where T : unmanaged {
            return (*(T*)_data).ToString();
        }
#endif
    }
}