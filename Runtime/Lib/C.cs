
//DO NOT MODIFY. FILE IS GENERATED
using System.Runtime.InteropServices;
using Rondo.Core.Extras;
using Rondo.Core.Memory;

namespace Rondo.Core.Lib {
    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct Cf<TR> {
       private readonly void* _fn;
       private readonly int _arity;
       private readonly void* _arg0;
       private readonly void* _arg1;
       private readonly void* _arg2;

       public Cf(delegate*<TR> fn) {
           _fn = fn;
           _arg0 = null;
           _arg1 = null;
           _arg2 = null;
           _arity = 0;
       }

       public Cf(delegate*<void*, TR> fn, void* a0) {
           _fn = fn;
           _arg0 = a0;
           _arg1 = null;
           _arg2 = null;
           _arity = 1;
       }

       public Cf(delegate*<void*, void*, TR> fn, void* a0, void* a1) {
           _fn = fn;
           _arg0 = a0;
           _arg1 = a1;
           _arg2 = null;
           _arity = 2;
       }

       public Cf(delegate*<void*, void*, void*, TR> fn, void* a0, void* a1, void* a2) {
           _fn = fn;
           _arg0 = a0;
           _arg1 = a1;
           _arg2 = a2;
           _arity = 3;
       }

       public TR Invoke() {
            switch (_arity) {
            case 0:
                return ((delegate*<TR>)_fn)();
            case 1:
                return ((delegate*<void*, TR>)_fn)(_arg0);
            case 2:
                return ((delegate*<void*, void*, TR>)_fn)(_arg0, _arg1);
            case 3:
                return ((delegate*<void*, void*, void*, TR>)_fn)(_arg0, _arg1, _arg2);
            default:
                Assert.Fail("Unsupported closure arity");
                return default;
            }
        }
    }

    public static unsafe partial class Cf {
                public static Cf<TR> New<TR>(delegate*<TR> fn) {
            return new Cf<TR>((delegate*<TR>)fn);
        }

        public static Cf<TR> New<A0, TR>(delegate*<A0*, TR> fn,A0 a0)
                where A0: unmanaged {
            var pa0 = Mem.C.Copy(a0);
            return new Cf<TR>((delegate*<void*, TR>)fn, pa0);
        }

        public static Cf<TR> New<A0, A1, TR>(delegate*<A0*, A1*, TR> fn,A0 a0, A1 a1)
                where A0: unmanaged
                where A1: unmanaged {
            var pa0 = Mem.C.Copy(a0);
            var pa1 = Mem.C.Copy(a1);
            return new Cf<TR>((delegate*<void*, void*, TR>)fn, pa0, pa1);
        }

        public static Cf<TR> New<A0, A1, A2, TR>(delegate*<A0*, A1*, A2*, TR> fn,A0 a0, A1 a1, A2 a2)
                where A0: unmanaged
                where A1: unmanaged
                where A2: unmanaged {
            var pa0 = Mem.C.Copy(a0);
            var pa1 = Mem.C.Copy(a1);
            var pa2 = Mem.C.Copy(a2);
            return new Cf<TR>((delegate*<void*, void*, void*, TR>)fn, pa0, pa1, pa2);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct Ca<T0> {
       private readonly void* _fn;
       private readonly int _arity;
       private readonly void* _arg0;
       private readonly void* _arg1;
       private readonly void* _arg2;

       public Ca(delegate*<T0, void> fn) {
           _fn = fn;
           _arg0 = null;
           _arg1 = null;
           _arg2 = null;
           _arity = 0;
       }

       public Ca(delegate*<T0, void*, void> fn, void* a0) {
           _fn = fn;
           _arg0 = a0;
           _arg1 = null;
           _arg2 = null;
           _arity = 1;
       }

       public Ca(delegate*<T0, void*, void*, void> fn, void* a0, void* a1) {
           _fn = fn;
           _arg0 = a0;
           _arg1 = a1;
           _arg2 = null;
           _arity = 2;
       }

       public Ca(delegate*<T0, void*, void*, void*, void> fn, void* a0, void* a1, void* a2) {
           _fn = fn;
           _arg0 = a0;
           _arg1 = a1;
           _arg2 = a2;
           _arity = 3;
       }

       public void Invoke(T0 p0) {
            switch (_arity) {
            case 0:
                ((delegate*<T0, void>)_fn)(p0);
                break;
            case 1:
                ((delegate*<T0, void*, void>)_fn)(p0, _arg0);
                break;
            case 2:
                ((delegate*<T0, void*, void*, void>)_fn)(p0, _arg0, _arg1);
                break;
            case 3:
                ((delegate*<T0, void*, void*, void*, void>)_fn)(p0, _arg0, _arg1, _arg2);
                break;
            default:
                Assert.Fail("Unsupported closure arity");
                return;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct Cf<T0, TR> {
       private readonly void* _fn;
       private readonly int _arity;
       private readonly void* _arg0;
       private readonly void* _arg1;
       private readonly void* _arg2;

       public Cf(delegate*<T0, TR> fn) {
           _fn = fn;
           _arg0 = null;
           _arg1 = null;
           _arg2 = null;
           _arity = 0;
       }

       public Cf(delegate*<T0, void*, TR> fn, void* a0) {
           _fn = fn;
           _arg0 = a0;
           _arg1 = null;
           _arg2 = null;
           _arity = 1;
       }

       public Cf(delegate*<T0, void*, void*, TR> fn, void* a0, void* a1) {
           _fn = fn;
           _arg0 = a0;
           _arg1 = a1;
           _arg2 = null;
           _arity = 2;
       }

       public Cf(delegate*<T0, void*, void*, void*, TR> fn, void* a0, void* a1, void* a2) {
           _fn = fn;
           _arg0 = a0;
           _arg1 = a1;
           _arg2 = a2;
           _arity = 3;
       }

       public TR Invoke(T0 p0) {
            switch (_arity) {
            case 0:
                return ((delegate*<T0, TR>)_fn)(p0);
            case 1:
                return ((delegate*<T0, void*, TR>)_fn)(p0, _arg0);
            case 2:
                return ((delegate*<T0, void*, void*, TR>)_fn)(p0, _arg0, _arg1);
            case 3:
                return ((delegate*<T0, void*, void*, void*, TR>)_fn)(p0, _arg0, _arg1, _arg2);
            default:
                Assert.Fail("Unsupported closure arity");
                return default;
            }
        }
    }

    public static unsafe partial class Ca {
                public static Ca<T0> New<T0>(delegate*<T0, void> fn) {
            return new Ca<T0>((delegate*<T0, void>)fn);
        }

        public static Ca<T0> New<T0, A0>(delegate*<T0, A0*, void> fn,A0 a0)
                where A0: unmanaged {
            var pa0 = Mem.C.Copy(a0);
            return new Ca<T0>((delegate*<T0, void*, void>)fn, pa0);
        }

        public static Ca<T0> New<T0, A0, A1>(delegate*<T0, A0*, A1*, void> fn,A0 a0, A1 a1)
                where A0: unmanaged
                where A1: unmanaged {
            var pa0 = Mem.C.Copy(a0);
            var pa1 = Mem.C.Copy(a1);
            return new Ca<T0>((delegate*<T0, void*, void*, void>)fn, pa0, pa1);
        }

        public static Ca<T0> New<T0, A0, A1, A2>(delegate*<T0, A0*, A1*, A2*, void> fn,A0 a0, A1 a1, A2 a2)
                where A0: unmanaged
                where A1: unmanaged
                where A2: unmanaged {
            var pa0 = Mem.C.Copy(a0);
            var pa1 = Mem.C.Copy(a1);
            var pa2 = Mem.C.Copy(a2);
            return new Ca<T0>((delegate*<T0, void*, void*, void*, void>)fn, pa0, pa1, pa2);
        }
    }

    public static unsafe partial class Cf {
                public static Cf<T0,TR> New<T0, TR>(delegate*<T0, TR> fn) {
            return new Cf<T0, TR>((delegate*<T0, TR>)fn);
        }

        public static Cf<T0,TR> New<T0, A0, TR>(delegate*<T0, A0*, TR> fn,A0 a0)
                where A0: unmanaged {
            var pa0 = Mem.C.Copy(a0);
            return new Cf<T0, TR>((delegate*<T0, void*, TR>)fn, pa0);
        }

        public static Cf<T0,TR> New<T0, A0, A1, TR>(delegate*<T0, A0*, A1*, TR> fn,A0 a0, A1 a1)
                where A0: unmanaged
                where A1: unmanaged {
            var pa0 = Mem.C.Copy(a0);
            var pa1 = Mem.C.Copy(a1);
            return new Cf<T0, TR>((delegate*<T0, void*, void*, TR>)fn, pa0, pa1);
        }

        public static Cf<T0,TR> New<T0, A0, A1, A2, TR>(delegate*<T0, A0*, A1*, A2*, TR> fn,A0 a0, A1 a1, A2 a2)
                where A0: unmanaged
                where A1: unmanaged
                where A2: unmanaged {
            var pa0 = Mem.C.Copy(a0);
            var pa1 = Mem.C.Copy(a1);
            var pa2 = Mem.C.Copy(a2);
            return new Cf<T0, TR>((delegate*<T0, void*, void*, void*, TR>)fn, pa0, pa1, pa2);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct Ca<T0, T1> {
       private readonly void* _fn;
       private readonly int _arity;
       private readonly void* _arg0;
       private readonly void* _arg1;
       private readonly void* _arg2;

       public Ca(delegate*<T0,T1, void> fn) {
           _fn = fn;
           _arg0 = null;
           _arg1 = null;
           _arg2 = null;
           _arity = 0;
       }

       public Ca(delegate*<T0,T1, void*, void> fn, void* a0) {
           _fn = fn;
           _arg0 = a0;
           _arg1 = null;
           _arg2 = null;
           _arity = 1;
       }

       public Ca(delegate*<T0,T1, void*, void*, void> fn, void* a0, void* a1) {
           _fn = fn;
           _arg0 = a0;
           _arg1 = a1;
           _arg2 = null;
           _arity = 2;
       }

       public Ca(delegate*<T0,T1, void*, void*, void*, void> fn, void* a0, void* a1, void* a2) {
           _fn = fn;
           _arg0 = a0;
           _arg1 = a1;
           _arg2 = a2;
           _arity = 3;
       }

       public void Invoke(T0 p0, T1 p1) {
            switch (_arity) {
            case 0:
                ((delegate*<T0,T1, void>)_fn)(p0,p1);
                break;
            case 1:
                ((delegate*<T0,T1, void*, void>)_fn)(p0,p1, _arg0);
                break;
            case 2:
                ((delegate*<T0,T1, void*, void*, void>)_fn)(p0,p1, _arg0, _arg1);
                break;
            case 3:
                ((delegate*<T0,T1, void*, void*, void*, void>)_fn)(p0,p1, _arg0, _arg1, _arg2);
                break;
            default:
                Assert.Fail("Unsupported closure arity");
                return;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct Cf<T0, T1, TR> {
       private readonly void* _fn;
       private readonly int _arity;
       private readonly void* _arg0;
       private readonly void* _arg1;
       private readonly void* _arg2;

       public Cf(delegate*<T0,T1, TR> fn) {
           _fn = fn;
           _arg0 = null;
           _arg1 = null;
           _arg2 = null;
           _arity = 0;
       }

       public Cf(delegate*<T0,T1, void*, TR> fn, void* a0) {
           _fn = fn;
           _arg0 = a0;
           _arg1 = null;
           _arg2 = null;
           _arity = 1;
       }

       public Cf(delegate*<T0,T1, void*, void*, TR> fn, void* a0, void* a1) {
           _fn = fn;
           _arg0 = a0;
           _arg1 = a1;
           _arg2 = null;
           _arity = 2;
       }

       public Cf(delegate*<T0,T1, void*, void*, void*, TR> fn, void* a0, void* a1, void* a2) {
           _fn = fn;
           _arg0 = a0;
           _arg1 = a1;
           _arg2 = a2;
           _arity = 3;
       }

       public TR Invoke(T0 p0, T1 p1) {
            switch (_arity) {
            case 0:
                return ((delegate*<T0, T1, TR>)_fn)(p0, p1);
            case 1:
                return ((delegate*<T0, T1, void*, TR>)_fn)(p0, p1, _arg0);
            case 2:
                return ((delegate*<T0, T1, void*, void*, TR>)_fn)(p0, p1, _arg0, _arg1);
            case 3:
                return ((delegate*<T0, T1, void*, void*, void*, TR>)_fn)(p0, p1, _arg0, _arg1, _arg2);
            default:
                Assert.Fail("Unsupported closure arity");
                return default;
            }
        }
    }

    public static unsafe partial class Ca {
                public static Ca<T0, T1> New<T0, T1>(delegate*<T0, T1, void> fn) {
            return new Ca<T0, T1>((delegate*<T0, T1, void>)fn);
        }

        public static Ca<T0, T1> New<T0, T1, A0>(delegate*<T0, T1, A0*, void> fn,A0 a0)
                where A0: unmanaged {
            var pa0 = Mem.C.Copy(a0);
            return new Ca<T0, T1>((delegate*<T0, T1, void*, void>)fn, pa0);
        }

        public static Ca<T0, T1> New<T0, T1, A0, A1>(delegate*<T0, T1, A0*, A1*, void> fn,A0 a0, A1 a1)
                where A0: unmanaged
                where A1: unmanaged {
            var pa0 = Mem.C.Copy(a0);
            var pa1 = Mem.C.Copy(a1);
            return new Ca<T0, T1>((delegate*<T0, T1, void*, void*, void>)fn, pa0, pa1);
        }

        public static Ca<T0, T1> New<T0, T1, A0, A1, A2>(delegate*<T0, T1, A0*, A1*, A2*, void> fn,A0 a0, A1 a1, A2 a2)
                where A0: unmanaged
                where A1: unmanaged
                where A2: unmanaged {
            var pa0 = Mem.C.Copy(a0);
            var pa1 = Mem.C.Copy(a1);
            var pa2 = Mem.C.Copy(a2);
            return new Ca<T0, T1>((delegate*<T0, T1, void*, void*, void*, void>)fn, pa0, pa1, pa2);
        }
    }

    public static unsafe partial class Cf {
                public static Cf<T0, T1,TR> New<T0, T1, TR>(delegate*<T0, T1, TR> fn) {
            return new Cf<T0, T1, TR>((delegate*<T0, T1, TR>)fn);
        }

        public static Cf<T0, T1,TR> New<T0, T1, A0, TR>(delegate*<T0, T1, A0*, TR> fn,A0 a0)
                where A0: unmanaged {
            var pa0 = Mem.C.Copy(a0);
            return new Cf<T0, T1, TR>((delegate*<T0, T1, void*, TR>)fn, pa0);
        }

        public static Cf<T0, T1,TR> New<T0, T1, A0, A1, TR>(delegate*<T0, T1, A0*, A1*, TR> fn,A0 a0, A1 a1)
                where A0: unmanaged
                where A1: unmanaged {
            var pa0 = Mem.C.Copy(a0);
            var pa1 = Mem.C.Copy(a1);
            return new Cf<T0, T1, TR>((delegate*<T0, T1, void*, void*, TR>)fn, pa0, pa1);
        }

        public static Cf<T0, T1,TR> New<T0, T1, A0, A1, A2, TR>(delegate*<T0, T1, A0*, A1*, A2*, TR> fn,A0 a0, A1 a1, A2 a2)
                where A0: unmanaged
                where A1: unmanaged
                where A2: unmanaged {
            var pa0 = Mem.C.Copy(a0);
            var pa1 = Mem.C.Copy(a1);
            var pa2 = Mem.C.Copy(a2);
            return new Cf<T0, T1, TR>((delegate*<T0, T1, void*, void*, void*, TR>)fn, pa0, pa1, pa2);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct Ca<T0, T1, T2> {
       private readonly void* _fn;
       private readonly int _arity;
       private readonly void* _arg0;
       private readonly void* _arg1;
       private readonly void* _arg2;

       public Ca(delegate*<T0,T1,T2, void> fn) {
           _fn = fn;
           _arg0 = null;
           _arg1 = null;
           _arg2 = null;
           _arity = 0;
       }

       public Ca(delegate*<T0,T1,T2, void*, void> fn, void* a0) {
           _fn = fn;
           _arg0 = a0;
           _arg1 = null;
           _arg2 = null;
           _arity = 1;
       }

       public Ca(delegate*<T0,T1,T2, void*, void*, void> fn, void* a0, void* a1) {
           _fn = fn;
           _arg0 = a0;
           _arg1 = a1;
           _arg2 = null;
           _arity = 2;
       }

       public Ca(delegate*<T0,T1,T2, void*, void*, void*, void> fn, void* a0, void* a1, void* a2) {
           _fn = fn;
           _arg0 = a0;
           _arg1 = a1;
           _arg2 = a2;
           _arity = 3;
       }

       public void Invoke(T0 p0, T1 p1, T2 p2) {
            switch (_arity) {
            case 0:
                ((delegate*<T0,T1,T2, void>)_fn)(p0,p1,p2);
                break;
            case 1:
                ((delegate*<T0,T1,T2, void*, void>)_fn)(p0,p1,p2, _arg0);
                break;
            case 2:
                ((delegate*<T0,T1,T2, void*, void*, void>)_fn)(p0,p1,p2, _arg0, _arg1);
                break;
            case 3:
                ((delegate*<T0,T1,T2, void*, void*, void*, void>)_fn)(p0,p1,p2, _arg0, _arg1, _arg2);
                break;
            default:
                Assert.Fail("Unsupported closure arity");
                return;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct Cf<T0, T1, T2, TR> {
       private readonly void* _fn;
       private readonly int _arity;
       private readonly void* _arg0;
       private readonly void* _arg1;
       private readonly void* _arg2;

       public Cf(delegate*<T0,T1,T2, TR> fn) {
           _fn = fn;
           _arg0 = null;
           _arg1 = null;
           _arg2 = null;
           _arity = 0;
       }

       public Cf(delegate*<T0,T1,T2, void*, TR> fn, void* a0) {
           _fn = fn;
           _arg0 = a0;
           _arg1 = null;
           _arg2 = null;
           _arity = 1;
       }

       public Cf(delegate*<T0,T1,T2, void*, void*, TR> fn, void* a0, void* a1) {
           _fn = fn;
           _arg0 = a0;
           _arg1 = a1;
           _arg2 = null;
           _arity = 2;
       }

       public Cf(delegate*<T0,T1,T2, void*, void*, void*, TR> fn, void* a0, void* a1, void* a2) {
           _fn = fn;
           _arg0 = a0;
           _arg1 = a1;
           _arg2 = a2;
           _arity = 3;
       }

       public TR Invoke(T0 p0, T1 p1, T2 p2) {
            switch (_arity) {
            case 0:
                return ((delegate*<T0, T1, T2, TR>)_fn)(p0, p1, p2);
            case 1:
                return ((delegate*<T0, T1, T2, void*, TR>)_fn)(p0, p1, p2, _arg0);
            case 2:
                return ((delegate*<T0, T1, T2, void*, void*, TR>)_fn)(p0, p1, p2, _arg0, _arg1);
            case 3:
                return ((delegate*<T0, T1, T2, void*, void*, void*, TR>)_fn)(p0, p1, p2, _arg0, _arg1, _arg2);
            default:
                Assert.Fail("Unsupported closure arity");
                return default;
            }
        }
    }

    public static unsafe partial class Ca {
                public static Ca<T0, T1, T2> New<T0, T1, T2>(delegate*<T0, T1, T2, void> fn) {
            return new Ca<T0, T1, T2>((delegate*<T0, T1, T2, void>)fn);
        }

        public static Ca<T0, T1, T2> New<T0, T1, T2, A0>(delegate*<T0, T1, T2, A0*, void> fn,A0 a0)
                where A0: unmanaged {
            var pa0 = Mem.C.Copy(a0);
            return new Ca<T0, T1, T2>((delegate*<T0, T1, T2, void*, void>)fn, pa0);
        }

        public static Ca<T0, T1, T2> New<T0, T1, T2, A0, A1>(delegate*<T0, T1, T2, A0*, A1*, void> fn,A0 a0, A1 a1)
                where A0: unmanaged
                where A1: unmanaged {
            var pa0 = Mem.C.Copy(a0);
            var pa1 = Mem.C.Copy(a1);
            return new Ca<T0, T1, T2>((delegate*<T0, T1, T2, void*, void*, void>)fn, pa0, pa1);
        }

        public static Ca<T0, T1, T2> New<T0, T1, T2, A0, A1, A2>(delegate*<T0, T1, T2, A0*, A1*, A2*, void> fn,A0 a0, A1 a1, A2 a2)
                where A0: unmanaged
                where A1: unmanaged
                where A2: unmanaged {
            var pa0 = Mem.C.Copy(a0);
            var pa1 = Mem.C.Copy(a1);
            var pa2 = Mem.C.Copy(a2);
            return new Ca<T0, T1, T2>((delegate*<T0, T1, T2, void*, void*, void*, void>)fn, pa0, pa1, pa2);
        }
    }

    public static unsafe partial class Cf {
                public static Cf<T0, T1, T2,TR> New<T0, T1, T2, TR>(delegate*<T0, T1, T2, TR> fn) {
            return new Cf<T0, T1, T2, TR>((delegate*<T0, T1, T2, TR>)fn);
        }

        public static Cf<T0, T1, T2,TR> New<T0, T1, T2, A0, TR>(delegate*<T0, T1, T2, A0*, TR> fn,A0 a0)
                where A0: unmanaged {
            var pa0 = Mem.C.Copy(a0);
            return new Cf<T0, T1, T2, TR>((delegate*<T0, T1, T2, void*, TR>)fn, pa0);
        }

        public static Cf<T0, T1, T2,TR> New<T0, T1, T2, A0, A1, TR>(delegate*<T0, T1, T2, A0*, A1*, TR> fn,A0 a0, A1 a1)
                where A0: unmanaged
                where A1: unmanaged {
            var pa0 = Mem.C.Copy(a0);
            var pa1 = Mem.C.Copy(a1);
            return new Cf<T0, T1, T2, TR>((delegate*<T0, T1, T2, void*, void*, TR>)fn, pa0, pa1);
        }

        public static Cf<T0, T1, T2,TR> New<T0, T1, T2, A0, A1, A2, TR>(delegate*<T0, T1, T2, A0*, A1*, A2*, TR> fn,A0 a0, A1 a1, A2 a2)
                where A0: unmanaged
                where A1: unmanaged
                where A2: unmanaged {
            var pa0 = Mem.C.Copy(a0);
            var pa1 = Mem.C.Copy(a1);
            var pa2 = Mem.C.Copy(a2);
            return new Cf<T0, T1, T2, TR>((delegate*<T0, T1, T2, void*, void*, void*, TR>)fn, pa0, pa1, pa2);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct Ca<T0, T1, T2, T3> {
       private readonly void* _fn;
       private readonly int _arity;
       private readonly void* _arg0;
       private readonly void* _arg1;
       private readonly void* _arg2;

       public Ca(delegate*<T0,T1,T2,T3, void> fn) {
           _fn = fn;
           _arg0 = null;
           _arg1 = null;
           _arg2 = null;
           _arity = 0;
       }

       public Ca(delegate*<T0,T1,T2,T3, void*, void> fn, void* a0) {
           _fn = fn;
           _arg0 = a0;
           _arg1 = null;
           _arg2 = null;
           _arity = 1;
       }

       public Ca(delegate*<T0,T1,T2,T3, void*, void*, void> fn, void* a0, void* a1) {
           _fn = fn;
           _arg0 = a0;
           _arg1 = a1;
           _arg2 = null;
           _arity = 2;
       }

       public Ca(delegate*<T0,T1,T2,T3, void*, void*, void*, void> fn, void* a0, void* a1, void* a2) {
           _fn = fn;
           _arg0 = a0;
           _arg1 = a1;
           _arg2 = a2;
           _arity = 3;
       }

       public void Invoke(T0 p0, T1 p1, T2 p2, T3 p3) {
            switch (_arity) {
            case 0:
                ((delegate*<T0,T1,T2,T3, void>)_fn)(p0,p1,p2,p3);
                break;
            case 1:
                ((delegate*<T0,T1,T2,T3, void*, void>)_fn)(p0,p1,p2,p3, _arg0);
                break;
            case 2:
                ((delegate*<T0,T1,T2,T3, void*, void*, void>)_fn)(p0,p1,p2,p3, _arg0, _arg1);
                break;
            case 3:
                ((delegate*<T0,T1,T2,T3, void*, void*, void*, void>)_fn)(p0,p1,p2,p3, _arg0, _arg1, _arg2);
                break;
            default:
                Assert.Fail("Unsupported closure arity");
                return;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct Cf<T0, T1, T2, T3, TR> {
       private readonly void* _fn;
       private readonly int _arity;
       private readonly void* _arg0;
       private readonly void* _arg1;
       private readonly void* _arg2;

       public Cf(delegate*<T0,T1,T2,T3, TR> fn) {
           _fn = fn;
           _arg0 = null;
           _arg1 = null;
           _arg2 = null;
           _arity = 0;
       }

       public Cf(delegate*<T0,T1,T2,T3, void*, TR> fn, void* a0) {
           _fn = fn;
           _arg0 = a0;
           _arg1 = null;
           _arg2 = null;
           _arity = 1;
       }

       public Cf(delegate*<T0,T1,T2,T3, void*, void*, TR> fn, void* a0, void* a1) {
           _fn = fn;
           _arg0 = a0;
           _arg1 = a1;
           _arg2 = null;
           _arity = 2;
       }

       public Cf(delegate*<T0,T1,T2,T3, void*, void*, void*, TR> fn, void* a0, void* a1, void* a2) {
           _fn = fn;
           _arg0 = a0;
           _arg1 = a1;
           _arg2 = a2;
           _arity = 3;
       }

       public TR Invoke(T0 p0, T1 p1, T2 p2, T3 p3) {
            switch (_arity) {
            case 0:
                return ((delegate*<T0, T1, T2, T3, TR>)_fn)(p0, p1, p2, p3);
            case 1:
                return ((delegate*<T0, T1, T2, T3, void*, TR>)_fn)(p0, p1, p2, p3, _arg0);
            case 2:
                return ((delegate*<T0, T1, T2, T3, void*, void*, TR>)_fn)(p0, p1, p2, p3, _arg0, _arg1);
            case 3:
                return ((delegate*<T0, T1, T2, T3, void*, void*, void*, TR>)_fn)(p0, p1, p2, p3, _arg0, _arg1, _arg2);
            default:
                Assert.Fail("Unsupported closure arity");
                return default;
            }
        }
    }

    public static unsafe partial class Ca {
                public static Ca<T0, T1, T2, T3> New<T0, T1, T2, T3>(delegate*<T0, T1, T2, T3, void> fn) {
            return new Ca<T0, T1, T2, T3>((delegate*<T0, T1, T2, T3, void>)fn);
        }

        public static Ca<T0, T1, T2, T3> New<T0, T1, T2, T3, A0>(delegate*<T0, T1, T2, T3, A0*, void> fn,A0 a0)
                where A0: unmanaged {
            var pa0 = Mem.C.Copy(a0);
            return new Ca<T0, T1, T2, T3>((delegate*<T0, T1, T2, T3, void*, void>)fn, pa0);
        }

        public static Ca<T0, T1, T2, T3> New<T0, T1, T2, T3, A0, A1>(delegate*<T0, T1, T2, T3, A0*, A1*, void> fn,A0 a0, A1 a1)
                where A0: unmanaged
                where A1: unmanaged {
            var pa0 = Mem.C.Copy(a0);
            var pa1 = Mem.C.Copy(a1);
            return new Ca<T0, T1, T2, T3>((delegate*<T0, T1, T2, T3, void*, void*, void>)fn, pa0, pa1);
        }

        public static Ca<T0, T1, T2, T3> New<T0, T1, T2, T3, A0, A1, A2>(delegate*<T0, T1, T2, T3, A0*, A1*, A2*, void> fn,A0 a0, A1 a1, A2 a2)
                where A0: unmanaged
                where A1: unmanaged
                where A2: unmanaged {
            var pa0 = Mem.C.Copy(a0);
            var pa1 = Mem.C.Copy(a1);
            var pa2 = Mem.C.Copy(a2);
            return new Ca<T0, T1, T2, T3>((delegate*<T0, T1, T2, T3, void*, void*, void*, void>)fn, pa0, pa1, pa2);
        }
    }

    public static unsafe partial class Cf {
                public static Cf<T0, T1, T2, T3,TR> New<T0, T1, T2, T3, TR>(delegate*<T0, T1, T2, T3, TR> fn) {
            return new Cf<T0, T1, T2, T3, TR>((delegate*<T0, T1, T2, T3, TR>)fn);
        }

        public static Cf<T0, T1, T2, T3,TR> New<T0, T1, T2, T3, A0, TR>(delegate*<T0, T1, T2, T3, A0*, TR> fn,A0 a0)
                where A0: unmanaged {
            var pa0 = Mem.C.Copy(a0);
            return new Cf<T0, T1, T2, T3, TR>((delegate*<T0, T1, T2, T3, void*, TR>)fn, pa0);
        }

        public static Cf<T0, T1, T2, T3,TR> New<T0, T1, T2, T3, A0, A1, TR>(delegate*<T0, T1, T2, T3, A0*, A1*, TR> fn,A0 a0, A1 a1)
                where A0: unmanaged
                where A1: unmanaged {
            var pa0 = Mem.C.Copy(a0);
            var pa1 = Mem.C.Copy(a1);
            return new Cf<T0, T1, T2, T3, TR>((delegate*<T0, T1, T2, T3, void*, void*, TR>)fn, pa0, pa1);
        }

        public static Cf<T0, T1, T2, T3,TR> New<T0, T1, T2, T3, A0, A1, A2, TR>(delegate*<T0, T1, T2, T3, A0*, A1*, A2*, TR> fn,A0 a0, A1 a1, A2 a2)
                where A0: unmanaged
                where A1: unmanaged
                where A2: unmanaged {
            var pa0 = Mem.C.Copy(a0);
            var pa1 = Mem.C.Copy(a1);
            var pa2 = Mem.C.Copy(a2);
            return new Cf<T0, T1, T2, T3, TR>((delegate*<T0, T1, T2, T3, void*, void*, void*, TR>)fn, pa0, pa1, pa2);
        }
    }

}