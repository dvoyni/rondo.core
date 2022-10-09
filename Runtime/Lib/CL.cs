//DO NOT MODIFY. FILE IS GENERATED

using System;
using System.Runtime.InteropServices;
using Rondo.Core.Extras;
using Rondo.Core.Memory;

namespace Rondo.Core.Lib {
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly unsafe struct CLf<TR> : IDisposable, IEquatable<CLf<TR>> {
        private readonly IntPtr _arg0;
        private readonly IntPtr _arg1;
        private readonly IntPtr _arg2;
        private readonly IntPtr _arg3;
        private readonly int _arity;
        private readonly void* _fn;

        public CLf(delegate*<TR> fn) {
            _fn = fn;
            _arg0 = IntPtr.Zero;
            _arg1 = IntPtr.Zero;
            _arg2 = IntPtr.Zero;
            _arg3 = IntPtr.Zero;
            _arity = 0;
        }

        public CLf(delegate*<void*, TR> fn, IntPtr a0) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = IntPtr.Zero;
            _arg2 = IntPtr.Zero;
            _arg3 = IntPtr.Zero;
            _arity = 1;
        }

        public CLf(delegate*<void*, void*, TR> fn, IntPtr a0, IntPtr a1) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = a1;
            _arg2 = IntPtr.Zero;
            _arg3 = IntPtr.Zero;
            _arity = 2;
        }

        public CLf(delegate*<void*, void*, void*, TR> fn, IntPtr a0, IntPtr a1, IntPtr a2) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = a1;
            _arg2 = a2;
            _arg3 = IntPtr.Zero;
            _arity = 3;
        }

        public CLf(delegate*<void*, void*, void*, void*, TR> fn, IntPtr a0, IntPtr a1, IntPtr a2, IntPtr a3) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = a1;
            _arg2 = a2;
            _arg3 = a3;
            _arity = 4;
        }

        public TR Invoke() {
            switch (_arity) {
                case 0:
                    return ((delegate*<TR>)_fn)();
                case 1:
                    return ((delegate*<void*, TR>)_fn)(_arg0.ToPointer());
                case 2:
                    return ((delegate*<void*, void*, TR>)_fn)(_arg0.ToPointer(), _arg1.ToPointer());
                case 3:
                    return ((delegate*<void*, void*, void*, TR>)_fn)(_arg0.ToPointer(), _arg1.ToPointer(), _arg2.ToPointer());
                case 4:
                    return ((delegate*<void*, void*, void*, void*, TR>)_fn)(_arg0.ToPointer(), _arg1.ToPointer(), _arg2.ToPointer(), _arg3.ToPointer());
                default:
                    Assert.Fail("Unsupported closure arity");
                    return default;
            }
        }

        public void Dispose() {
            if (_arg0 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg0);
            }
            if (_arg1 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg1);
            }
            if (_arg2 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg2);
            }
            if (_arg3 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg3);
            }
        }

        public bool Equals(CLf<TR> other) {
#pragma warning disable CS8909
            return _fn == other._fn && _arity == other._arity && _arg0 == other._arg0 && _arg1 == other._arg1 && _arg2 == other._arg2 && _arg3 == other._arg3;
#pragma warning restore CS8909
        }
    }

    public static unsafe partial class CLf {
        public static CLf<TR> New<TR>(delegate*<TR> fn) {
            return new CLf<TR>((delegate*<TR>)fn);
        }

        public static CLf<TR> New<A0, TR>(delegate*<A0*, TR> fn, A0 a0)
                where A0 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            return new CLf<TR>((delegate*<void*, TR>)fn, pa0);
        }

        public static CLf<TR> New<A0, A1, TR>(delegate*<A0*, A1*, TR> fn, A0 a0, A1 a1)
                where A0 : unmanaged
                where A1 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            var sz1 = Mem.SizeOf<A1>();
            var pa1 = Mem.AllocOuterMemoryAndCopy(&a1, sz1);
            return new CLf<TR>((delegate*<void*, void*, TR>)fn, pa0, pa1);
        }

        public static CLf<TR> New<A0, A1, A2, TR>(delegate*<A0*, A1*, A2*, TR> fn, A0 a0, A1 a1, A2 a2)
                where A0 : unmanaged
                where A1 : unmanaged
                where A2 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            var sz1 = Mem.SizeOf<A1>();
            var pa1 = Mem.AllocOuterMemoryAndCopy(&a1, sz1);
            var sz2 = Mem.SizeOf<A2>();
            var pa2 = Mem.AllocOuterMemoryAndCopy(&a2, sz2);
            return new CLf<TR>((delegate*<void*, void*, void*, TR>)fn, pa0, pa1, pa2);
        }

        public static CLf<TR> New<A0, A1, A2, A3, TR>(delegate*<A0*, A1*, A2*, A3*, TR> fn, A0 a0, A1 a1, A2 a2, A3 a3)
                where A0 : unmanaged
                where A1 : unmanaged
                where A2 : unmanaged
                where A3 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            var sz1 = Mem.SizeOf<A1>();
            var pa1 = Mem.AllocOuterMemoryAndCopy(&a1, sz1);
            var sz2 = Mem.SizeOf<A2>();
            var pa2 = Mem.AllocOuterMemoryAndCopy(&a2, sz2);
            var sz3 = Mem.SizeOf<A3>();
            var pa3 = Mem.AllocOuterMemoryAndCopy(&a3, sz3);
            return new CLf<TR>((delegate*<void*, void*, void*, void*, TR>)fn, pa0, pa1, pa2, pa3);
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly unsafe struct CLa<T0> : IDisposable, IEquatable<CLa<T0>> {
        private readonly IntPtr _arg0;
        private readonly IntPtr _arg1;
        private readonly IntPtr _arg2;
        private readonly IntPtr _arg3;
        private readonly int _arity;
        private readonly void* _fn;

        public CLa(delegate*<T0, void> fn) {
            _fn = fn;
            _arg0 = IntPtr.Zero;
            _arg1 = IntPtr.Zero;
            _arg2 = IntPtr.Zero;
            _arg3 = IntPtr.Zero;
            _arity = 0;
        }

        public CLa(delegate*<T0, IntPtr, void> fn, IntPtr a0) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = IntPtr.Zero;
            _arg2 = IntPtr.Zero;
            _arg3 = IntPtr.Zero;
            _arity = 1;
        }

        public CLa(delegate*<T0, IntPtr, IntPtr, void> fn, IntPtr a0, IntPtr a1) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = a1;
            _arg2 = IntPtr.Zero;
            _arg3 = IntPtr.Zero;
            _arity = 2;
        }

        public CLa(delegate*<T0, IntPtr, IntPtr, IntPtr, void> fn, IntPtr a0, IntPtr a1, IntPtr a2) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = a1;
            _arg2 = a2;
            _arg3 = IntPtr.Zero;
            _arity = 3;
        }

        public CLa(delegate*<T0, IntPtr, IntPtr, IntPtr, IntPtr, void> fn, IntPtr a0, IntPtr a1, IntPtr a2, IntPtr a3) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = a1;
            _arg2 = a2;
            _arg3 = a3;
            _arity = 4;
        }

        public void Invoke(T0 p0) {
            switch (_arity) {
                case 0:
                    ((delegate*<T0, void>)_fn)(p0);
                    break;
                case 1:
                    ((delegate*<T0, void*, void>)_fn)(p0, _arg0.ToPointer());
                    break;
                case 2:
                    ((delegate*<T0, void*, void*, void>)_fn)(p0, _arg0.ToPointer(), _arg1.ToPointer());
                    break;
                case 3:
                    ((delegate*<T0, void*, void*, void*, void>)_fn)(p0, _arg0.ToPointer(), _arg1.ToPointer(), _arg2.ToPointer());
                    break;
                case 4:
                    ((delegate*<T0, void*, void*, void*, void*, void>)_fn)(p0, _arg0.ToPointer(), _arg1.ToPointer(), _arg2.ToPointer(), _arg3.ToPointer());
                    break;
                default:
                    Assert.Fail("Unsupported closure arity");
                    return;
            }
        }

        public void Dispose() {
            if (_arg0 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg0);
            }
            if (_arg1 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg1);
            }
            if (_arg2 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg2);
            }
            if (_arg3 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg3);
            }
        }

        public bool Equals(CLa<T0> other) {
#pragma warning disable CS8909
            return _fn == other._fn && _arity == other._arity && _arg0 == other._arg0 && _arg1 == other._arg1 && _arg2 == other._arg2 && _arg3 == other._arg3;
#pragma warning restore CS8909
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly unsafe struct CLf<T0, TR> : IDisposable, IEquatable<CLf<T0, TR>> {
        private readonly IntPtr _arg0;
        private readonly IntPtr _arg1;
        private readonly IntPtr _arg2;
        private readonly IntPtr _arg3;
        private readonly int _arity;
        private readonly void* _fn;

        public CLf(delegate*<T0, TR> fn) {
            _fn = fn;
            _arg0 = IntPtr.Zero;
            _arg1 = IntPtr.Zero;
            _arg2 = IntPtr.Zero;
            _arg3 = IntPtr.Zero;
            _arity = 0;
        }

        public CLf(delegate*<T0, void*, TR> fn, IntPtr a0) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = IntPtr.Zero;
            _arg2 = IntPtr.Zero;
            _arg3 = IntPtr.Zero;
            _arity = 1;
        }

        public CLf(delegate*<T0, void*, void*, TR> fn, IntPtr a0, IntPtr a1) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = a1;
            _arg2 = IntPtr.Zero;
            _arg3 = IntPtr.Zero;
            _arity = 2;
        }

        public CLf(delegate*<T0, void*, void*, void*, TR> fn, IntPtr a0, IntPtr a1, IntPtr a2) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = a1;
            _arg2 = a2;
            _arg3 = IntPtr.Zero;
            _arity = 3;
        }

        public CLf(delegate*<T0, void*, void*, void*, void*, TR> fn, IntPtr a0, IntPtr a1, IntPtr a2, IntPtr a3) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = a1;
            _arg2 = a2;
            _arg3 = a3;
            _arity = 4;
        }

        public TR Invoke(T0 p0) {
            switch (_arity) {
                case 0:
                    return ((delegate*<T0, TR>)_fn)(p0);
                case 1:
                    return ((delegate*<T0, void*, TR>)_fn)(p0, _arg0.ToPointer());
                case 2:
                    return ((delegate*<T0, void*, void*, TR>)_fn)(p0, _arg0.ToPointer(), _arg1.ToPointer());
                case 3:
                    return ((delegate*<T0, void*, void*, void*, TR>)_fn)(p0, _arg0.ToPointer(), _arg1.ToPointer(), _arg2.ToPointer());
                case 4:
                    return ((delegate*<T0, void*, void*, void*, void*, TR>)_fn)(p0, _arg0.ToPointer(), _arg1.ToPointer(), _arg2.ToPointer(), _arg3.ToPointer());
                default:
                    Assert.Fail("Unsupported closure arity");
                    return default;
            }
        }

        public void Dispose() {
            if (_arg0 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg0);
            }
            if (_arg1 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg1);
            }
            if (_arg2 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg2);
            }
            if (_arg3 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg3);
            }
        }

        public bool Equals(CLf<T0, TR> other) {
#pragma warning disable CS8909
            return _fn == other._fn && _arity == other._arity && _arg0 == other._arg0 && _arg1 == other._arg1 && _arg2 == other._arg2 && _arg3 == other._arg3;
#pragma warning restore CS8909
        }
    }

    public static unsafe partial class CLa {
        public static CLa<T0> New<T0>(delegate*<T0, void> fn) {
            return new CLa<T0>((delegate*<T0, void>)fn);
        }

        public static CLa<T0> New<T0, A0>(delegate*<T0, A0*, void> fn, A0 a0)
                where A0 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            return new CLa<T0>((delegate*<T0, IntPtr, void>)fn, pa0);
        }

        public static CLa<T0> New<T0, A0, A1>(delegate*<T0, A0*, A1*, void> fn, A0 a0, A1 a1)
                where A0 : unmanaged
                where A1 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            var sz1 = Mem.SizeOf<A1>();
            var pa1 = Mem.AllocOuterMemoryAndCopy(&a1, sz1);
            return new CLa<T0>((delegate*<T0, IntPtr, IntPtr, void>)fn, pa0, pa1);
        }

        public static CLa<T0> New<T0, A0, A1, A2>(delegate*<T0, A0*, A1*, A2*, void> fn, A0 a0, A1 a1, A2 a2)
                where A0 : unmanaged
                where A1 : unmanaged
                where A2 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            var sz1 = Mem.SizeOf<A1>();
            var pa1 = Mem.AllocOuterMemoryAndCopy(&a1, sz1);
            var sz2 = Mem.SizeOf<A2>();
            var pa2 = Mem.AllocOuterMemoryAndCopy(&a2, sz2);
            return new CLa<T0>((delegate*<T0, IntPtr, IntPtr, IntPtr, void>)fn, pa0, pa1, pa2);
        }

        public static CLa<T0> New<T0, A0, A1, A2, A3>(delegate*<T0, A0*, A1*, A2*, A3*, void> fn, A0 a0, A1 a1, A2 a2, A3 a3)
                where A0 : unmanaged
                where A1 : unmanaged
                where A2 : unmanaged
                where A3 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            var sz1 = Mem.SizeOf<A1>();
            var pa1 = Mem.AllocOuterMemoryAndCopy(&a1, sz1);
            var sz2 = Mem.SizeOf<A2>();
            var pa2 = Mem.AllocOuterMemoryAndCopy(&a2, sz2);
            var sz3 = Mem.SizeOf<A3>();
            var pa3 = Mem.AllocOuterMemoryAndCopy(&a3, sz3);
            return new CLa<T0>((delegate*<T0, IntPtr, IntPtr, IntPtr, IntPtr, void>)fn, pa0, pa1, pa2, pa3);
        }
    }

    public static unsafe partial class CLf {
        public static CLf<T0, TR> New<T0, TR>(delegate*<T0, TR> fn) {
            return new CLf<T0, TR>((delegate*<T0, TR>)fn);
        }

        public static CLf<T0, TR> New<T0, A0, TR>(delegate*<T0, A0*, TR> fn, A0 a0)
                where A0 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            return new CLf<T0, TR>((delegate*<T0, void*, TR>)fn, pa0);
        }

        public static CLf<T0, TR> New<T0, A0, A1, TR>(delegate*<T0, A0*, A1*, TR> fn, A0 a0, A1 a1)
                where A0 : unmanaged
                where A1 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            var sz1 = Mem.SizeOf<A1>();
            var pa1 = Mem.AllocOuterMemoryAndCopy(&a1, sz1);
            return new CLf<T0, TR>((delegate*<T0, void*, void*, TR>)fn, pa0, pa1);
        }

        public static CLf<T0, TR> New<T0, A0, A1, A2, TR>(delegate*<T0, A0*, A1*, A2*, TR> fn, A0 a0, A1 a1, A2 a2)
                where A0 : unmanaged
                where A1 : unmanaged
                where A2 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            var sz1 = Mem.SizeOf<A1>();
            var pa1 = Mem.AllocOuterMemoryAndCopy(&a1, sz1);
            var sz2 = Mem.SizeOf<A2>();
            var pa2 = Mem.AllocOuterMemoryAndCopy(&a2, sz2);
            return new CLf<T0, TR>((delegate*<T0, void*, void*, void*, TR>)fn, pa0, pa1, pa2);
        }

        public static CLf<T0, TR> New<T0, A0, A1, A2, A3, TR>(delegate*<T0, A0*, A1*, A2*, A3*, TR> fn, A0 a0, A1 a1, A2 a2, A3 a3)
                where A0 : unmanaged
                where A1 : unmanaged
                where A2 : unmanaged
                where A3 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            var sz1 = Mem.SizeOf<A1>();
            var pa1 = Mem.AllocOuterMemoryAndCopy(&a1, sz1);
            var sz2 = Mem.SizeOf<A2>();
            var pa2 = Mem.AllocOuterMemoryAndCopy(&a2, sz2);
            var sz3 = Mem.SizeOf<A3>();
            var pa3 = Mem.AllocOuterMemoryAndCopy(&a3, sz3);
            return new CLf<T0, TR>((delegate*<T0, void*, void*, void*, void*, TR>)fn, pa0, pa1, pa2, pa3);
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly unsafe struct CLa<T0, T1> : IDisposable, IEquatable<CLa<T0, T1>> {
        private readonly IntPtr _arg0;
        private readonly IntPtr _arg1;
        private readonly IntPtr _arg2;
        private readonly IntPtr _arg3;
        private readonly int _arity;
        private readonly void* _fn;

        public CLa(delegate*<T0, T1, void> fn) {
            _fn = fn;
            _arg0 = IntPtr.Zero;
            _arg1 = IntPtr.Zero;
            _arg2 = IntPtr.Zero;
            _arg3 = IntPtr.Zero;
            _arity = 0;
        }

        public CLa(delegate*<T0, T1, IntPtr, void> fn, IntPtr a0) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = IntPtr.Zero;
            _arg2 = IntPtr.Zero;
            _arg3 = IntPtr.Zero;
            _arity = 1;
        }

        public CLa(delegate*<T0, T1, IntPtr, IntPtr, void> fn, IntPtr a0, IntPtr a1) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = a1;
            _arg2 = IntPtr.Zero;
            _arg3 = IntPtr.Zero;
            _arity = 2;
        }

        public CLa(delegate*<T0, T1, IntPtr, IntPtr, IntPtr, void> fn, IntPtr a0, IntPtr a1, IntPtr a2) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = a1;
            _arg2 = a2;
            _arg3 = IntPtr.Zero;
            _arity = 3;
        }

        public CLa(delegate*<T0, T1, IntPtr, IntPtr, IntPtr, IntPtr, void> fn, IntPtr a0, IntPtr a1, IntPtr a2, IntPtr a3) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = a1;
            _arg2 = a2;
            _arg3 = a3;
            _arity = 4;
        }

        public void Invoke(T0 p0, T1 p1) {
            switch (_arity) {
                case 0:
                    ((delegate*<T0, T1, void>)_fn)(p0, p1);
                    break;
                case 1:
                    ((delegate*<T0, T1, void*, void>)_fn)(p0, p1, _arg0.ToPointer());
                    break;
                case 2:
                    ((delegate*<T0, T1, void*, void*, void>)_fn)(p0, p1, _arg0.ToPointer(), _arg1.ToPointer());
                    break;
                case 3:
                    ((delegate*<T0, T1, void*, void*, void*, void>)_fn)(p0, p1, _arg0.ToPointer(), _arg1.ToPointer(), _arg2.ToPointer());
                    break;
                case 4:
                    ((delegate*<T0, T1, void*, void*, void*, void*, void>)_fn)(p0, p1, _arg0.ToPointer(), _arg1.ToPointer(), _arg2.ToPointer(), _arg3.ToPointer());
                    break;
                default:
                    Assert.Fail("Unsupported closure arity");
                    return;
            }
        }

        public void Dispose() {
            if (_arg0 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg0);
            }
            if (_arg1 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg1);
            }
            if (_arg2 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg2);
            }
            if (_arg3 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg3);
            }
        }

        public bool Equals(CLa<T0, T1> other) {
#pragma warning disable CS8909
            return _fn == other._fn && _arity == other._arity && _arg0 == other._arg0 && _arg1 == other._arg1 && _arg2 == other._arg2 && _arg3 == other._arg3;
#pragma warning restore CS8909
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly unsafe struct CLf<T0, T1, TR> : IDisposable, IEquatable<CLf<T0, T1, TR>> {
        private readonly IntPtr _arg0;
        private readonly IntPtr _arg1;
        private readonly IntPtr _arg2;
        private readonly IntPtr _arg3;
        private readonly int _arity;
        private readonly void* _fn;

        public CLf(delegate*<T0, T1, TR> fn) {
            _fn = fn;
            _arg0 = IntPtr.Zero;
            _arg1 = IntPtr.Zero;
            _arg2 = IntPtr.Zero;
            _arg3 = IntPtr.Zero;
            _arity = 0;
        }

        public CLf(delegate*<T0, T1, void*, TR> fn, IntPtr a0) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = IntPtr.Zero;
            _arg2 = IntPtr.Zero;
            _arg3 = IntPtr.Zero;
            _arity = 1;
        }

        public CLf(delegate*<T0, T1, void*, void*, TR> fn, IntPtr a0, IntPtr a1) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = a1;
            _arg2 = IntPtr.Zero;
            _arg3 = IntPtr.Zero;
            _arity = 2;
        }

        public CLf(delegate*<T0, T1, void*, void*, void*, TR> fn, IntPtr a0, IntPtr a1, IntPtr a2) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = a1;
            _arg2 = a2;
            _arg3 = IntPtr.Zero;
            _arity = 3;
        }

        public CLf(delegate*<T0, T1, void*, void*, void*, void*, TR> fn, IntPtr a0, IntPtr a1, IntPtr a2, IntPtr a3) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = a1;
            _arg2 = a2;
            _arg3 = a3;
            _arity = 4;
        }

        public TR Invoke(T0 p0, T1 p1) {
            switch (_arity) {
                case 0:
                    return ((delegate*<T0, T1, TR>)_fn)(p0, p1);
                case 1:
                    return ((delegate*<T0, T1, void*, TR>)_fn)(p0, p1, _arg0.ToPointer());
                case 2:
                    return ((delegate*<T0, T1, void*, void*, TR>)_fn)(p0, p1, _arg0.ToPointer(), _arg1.ToPointer());
                case 3:
                    return ((delegate*<T0, T1, void*, void*, void*, TR>)_fn)(p0, p1, _arg0.ToPointer(), _arg1.ToPointer(), _arg2.ToPointer());
                case 4:
                    return ((delegate*<T0, T1, void*, void*, void*, void*, TR>)_fn)(p0, p1, _arg0.ToPointer(), _arg1.ToPointer(), _arg2.ToPointer(), _arg3.ToPointer());
                default:
                    Assert.Fail("Unsupported closure arity");
                    return default;
            }
        }

        public void Dispose() {
            if (_arg0 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg0);
            }
            if (_arg1 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg1);
            }
            if (_arg2 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg2);
            }
            if (_arg3 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg3);
            }
        }

        public bool Equals(CLf<T0, T1, TR> other) {
#pragma warning disable CS8909
            return _fn == other._fn && _arity == other._arity && _arg0 == other._arg0 && _arg1 == other._arg1 && _arg2 == other._arg2 && _arg3 == other._arg3;
#pragma warning restore CS8909
        }
    }

    public static unsafe partial class CLa {
        public static CLa<T0, T1> New<T0, T1>(delegate*<T0, T1, void> fn) {
            return new CLa<T0, T1>((delegate*<T0, T1, void>)fn);
        }

        public static CLa<T0, T1> New<T0, T1, A0>(delegate*<T0, T1, A0*, void> fn, A0 a0)
                where A0 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            return new CLa<T0, T1>((delegate*<T0, T1, IntPtr, void>)fn, pa0);
        }

        public static CLa<T0, T1> New<T0, T1, A0, A1>(delegate*<T0, T1, A0*, A1*, void> fn, A0 a0, A1 a1)
                where A0 : unmanaged
                where A1 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            var sz1 = Mem.SizeOf<A1>();
            var pa1 = Mem.AllocOuterMemoryAndCopy(&a1, sz1);
            return new CLa<T0, T1>((delegate*<T0, T1, IntPtr, IntPtr, void>)fn, pa0, pa1);
        }

        public static CLa<T0, T1> New<T0, T1, A0, A1, A2>(delegate*<T0, T1, A0*, A1*, A2*, void> fn, A0 a0, A1 a1, A2 a2)
                where A0 : unmanaged
                where A1 : unmanaged
                where A2 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            var sz1 = Mem.SizeOf<A1>();
            var pa1 = Mem.AllocOuterMemoryAndCopy(&a1, sz1);
            var sz2 = Mem.SizeOf<A2>();
            var pa2 = Mem.AllocOuterMemoryAndCopy(&a2, sz2);
            return new CLa<T0, T1>((delegate*<T0, T1, IntPtr, IntPtr, IntPtr, void>)fn, pa0, pa1, pa2);
        }

        public static CLa<T0, T1> New<T0, T1, A0, A1, A2, A3>(delegate*<T0, T1, A0*, A1*, A2*, A3*, void> fn, A0 a0, A1 a1, A2 a2, A3 a3)
                where A0 : unmanaged
                where A1 : unmanaged
                where A2 : unmanaged
                where A3 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            var sz1 = Mem.SizeOf<A1>();
            var pa1 = Mem.AllocOuterMemoryAndCopy(&a1, sz1);
            var sz2 = Mem.SizeOf<A2>();
            var pa2 = Mem.AllocOuterMemoryAndCopy(&a2, sz2);
            var sz3 = Mem.SizeOf<A3>();
            var pa3 = Mem.AllocOuterMemoryAndCopy(&a3, sz3);
            return new CLa<T0, T1>((delegate*<T0, T1, IntPtr, IntPtr, IntPtr, IntPtr, void>)fn, pa0, pa1, pa2, pa3);
        }
    }

    public static unsafe partial class CLf {
        public static CLf<T0, T1, TR> New<T0, T1, TR>(delegate*<T0, T1, TR> fn) {
            return new CLf<T0, T1, TR>((delegate*<T0, T1, TR>)fn);
        }

        public static CLf<T0, T1, TR> New<T0, T1, A0, TR>(delegate*<T0, T1, A0*, TR> fn, A0 a0)
                where A0 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            return new CLf<T0, T1, TR>((delegate*<T0, T1, void*, TR>)fn, pa0);
        }

        public static CLf<T0, T1, TR> New<T0, T1, A0, A1, TR>(delegate*<T0, T1, A0*, A1*, TR> fn, A0 a0, A1 a1)
                where A0 : unmanaged
                where A1 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            var sz1 = Mem.SizeOf<A1>();
            var pa1 = Mem.AllocOuterMemoryAndCopy(&a1, sz1);
            return new CLf<T0, T1, TR>((delegate*<T0, T1, void*, void*, TR>)fn, pa0, pa1);
        }

        public static CLf<T0, T1, TR> New<T0, T1, A0, A1, A2, TR>(delegate*<T0, T1, A0*, A1*, A2*, TR> fn, A0 a0, A1 a1, A2 a2)
                where A0 : unmanaged
                where A1 : unmanaged
                where A2 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            var sz1 = Mem.SizeOf<A1>();
            var pa1 = Mem.AllocOuterMemoryAndCopy(&a1, sz1);
            var sz2 = Mem.SizeOf<A2>();
            var pa2 = Mem.AllocOuterMemoryAndCopy(&a2, sz2);
            return new CLf<T0, T1, TR>((delegate*<T0, T1, void*, void*, void*, TR>)fn, pa0, pa1, pa2);
        }

        public static CLf<T0, T1, TR> New<T0, T1, A0, A1, A2, A3, TR>(delegate*<T0, T1, A0*, A1*, A2*, A3*, TR> fn, A0 a0, A1 a1, A2 a2, A3 a3)
                where A0 : unmanaged
                where A1 : unmanaged
                where A2 : unmanaged
                where A3 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            var sz1 = Mem.SizeOf<A1>();
            var pa1 = Mem.AllocOuterMemoryAndCopy(&a1, sz1);
            var sz2 = Mem.SizeOf<A2>();
            var pa2 = Mem.AllocOuterMemoryAndCopy(&a2, sz2);
            var sz3 = Mem.SizeOf<A3>();
            var pa3 = Mem.AllocOuterMemoryAndCopy(&a3, sz3);
            return new CLf<T0, T1, TR>((delegate*<T0, T1, void*, void*, void*, void*, TR>)fn, pa0, pa1, pa2, pa3);
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly unsafe struct CLa<T0, T1, T2> : IDisposable, IEquatable<CLa<T0, T1, T2>> {
        private readonly IntPtr _arg0;
        private readonly IntPtr _arg1;
        private readonly IntPtr _arg2;
        private readonly IntPtr _arg3;
        private readonly int _arity;
        private readonly void* _fn;

        public CLa(delegate*<T0, T1, T2, void> fn) {
            _fn = fn;
            _arg0 = IntPtr.Zero;
            _arg1 = IntPtr.Zero;
            _arg2 = IntPtr.Zero;
            _arg3 = IntPtr.Zero;
            _arity = 0;
        }

        public CLa(delegate*<T0, T1, T2, IntPtr, void> fn, IntPtr a0) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = IntPtr.Zero;
            _arg2 = IntPtr.Zero;
            _arg3 = IntPtr.Zero;
            _arity = 1;
        }

        public CLa(delegate*<T0, T1, T2, IntPtr, IntPtr, void> fn, IntPtr a0, IntPtr a1) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = a1;
            _arg2 = IntPtr.Zero;
            _arg3 = IntPtr.Zero;
            _arity = 2;
        }

        public CLa(delegate*<T0, T1, T2, IntPtr, IntPtr, IntPtr, void> fn, IntPtr a0, IntPtr a1, IntPtr a2) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = a1;
            _arg2 = a2;
            _arg3 = IntPtr.Zero;
            _arity = 3;
        }

        public CLa(delegate*<T0, T1, T2, IntPtr, IntPtr, IntPtr, IntPtr, void> fn, IntPtr a0, IntPtr a1, IntPtr a2, IntPtr a3) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = a1;
            _arg2 = a2;
            _arg3 = a3;
            _arity = 4;
        }

        public void Invoke(T0 p0, T1 p1, T2 p2) {
            switch (_arity) {
                case 0:
                    ((delegate*<T0, T1, T2, void>)_fn)(p0, p1, p2);
                    break;
                case 1:
                    ((delegate*<T0, T1, T2, void*, void>)_fn)(p0, p1, p2, _arg0.ToPointer());
                    break;
                case 2:
                    ((delegate*<T0, T1, T2, void*, void*, void>)_fn)(p0, p1, p2, _arg0.ToPointer(), _arg1.ToPointer());
                    break;
                case 3:
                    ((delegate*<T0, T1, T2, void*, void*, void*, void>)_fn)(p0, p1, p2, _arg0.ToPointer(), _arg1.ToPointer(), _arg2.ToPointer());
                    break;
                case 4:
                    ((delegate*<T0, T1, T2, void*, void*, void*, void*, void>)_fn)(p0, p1, p2, _arg0.ToPointer(), _arg1.ToPointer(), _arg2.ToPointer(), _arg3.ToPointer());
                    break;
                default:
                    Assert.Fail("Unsupported closure arity");
                    return;
            }
        }

        public void Dispose() {
            if (_arg0 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg0);
            }
            if (_arg1 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg1);
            }
            if (_arg2 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg2);
            }
            if (_arg3 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg3);
            }
        }

        public bool Equals(CLa<T0, T1, T2> other) {
#pragma warning disable CS8909
            return _fn == other._fn && _arity == other._arity && _arg0 == other._arg0 && _arg1 == other._arg1 && _arg2 == other._arg2 && _arg3 == other._arg3;
#pragma warning restore CS8909
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly unsafe struct CLf<T0, T1, T2, TR> : IDisposable, IEquatable<CLf<T0, T1, T2, TR>> {
        private readonly IntPtr _arg0;
        private readonly IntPtr _arg1;
        private readonly IntPtr _arg2;
        private readonly IntPtr _arg3;
        private readonly int _arity;
        private readonly void* _fn;

        public CLf(delegate*<T0, T1, T2, TR> fn) {
            _fn = fn;
            _arg0 = IntPtr.Zero;
            _arg1 = IntPtr.Zero;
            _arg2 = IntPtr.Zero;
            _arg3 = IntPtr.Zero;
            _arity = 0;
        }

        public CLf(delegate*<T0, T1, T2, void*, TR> fn, IntPtr a0) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = IntPtr.Zero;
            _arg2 = IntPtr.Zero;
            _arg3 = IntPtr.Zero;
            _arity = 1;
        }

        public CLf(delegate*<T0, T1, T2, void*, void*, TR> fn, IntPtr a0, IntPtr a1) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = a1;
            _arg2 = IntPtr.Zero;
            _arg3 = IntPtr.Zero;
            _arity = 2;
        }

        public CLf(delegate*<T0, T1, T2, void*, void*, void*, TR> fn, IntPtr a0, IntPtr a1, IntPtr a2) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = a1;
            _arg2 = a2;
            _arg3 = IntPtr.Zero;
            _arity = 3;
        }

        public CLf(delegate*<T0, T1, T2, void*, void*, void*, void*, TR> fn, IntPtr a0, IntPtr a1, IntPtr a2, IntPtr a3) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = a1;
            _arg2 = a2;
            _arg3 = a3;
            _arity = 4;
        }

        public TR Invoke(T0 p0, T1 p1, T2 p2) {
            switch (_arity) {
                case 0:
                    return ((delegate*<T0, T1, T2, TR>)_fn)(p0, p1, p2);
                case 1:
                    return ((delegate*<T0, T1, T2, void*, TR>)_fn)(p0, p1, p2, _arg0.ToPointer());
                case 2:
                    return ((delegate*<T0, T1, T2, void*, void*, TR>)_fn)(p0, p1, p2, _arg0.ToPointer(), _arg1.ToPointer());
                case 3:
                    return ((delegate*<T0, T1, T2, void*, void*, void*, TR>)_fn)(p0, p1, p2, _arg0.ToPointer(), _arg1.ToPointer(), _arg2.ToPointer());
                case 4:
                    return ((delegate*<T0, T1, T2, void*, void*, void*, void*, TR>)_fn)(p0, p1, p2, _arg0.ToPointer(), _arg1.ToPointer(), _arg2.ToPointer(), _arg3.ToPointer());
                default:
                    Assert.Fail("Unsupported closure arity");
                    return default;
            }
        }

        public void Dispose() {
            if (_arg0 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg0);
            }
            if (_arg1 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg1);
            }
            if (_arg2 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg2);
            }
            if (_arg3 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg3);
            }
        }

        public bool Equals(CLf<T0, T1, T2, TR> other) {
#pragma warning disable CS8909
            return _fn == other._fn && _arity == other._arity && _arg0 == other._arg0 && _arg1 == other._arg1 && _arg2 == other._arg2 && _arg3 == other._arg3;
#pragma warning restore CS8909
        }
    }

    public static unsafe partial class CLa {
        public static CLa<T0, T1, T2> New<T0, T1, T2>(delegate*<T0, T1, T2, void> fn) {
            return new CLa<T0, T1, T2>((delegate*<T0, T1, T2, void>)fn);
        }

        public static CLa<T0, T1, T2> New<T0, T1, T2, A0>(delegate*<T0, T1, T2, A0*, void> fn, A0 a0)
                where A0 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            return new CLa<T0, T1, T2>((delegate*<T0, T1, T2, IntPtr, void>)fn, pa0);
        }

        public static CLa<T0, T1, T2> New<T0, T1, T2, A0, A1>(delegate*<T0, T1, T2, A0*, A1*, void> fn, A0 a0, A1 a1)
                where A0 : unmanaged
                where A1 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            var sz1 = Mem.SizeOf<A1>();
            var pa1 = Mem.AllocOuterMemoryAndCopy(&a1, sz1);
            return new CLa<T0, T1, T2>((delegate*<T0, T1, T2, IntPtr, IntPtr, void>)fn, pa0, pa1);
        }

        public static CLa<T0, T1, T2> New<T0, T1, T2, A0, A1, A2>(delegate*<T0, T1, T2, A0*, A1*, A2*, void> fn, A0 a0, A1 a1, A2 a2)
                where A0 : unmanaged
                where A1 : unmanaged
                where A2 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            var sz1 = Mem.SizeOf<A1>();
            var pa1 = Mem.AllocOuterMemoryAndCopy(&a1, sz1);
            var sz2 = Mem.SizeOf<A2>();
            var pa2 = Mem.AllocOuterMemoryAndCopy(&a2, sz2);
            return new CLa<T0, T1, T2>((delegate*<T0, T1, T2, IntPtr, IntPtr, IntPtr, void>)fn, pa0, pa1, pa2);
        }

        public static CLa<T0, T1, T2> New<T0, T1, T2, A0, A1, A2, A3>(delegate*<T0, T1, T2, A0*, A1*, A2*, A3*, void> fn, A0 a0, A1 a1, A2 a2, A3 a3)
                where A0 : unmanaged
                where A1 : unmanaged
                where A2 : unmanaged
                where A3 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            var sz1 = Mem.SizeOf<A1>();
            var pa1 = Mem.AllocOuterMemoryAndCopy(&a1, sz1);
            var sz2 = Mem.SizeOf<A2>();
            var pa2 = Mem.AllocOuterMemoryAndCopy(&a2, sz2);
            var sz3 = Mem.SizeOf<A3>();
            var pa3 = Mem.AllocOuterMemoryAndCopy(&a3, sz3);
            return new CLa<T0, T1, T2>((delegate*<T0, T1, T2, IntPtr, IntPtr, IntPtr, IntPtr, void>)fn, pa0, pa1, pa2, pa3);
        }
    }

    public static unsafe partial class CLf {
        public static CLf<T0, T1, T2, TR> New<T0, T1, T2, TR>(delegate*<T0, T1, T2, TR> fn) {
            return new CLf<T0, T1, T2, TR>((delegate*<T0, T1, T2, TR>)fn);
        }

        public static CLf<T0, T1, T2, TR> New<T0, T1, T2, A0, TR>(delegate*<T0, T1, T2, A0*, TR> fn, A0 a0)
                where A0 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            return new CLf<T0, T1, T2, TR>((delegate*<T0, T1, T2, void*, TR>)fn, pa0);
        }

        public static CLf<T0, T1, T2, TR> New<T0, T1, T2, A0, A1, TR>(delegate*<T0, T1, T2, A0*, A1*, TR> fn, A0 a0, A1 a1)
                where A0 : unmanaged
                where A1 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            var sz1 = Mem.SizeOf<A1>();
            var pa1 = Mem.AllocOuterMemoryAndCopy(&a1, sz1);
            return new CLf<T0, T1, T2, TR>((delegate*<T0, T1, T2, void*, void*, TR>)fn, pa0, pa1);
        }

        public static CLf<T0, T1, T2, TR> New<T0, T1, T2, A0, A1, A2, TR>(delegate*<T0, T1, T2, A0*, A1*, A2*, TR> fn, A0 a0, A1 a1, A2 a2)
                where A0 : unmanaged
                where A1 : unmanaged
                where A2 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            var sz1 = Mem.SizeOf<A1>();
            var pa1 = Mem.AllocOuterMemoryAndCopy(&a1, sz1);
            var sz2 = Mem.SizeOf<A2>();
            var pa2 = Mem.AllocOuterMemoryAndCopy(&a2, sz2);
            return new CLf<T0, T1, T2, TR>((delegate*<T0, T1, T2, void*, void*, void*, TR>)fn, pa0, pa1, pa2);
        }

        public static CLf<T0, T1, T2, TR> New<T0, T1, T2, A0, A1, A2, A3, TR>(delegate*<T0, T1, T2, A0*, A1*, A2*, A3*, TR> fn, A0 a0, A1 a1, A2 a2, A3 a3)
                where A0 : unmanaged
                where A1 : unmanaged
                where A2 : unmanaged
                where A3 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            var sz1 = Mem.SizeOf<A1>();
            var pa1 = Mem.AllocOuterMemoryAndCopy(&a1, sz1);
            var sz2 = Mem.SizeOf<A2>();
            var pa2 = Mem.AllocOuterMemoryAndCopy(&a2, sz2);
            var sz3 = Mem.SizeOf<A3>();
            var pa3 = Mem.AllocOuterMemoryAndCopy(&a3, sz3);
            return new CLf<T0, T1, T2, TR>((delegate*<T0, T1, T2, void*, void*, void*, void*, TR>)fn, pa0, pa1, pa2, pa3);
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly unsafe struct CLa<T0, T1, T2, T3> : IDisposable, IEquatable<CLa<T0, T1, T2, T3>> {
        private readonly IntPtr _arg0;
        private readonly IntPtr _arg1;
        private readonly IntPtr _arg2;
        private readonly IntPtr _arg3;
        private readonly int _arity;
        private readonly void* _fn;

        public CLa(delegate*<T0, T1, T2, T3, void> fn) {
            _fn = fn;
            _arg0 = IntPtr.Zero;
            _arg1 = IntPtr.Zero;
            _arg2 = IntPtr.Zero;
            _arg3 = IntPtr.Zero;
            _arity = 0;
        }

        public CLa(delegate*<T0, T1, T2, T3, IntPtr, void> fn, IntPtr a0) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = IntPtr.Zero;
            _arg2 = IntPtr.Zero;
            _arg3 = IntPtr.Zero;
            _arity = 1;
        }

        public CLa(delegate*<T0, T1, T2, T3, IntPtr, IntPtr, void> fn, IntPtr a0, IntPtr a1) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = a1;
            _arg2 = IntPtr.Zero;
            _arg3 = IntPtr.Zero;
            _arity = 2;
        }

        public CLa(delegate*<T0, T1, T2, T3, IntPtr, IntPtr, IntPtr, void> fn, IntPtr a0, IntPtr a1, IntPtr a2) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = a1;
            _arg2 = a2;
            _arg3 = IntPtr.Zero;
            _arity = 3;
        }

        public CLa(delegate*<T0, T1, T2, T3, IntPtr, IntPtr, IntPtr, IntPtr, void> fn, IntPtr a0, IntPtr a1, IntPtr a2, IntPtr a3) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = a1;
            _arg2 = a2;
            _arg3 = a3;
            _arity = 4;
        }

        public void Invoke(T0 p0, T1 p1, T2 p2, T3 p3) {
            switch (_arity) {
                case 0:
                    ((delegate*<T0, T1, T2, T3, void>)_fn)(p0, p1, p2, p3);
                    break;
                case 1:
                    ((delegate*<T0, T1, T2, T3, void*, void>)_fn)(p0, p1, p2, p3, _arg0.ToPointer());
                    break;
                case 2:
                    ((delegate*<T0, T1, T2, T3, void*, void*, void>)_fn)(p0, p1, p2, p3, _arg0.ToPointer(), _arg1.ToPointer());
                    break;
                case 3:
                    ((delegate*<T0, T1, T2, T3, void*, void*, void*, void>)_fn)(p0, p1, p2, p3, _arg0.ToPointer(), _arg1.ToPointer(), _arg2.ToPointer());
                    break;
                case 4:
                    ((delegate*<T0, T1, T2, T3, void*, void*, void*, void*, void>)_fn)(p0, p1, p2, p3, _arg0.ToPointer(), _arg1.ToPointer(), _arg2.ToPointer(), _arg3.ToPointer());
                    break;
                default:
                    Assert.Fail("Unsupported closure arity");
                    return;
            }
        }

        public void Dispose() {
            if (_arg0 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg0);
            }
            if (_arg1 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg1);
            }
            if (_arg2 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg2);
            }
            if (_arg3 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg3);
            }
        }

        public bool Equals(CLa<T0, T1, T2, T3> other) {
#pragma warning disable CS8909
            return _fn == other._fn && _arity == other._arity && _arg0 == other._arg0 && _arg1 == other._arg1 && _arg2 == other._arg2 && _arg3 == other._arg3;
#pragma warning restore CS8909
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly unsafe struct CLf<T0, T1, T2, T3, TR> : IDisposable, IEquatable<CLf<T0, T1, T2, T3, TR>> {
        private readonly IntPtr _arg0;
        private readonly IntPtr _arg1;
        private readonly IntPtr _arg2;
        private readonly IntPtr _arg3;
        private readonly int _arity;
        private readonly void* _fn;

        public CLf(delegate*<T0, T1, T2, T3, TR> fn) {
            _fn = fn;
            _arg0 = IntPtr.Zero;
            _arg1 = IntPtr.Zero;
            _arg2 = IntPtr.Zero;
            _arg3 = IntPtr.Zero;
            _arity = 0;
        }

        public CLf(delegate*<T0, T1, T2, T3, void*, TR> fn, IntPtr a0) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = IntPtr.Zero;
            _arg2 = IntPtr.Zero;
            _arg3 = IntPtr.Zero;
            _arity = 1;
        }

        public CLf(delegate*<T0, T1, T2, T3, void*, void*, TR> fn, IntPtr a0, IntPtr a1) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = a1;
            _arg2 = IntPtr.Zero;
            _arg3 = IntPtr.Zero;
            _arity = 2;
        }

        public CLf(delegate*<T0, T1, T2, T3, void*, void*, void*, TR> fn, IntPtr a0, IntPtr a1, IntPtr a2) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = a1;
            _arg2 = a2;
            _arg3 = IntPtr.Zero;
            _arity = 3;
        }

        public CLf(delegate*<T0, T1, T2, T3, void*, void*, void*, void*, TR> fn, IntPtr a0, IntPtr a1, IntPtr a2, IntPtr a3) {
            _fn = fn;
            _arg0 = a0;
            _arg1 = a1;
            _arg2 = a2;
            _arg3 = a3;
            _arity = 4;
        }

        public TR Invoke(T0 p0, T1 p1, T2 p2, T3 p3) {
            switch (_arity) {
                case 0:
                    return ((delegate*<T0, T1, T2, T3, TR>)_fn)(p0, p1, p2, p3);
                case 1:
                    return ((delegate*<T0, T1, T2, T3, void*, TR>)_fn)(p0, p1, p2, p3, _arg0.ToPointer());
                case 2:
                    return ((delegate*<T0, T1, T2, T3, void*, void*, TR>)_fn)(p0, p1, p2, p3, _arg0.ToPointer(), _arg1.ToPointer());
                case 3:
                    return ((delegate*<T0, T1, T2, T3, void*, void*, void*, TR>)_fn)(p0, p1, p2, p3, _arg0.ToPointer(), _arg1.ToPointer(), _arg2.ToPointer());
                case 4:
                    return ((delegate*<T0, T1, T2, T3, void*, void*, void*, void*, TR>)_fn)(p0, p1, p2, p3, _arg0.ToPointer(), _arg1.ToPointer(), _arg2.ToPointer(), _arg3.ToPointer());
                default:
                    Assert.Fail("Unsupported closure arity");
                    return default;
            }
        }

        public void Dispose() {
            if (_arg0 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg0);
            }
            if (_arg1 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg1);
            }
            if (_arg2 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg2);
            }
            if (_arg3 != IntPtr.Zero) {
                Mem.FreeOuterMemory(_arg3);
            }
        }

        public bool Equals(CLf<T0, T1, T2, T3, TR> other) {
#pragma warning disable CS8909
            return _fn == other._fn && _arity == other._arity && _arg0 == other._arg0 && _arg1 == other._arg1 && _arg2 == other._arg2 && _arg3 == other._arg3;
#pragma warning restore CS8909
        }
    }

    public static unsafe partial class CLa {
        public static CLa<T0, T1, T2, T3> New<T0, T1, T2, T3>(delegate*<T0, T1, T2, T3, void> fn) {
            return new CLa<T0, T1, T2, T3>((delegate*<T0, T1, T2, T3, void>)fn);
        }

        public static CLa<T0, T1, T2, T3> New<T0, T1, T2, T3, A0>(delegate*<T0, T1, T2, T3, A0*, void> fn, A0 a0)
                where A0 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            return new CLa<T0, T1, T2, T3>((delegate*<T0, T1, T2, T3, IntPtr, void>)fn, pa0);
        }

        public static CLa<T0, T1, T2, T3> New<T0, T1, T2, T3, A0, A1>(delegate*<T0, T1, T2, T3, A0*, A1*, void> fn, A0 a0, A1 a1)
                where A0 : unmanaged
                where A1 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            var sz1 = Mem.SizeOf<A1>();
            var pa1 = Mem.AllocOuterMemoryAndCopy(&a1, sz1);
            return new CLa<T0, T1, T2, T3>((delegate*<T0, T1, T2, T3, IntPtr, IntPtr, void>)fn, pa0, pa1);
        }

        public static CLa<T0, T1, T2, T3> New<T0, T1, T2, T3, A0, A1, A2>(delegate*<T0, T1, T2, T3, A0*, A1*, A2*, void> fn, A0 a0, A1 a1, A2 a2)
                where A0 : unmanaged
                where A1 : unmanaged
                where A2 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            var sz1 = Mem.SizeOf<A1>();
            var pa1 = Mem.AllocOuterMemoryAndCopy(&a1, sz1);
            var sz2 = Mem.SizeOf<A2>();
            var pa2 = Mem.AllocOuterMemoryAndCopy(&a2, sz2);
            return new CLa<T0, T1, T2, T3>((delegate*<T0, T1, T2, T3, IntPtr, IntPtr, IntPtr, void>)fn, pa0, pa1, pa2);
        }

        public static CLa<T0, T1, T2, T3> New<T0, T1, T2, T3, A0, A1, A2, A3>(delegate*<T0, T1, T2, T3, A0*, A1*, A2*, A3*, void> fn, A0 a0, A1 a1, A2 a2, A3 a3)
                where A0 : unmanaged
                where A1 : unmanaged
                where A2 : unmanaged
                where A3 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            var sz1 = Mem.SizeOf<A1>();
            var pa1 = Mem.AllocOuterMemoryAndCopy(&a1, sz1);
            var sz2 = Mem.SizeOf<A2>();
            var pa2 = Mem.AllocOuterMemoryAndCopy(&a2, sz2);
            var sz3 = Mem.SizeOf<A3>();
            var pa3 = Mem.AllocOuterMemoryAndCopy(&a3, sz3);
            return new CLa<T0, T1, T2, T3>((delegate*<T0, T1, T2, T3, IntPtr, IntPtr, IntPtr, IntPtr, void>)fn, pa0, pa1, pa2, pa3);
        }
    }

    public static unsafe partial class CLf {
        public static CLf<T0, T1, T2, T3, TR> New<T0, T1, T2, T3, TR>(delegate*<T0, T1, T2, T3, TR> fn) {
            return new CLf<T0, T1, T2, T3, TR>((delegate*<T0, T1, T2, T3, TR>)fn);
        }

        public static CLf<T0, T1, T2, T3, TR> New<T0, T1, T2, T3, A0, TR>(delegate*<T0, T1, T2, T3, A0*, TR> fn, A0 a0)
                where A0 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            return new CLf<T0, T1, T2, T3, TR>((delegate*<T0, T1, T2, T3, void*, TR>)fn, pa0);
        }

        public static CLf<T0, T1, T2, T3, TR> New<T0, T1, T2, T3, A0, A1, TR>(delegate*<T0, T1, T2, T3, A0*, A1*, TR> fn, A0 a0, A1 a1)
                where A0 : unmanaged
                where A1 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            var sz1 = Mem.SizeOf<A1>();
            var pa1 = Mem.AllocOuterMemoryAndCopy(&a1, sz1);
            return new CLf<T0, T1, T2, T3, TR>((delegate*<T0, T1, T2, T3, void*, void*, TR>)fn, pa0, pa1);
        }

        public static CLf<T0, T1, T2, T3, TR> New<T0, T1, T2, T3, A0, A1, A2, TR>(delegate*<T0, T1, T2, T3, A0*, A1*, A2*, TR> fn, A0 a0, A1 a1, A2 a2)
                where A0 : unmanaged
                where A1 : unmanaged
                where A2 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            var sz1 = Mem.SizeOf<A1>();
            var pa1 = Mem.AllocOuterMemoryAndCopy(&a1, sz1);
            var sz2 = Mem.SizeOf<A2>();
            var pa2 = Mem.AllocOuterMemoryAndCopy(&a2, sz2);
            return new CLf<T0, T1, T2, T3, TR>((delegate*<T0, T1, T2, T3, void*, void*, void*, TR>)fn, pa0, pa1, pa2);
        }

        public static CLf<T0, T1, T2, T3, TR> New<T0, T1, T2, T3, A0, A1, A2, A3, TR>(delegate*<T0, T1, T2, T3, A0*, A1*, A2*, A3*, TR> fn, A0 a0, A1 a1, A2 a2, A3 a3)
                where A0 : unmanaged
                where A1 : unmanaged
                where A2 : unmanaged
                where A3 : unmanaged {
            var sz0 = Mem.SizeOf<A0>();
            var pa0 = Mem.AllocOuterMemoryAndCopy(&a0, sz0);
            var sz1 = Mem.SizeOf<A1>();
            var pa1 = Mem.AllocOuterMemoryAndCopy(&a1, sz1);
            var sz2 = Mem.SizeOf<A2>();
            var pa2 = Mem.AllocOuterMemoryAndCopy(&a2, sz2);
            var sz3 = Mem.SizeOf<A3>();
            var pa3 = Mem.AllocOuterMemoryAndCopy(&a3, sz3);
            return new CLf<T0, T1, T2, T3, TR>((delegate*<T0, T1, T2, T3, void*, void*, void*, void*, TR>)fn, pa0, pa1, pa2, pa3);
        }
    }
}