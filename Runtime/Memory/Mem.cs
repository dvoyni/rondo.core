using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace Rondo.Core.Memory {
    public unsafe class Mem {
        internal static void __DomainReload() {
            _localMemory.Clear();
        }

        private static readonly Dictionary<Thread, Mem> _localMemory = new();

        public static Mem C {
            get {
                if (!_localMemory.TryGetValue(Thread.CurrentThread, out var mem)) {
                    mem = new Mem();
                    _localMemory[Thread.CurrentThread] = mem;
                }
                return mem;
            }
            internal set { _localMemory[Thread.CurrentThread] = value; }
        }

        public static IMemManager Manager = new MemManager();
        private readonly RefHash _refs = new();
        private byte* _stack;
        private int _size;

        private int _offset;

        internal int Allocated => _offset;
        internal byte* Stack => _stack;
        internal int Size => _size;

        internal Mem(int size = 1025 * 1024 * 128) {
            _size = size;
            _stack = (byte*)Manager.Alloc(_size);
        }

#if DEBUG
        public override string ToString() {
            return $"{nameof(Mem)}(Allocated:{_offset:N0}/{_size:N0})";
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
            var ptr = new Ptr(ts, dataPtr);
            Manager.MemCpy(&source, dataPtr, ts.Size);
            return ptr;
        }

        public Ptr CopyPtrFromOuterMemory(Ts type, IntPtr unmanagedPtr) {
            if (type.Size > 0) {
                var dataPtr = Alloc(type.Size);
                var ptr = new Ptr(type, dataPtr);
                Manager.MemCpy(unmanagedPtr.ToPointer(), dataPtr, type.Size);
                return ptr;
            }
            
            return new Ptr(type, null);
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

        public Ptr CopyPtr(byte* data, int size) {
            var dataPtr = Alloc(size);
            var ptr = new Ptr((Ts)typeof(byte*), dataPtr);
            Manager.MemCpy(data, dataPtr, size);
            return ptr;
        }

        internal Ptr CopyPtr(Ptr other) {
            if (other == Ptr.Null) {
                return Ptr.Null;
            }
            var dataPtr = Alloc(other.Type.Size);
            var ptr = new Ptr(other.Type, dataPtr);
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