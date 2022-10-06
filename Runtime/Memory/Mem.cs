using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Rondo.Core.Memory {
    public unsafe class Mem {
        public static Mem C { get; internal set; }
        public static Mem Prev { get; internal set; }
        private static ulong _lastId;
        private static readonly Dictionary<Type, Dictionary<string, int>> _offsets = new();
        private static readonly Dictionary<Type, int> _sizes = new();

        internal static void __DomainReload() {
            _offsets.Clear();
            _sizes.Clear();
        }

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

        internal void Free() {
            if (_stack != null) {
                Marshal.FreeHGlobal((IntPtr)_stack);
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

        public static int SizeOf(Type type) {
            if (type.IsEnum) {
                return SizeOf(type.GetEnumUnderlyingType());
            }
            /*if (_sizes.TryGetValue(type, out var sz)) {
                //Assert.That(sz == Marshal.SizeOf(type), "Marshal.SizeOf mismatch");
                return sz;
            }

            if (type.Name == "Void*") {
                return SizeOf(typeof(IntPtr));
            }

            if (type.IsValueType && !type.IsEnum && !type.IsPrimitive) {
                sz = 0;
                var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                foreach (var fi in fields) {
                    sz += SizeOf(fi.FieldType);
                }
                _sizes[type] = sz;
                

                if (sz != Marshal.SizeOf(type)) {
                    var x = $"Marshal.SizeOf({type}) mismatch";
                }
                return sz;
            }*/

            return Marshal.SizeOf(type);
        }

        public static int SizeOf<T>() where T : unmanaged {
            return sizeof(T);// SizeOf(typeof(T));
            
        }

        public static int OffsetOf(Type type, string name) {
           /* if (_offsets.TryGetValue(type, out var t)) {
                if (t.TryGetValue(name, out var sz)) {
                    if (sz != (int)Marshal.OffsetOf(type, name)) {
                        var x = $"Marshal.OffsetOf({type}) mismatch";
                    }
                    return sz;
                }
            }*/
            return (int)Marshal.OffsetOf(type, name);
        }

        public static void __RegisterOffsetOf<T>(string name, int offset) {
            if (!_offsets.TryGetValue(typeof(T), out var t)) {
                _offsets[typeof(T)] = t = new();
            }
            t[name] = offset;
        }

        public static void __RegisterSizeOf(Type t,int size) {
            _sizes[t] = size;
        }
    }
}