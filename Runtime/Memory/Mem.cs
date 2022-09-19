using System;
using System.Runtime.InteropServices;

namespace Rondo.Core.Memory {
    public unsafe class Mem {
        public static Mem C { get; internal set; }
        public static Mem Prev { get; internal set; }
        private static ulong _lastId;

        private readonly RefHash _refs = new();
        private byte* _stack;
        private int _size;

        private int _offset;

        internal ulong Id { get; private set; }

        internal int Allocated => _offset;
        internal byte* Stack => _stack;
        internal int Size => _size;

        internal Mem(int size, ulong id = 0) {
            _size = size;
            _stack = (byte*)Marshal.AllocHGlobal(_size);
            if (id == 0) {
                Id = ++_lastId;
            }
            else {
                Id = id;
                if (_lastId < id) {
                    _lastId = id;
                }
            }
        }

#if DEBUG
        public override string ToString() {
            return $"{nameof(Mem)}(Id:{Id}, Allocated:{_offset:N0}/{_size:N0})";
        }
#endif

        ~Mem() {
            Marshal.FreeHGlobal((IntPtr)_stack);
        }

        internal void Clear() {
            _offset = 0;
            _refs.Clear();
        }

        public void Enlarge(int newSize) {
            _size = newSize;
            _stack = (byte*)Marshal.ReAllocHGlobal((IntPtr)_stack, (IntPtr)_size);
        }

        public T* Copy<T>(T source) where T : unmanaged {
            return CopyPtr(source).Cast<T>();
        }

        public Ptr CopyPtr<T>(T source) where T : unmanaged {
            var ts = (Ts)typeof(T);
            var dataPtr = Alloc(ts.Size);
            var ptr = new Ptr(ts, dataPtr, Id);
            Buffer.MemoryCopy(&source, dataPtr, ts.Size, ts.Size);
            return ptr;
        }

        public Ptr CopyPtr(Ts type, IntPtr heapPtr) {
            var dataPtr = Alloc(type.Size);
            var ptr = new Ptr(type, dataPtr, Id);
            Buffer.MemoryCopy(heapPtr.ToPointer(), dataPtr, type.Size, type.Size);
            return ptr;
        }

        public Ptr CopyPtr(byte* data, int size) {
            var dataPtr = Alloc(size);
            var ptr = new Ptr((Ts)typeof(byte*), dataPtr, Id);
            Buffer.MemoryCopy(data, dataPtr, size, size);
            return ptr;
        }

        internal Ptr CopyPtr(Ptr other) {
            if (other == Ptr.Null) {
                return Ptr.Null;
            }
            var dataPtr = Alloc(other.Type.Size);
            var ptr = new Ptr(other.Type, dataPtr, Id);
            Buffer.MemoryCopy(other.Raw, dataPtr, other.Type.Size, other.Type.Size);
            return ptr;
        }

        public void* Alloc(int size) {
            var ptr = _stack + _offset;
            _offset += size;
            if (_offset > _size) {
                throw new MemoryLimitReachedException(_offset);
            }

            return ptr;
        }

        public Ref HashObject(object obj) {
            return _refs.Hash(obj);
        }

        public T GetObject<T>(Ref r) where T : class {
            return _refs.Get<T>(r);
        }

        public static void Swap() {
            (Prev, C) = (C, Prev);
            C.Clear();
            C.Id = ++_lastId;
        }

        public static int SizeOf(Type t) {
            if (t.IsEnum) {
                return SizeOf(t.GetEnumUnderlyingType());
            }
            return Marshal.SizeOf(t);
        }

        public static int SizeOf<T>() where T : unmanaged {
            return sizeof(T);
        }
    }
}