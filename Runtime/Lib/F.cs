using System;
using System.Collections.Generic;
using Rondo.Core.Extras;
using Rondo.Core.Lib.Containers;
using Rondo.Core.Memory;

namespace Rondo.Core.Lib {
    public static unsafe class F {
        public static T Id<T>(T t) {
            return t;
        }

        public static Cf<T> Always<T>(T t) where T : unmanaged {
            static T Impl(T* t) {
                return *t;
            }

            return Cf.New(&Impl, t);
        }

        public static Cf<TX, TY> Always<TX, TY>(TY t) where TY : unmanaged {
            static TY Impl(TX _, TY* t) {
                return *t;
            }

            return Cf.New<TX, TY, TY>(&Impl, t);
        }

        public static TL Left<TL, TR>(TL left, TR _) {
            return left;
        }

        public static TR Right<TL, TR>(TL _, TR right) {
            return right;
        }

        public static bool IsNull(Ptr ptr) {
            return ptr == Ptr.Null;
        }

        public static bool NotNull(Ptr ptr) {
            return ptr != Ptr.Null;
        }

        public static bool IsDefault<T>(T x) where T : IEquatable<T> {
            return x.Equals(default);
        }

        public static bool NotDefault<T>(T x) where T : IEquatable<T> {
            return !x.Equals(default);
        }

        public static unsafe bool Equals<T>(T x, T* y) where T : unmanaged, IEquatable<T> {
            return x.Equals(*y);
        }

        public static unsafe bool NotEquals<T>(T x, T* y) where T : unmanaged, IEquatable<T> {
            return !x.Equals(*y);
        }

        public static S ToS<T>(T x) {
            return (S)x.ToString();
        }

        public static Cf<Ptr, Ptr> ToPtrPtr<TIn, TOut>(delegate*<TIn, TOut> fn)
                where TIn : unmanaged where TOut : unmanaged {
            return Cf.New<Ptr, Cf<TIn, TOut>, Ptr>(&ToPtrPtrCast, Cf.New(fn));
        }

        public static Cf<Ptr, Ptr> ToPtrPtr<TIn, TOut>(Cf<TIn, TOut> fn)
                where TIn : unmanaged where TOut : unmanaged {
            return Cf.New<Ptr, Cf<TIn, TOut>, Ptr>(&ToPtrPtrCast, fn);
        }

        private static Ptr ToPtrPtrCast<TIn, TOut>(Ptr pIn, Cf<TIn, TOut>* fn)
                where TIn : unmanaged where TOut : unmanaged {
            Assert.That(pIn != Ptr.Null, "Null pointer cannot be dereferenced");
            var msg = fn->Invoke(*(TIn*)pIn.Raw);
            return Mem.C.CopyPtr(msg);
        }

        public static Xf<Ptr, Ptr> ToPtrPtr<TIn, TOut>(Xf<TIn, TOut> fn)
                where TIn : unmanaged where TOut : unmanaged {
            return Xf.New<Ptr, Xf<TIn, TOut>, Ptr>(&ToPtrPtrCast, fn);
        }

        private static Ptr ToPtrPtrCast<TIn, TOut>(Ptr pIn, Xf<TIn, TOut>* fn)
                where TIn : unmanaged where TOut : unmanaged {
            Assert.That(pIn != Ptr.Null, "Null pointer cannot be dereferenced");
            var msg = fn->Invoke(*(TIn*)pIn.Raw);
            return Mem.C.CopyPtr(msg);
        }

        public static S Join(S a, S b) {
            return (S)((string)a + (string)b);
        }

        public static (TA, TB) ToTuple<TA, TB>(TA a, TB b) {
            return (a, b);
        }

        public static int Compare<T>(T a, T b) where T : IComparable<T> {
            return a.CompareTo(b);
        }

        public static int DefaultComparer<T>(T a, T b) {
            return Comparer<T>.Default.Compare(a, b);
        }
    }
}