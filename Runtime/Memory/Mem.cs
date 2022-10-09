using System;
using System.Reflection;

namespace Rondo.Core.Memory {
    public unsafe class Mem {
        public static Mem C { get; internal set; }
        public static Mem Prev { get; internal set; }
        private static ulong _lastId;

        public static IMemManager Manager = new MemManager();
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
            _stack = (byte*)Manager.Alloc(_size);
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

        internal void Free() {
            if (_stack != null) {
                Manager.Free(_stack);
                _stack = null;
            }
        }

        ~Mem() {
            Free();
        }

        internal void Clear() {
            _offset = 0;
            _refs.Clear();
        }

        public void Enlarge(int newSize) {
            Free();
            _stack = (byte*)Manager.Alloc(newSize);
            _size = newSize;
        }

        public T* Copy<T>(T source) where T : unmanaged {
            return CopyPtr(source).Cast<T>();
        }

        public Ptr CopyPtr<T>(T source) where T : unmanaged {
            var ts = (Ts)typeof(T);
            var dataPtr = Alloc(ts.Size);
            var ptr = new Ptr(ts, dataPtr, Id);
            Manager.MemCpy(&source, dataPtr, ts.Size);
            return ptr;
        }

        public Ptr CopyPtrFromOuterMemory(Ts type, IntPtr unmanagedPtr) {
            var dataPtr = Alloc(type.Size);
            var ptr = new Ptr(type, dataPtr, Id);
            Manager.MemCpy(unmanagedPtr.ToPointer(), dataPtr, type.Size);
            return ptr;
        }

        public static IntPtr AllocOuterMemory(long size) {
            return new IntPtr(Manager.Alloc(size));
        }

        public static IntPtr AllocOuterMemoryAndCopy(void* src, long size) {
            var dst = AllocOuterMemory(size);
            Manager.MemCpy(src, dst.ToPointer(), size);
            return dst;
        }

        public static void AllocAndCopyPtrToOuterMemory(Ptr src, ref IntPtr dst, ref int capacity, out Ts type) {
            type = src.Type;

            if (dst == null) {
                capacity = type.Size;
                dst = new IntPtr(Manager.Alloc(capacity));
            }
            else {
                if (capacity < type.Size) {
                    capacity = type.Size;
                    Manager.Free(dst.ToPointer());
                    dst = new IntPtr(Manager.Alloc(capacity));
                }
            }

            Manager.MemCpy(src.Raw, dst.ToPointer(), type.Size);
        }

        public static void FreeOuterMemory(ref IntPtr mem) {
            if (mem != IntPtr.Zero) {
                Manager.Free(mem.ToPointer());
                mem = IntPtr.Zero;
            }
        }

        public static void FreeOuterMemory(IntPtr mem) {
            if (mem != IntPtr.Zero) {
                Manager.Free(mem.ToPointer());
            }
        }

        public Ptr CopyPtr(byte* data, int size) {
            var dataPtr = Alloc(size);
            var ptr = new Ptr((Ts)typeof(byte*), dataPtr, Id);
            Manager.MemCpy(data, dataPtr, size);
            return ptr;
        }

        internal Ptr CopyPtr(Ptr other) {
            if (other == Ptr.Null) {
                return Ptr.Null;
            }
            var dataPtr = Alloc(other.Type.Size);
            var ptr = new Ptr(other.Type, dataPtr, Id);
            Manager.MemCpy(other.Raw, dataPtr, other.Type.Size);
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
            return Manager.SizeOf(t);
        }

        public static int SizeOf<T>() where T : unmanaged {
            return Manager.SizeOf<T>();
        }

        public static int OffsetOf(FieldInfo fi) {
            return Manager.GetFieldOffset(fi);
        }
    }
}