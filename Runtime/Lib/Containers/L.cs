using System;
using System.Collections;
using System.Collections.Generic;
using Rondo.Core.Memory;

namespace Rondo.Core.Lib.Containers {
    public readonly unsafe struct L<T> : IEquatable<L<T>> where T : unmanaged {
        internal static readonly int ElementSize = Mem.SizeOf<T>();
        internal readonly T* Data;
        internal readonly int Size;

        internal L(T* data, int size) {
            size = Math.Max(0, size);
            Data = data;
            Size = size;
        }

        internal L(L<T> other, int size = -1) {
            if (size < 0) {
                size = other.Size;
            }
            Size = size;
            Data = (T*)Mem.C.Alloc(size * ElementSize);
            var sz = ElementSize * Math.Min(other.Size, size);
            Buffer.MemoryCopy(other.Data, Data, sz, sz);
        }

        internal L(int size) {
            Size = size;

            Data = (size > 0) ? (T*)Mem.C.Alloc(size * ElementSize) : null;
        }

        public L(T a0) : this(1) {
            Data[0] = a0;
        }

        public L(T a0, T a1) : this(2) {
            Data[0] = a0;
            Data[1] = a1;
        }

        public L(T a0, T a1, T a2) : this(3) {
            Data[0] = a0;
            Data[1] = a1;
            Data[2] = a2;
        }

        public L(T a0, T a1, T a2, T a3) : this(4) {
            Data[0] = a0;
            Data[1] = a1;
            Data[2] = a2;
            Data[3] = a3;
        }

        public L(T a0, T a1, T a2, T a3, T a4) : this(5) {
            Data[0] = a0;
            Data[1] = a1;
            Data[2] = a2;
            Data[3] = a3;
            Data[4] = a4;
        }

        public L(T a0, T a1, T a2, T a3, T a4, T a5) : this(6) {
            Data[0] = a0;
            Data[1] = a1;
            Data[2] = a2;
            Data[3] = a3;
            Data[4] = a4;
            Data[5] = a5;
        }

        public L(T a0, T a1, T a2, T a3, T a4, T a5, T a6) : this(7) {
            Data[0] = a0;
            Data[1] = a1;
            Data[2] = a2;
            Data[3] = a3;
            Data[4] = a4;
            Data[5] = a5;
            Data[6] = a6;
        }

        public L(T a0, T a1, T a2, T a3, T a4, T a5, T a6, T a7) : this(8) {
            Data[0] = a0;
            Data[1] = a1;
            Data[2] = a2;
            Data[3] = a3;
            Data[4] = a4;
            Data[5] = a5;
            Data[6] = a6;
            Data[7] = a7;
        }

        public L(T a0, T a1, T a2, T a3, T a4, T a5, T a6, T a7, T a8) : this(9) {
            Data[0] = a0;
            Data[1] = a1;
            Data[2] = a2;
            Data[3] = a3;
            Data[4] = a4;
            Data[5] = a5;
            Data[6] = a6;
            Data[7] = a7;
            Data[8] = a8;
        }

        public L(T a0, T a1, T a2, T a3, T a4, T a5, T a6, T a7, T a8, T a9) : this(10) {
            Data[0] = a0;
            Data[1] = a1;
            Data[2] = a2;
            Data[3] = a3;
            Data[4] = a4;
            Data[5] = a5;
            Data[6] = a6;
            Data[7] = a7;
            Data[8] = a8;
            Data[9] = a9;
        }

        /// <summary>
        /// Add an element to the front of a list.
        /// </summary>
        public static L<T> operator +(T head, L<T> tail) {
            return tail.Cons(head);
        }

        /// <summary>
        /// Add an element to the end of a list.
        /// </summary>
        public static L<T> operator +(L<T> front, T x) {
            return front.Push(x);
        }

        /// <summary>
        /// Put two lists together.
        /// </summary>
        public static L<T> operator +(L<T> a, L<T> b) {
            return a.Append(b);
        }

        public Maybe<T> this[int index] {
            get { return this.At(index); }
        }

        public E Enumerator => new(this);

        public struct E : IEnumerator<T> {
            private readonly L<T> _list;
            private int _index;
            private T _current;

            internal E(L<T> list) {
                _list = list;
                _index = 0;
                _current = default;
            }

            public bool MoveNext() {
                if (_index >= _list.Size) {
                    return false;
                }
                _current = _list.Data[_index];
                _index++;
                return true;
            }

            public void Reset() {
                _index = 0;
            }

            public T Current => _current;

            object IEnumerator.Current => Current;

            public void Dispose() { }
        }

        public bool Equals(L<T> other) {
            return Data == other.Data;
        }

        public override bool Equals(object obj) {
            return obj is L<T> other && Equals(other);
        }

        public override int GetHashCode() {
            return ((IntPtr)Data).ToInt32();
        }

#if DEBUG
        public override string ToString() {
            return Serializer.Stringify(this);
        }
#endif
    }

    public static unsafe class L {
        public static L<T> Empty<T>() where T : unmanaged {
            return new L<T>();
        }

        /// <summary>
        /// Create a list with n copies of a value
        /// </summary>
        public static L<T> Repeat<T>(int n, T x) where T : unmanaged {
            var list = new L<T>(n);
            while (n-- > 0) {
                list.Data[n] = x;
            }
            return list;
        }

        /// <summary>
        /// Create a list of numbers, every element increasing by one. You give the lowest and highest number that should be in the list.
        /// </summary>
        public static L<int> Range(int min, int max) {
            var list = new L<int>(max - min + 1);
            for (var i = 0; i < list.Size; i++) {
                list.Data[i] = min + i;
            }
            return list;
        }

        /// <summary>
        /// Add an element to the front of a list.
        /// </summary>
        public static L<T> Cons<T>(this L<T> tail, T head) where T : unmanaged {
            var list = new L<T>(tail.Size + 1);
            list.Data[0] = head;
            var sz = tail.Size * L<T>.ElementSize;
            Buffer.MemoryCopy(tail.Data, list.Data + 1, sz, sz);
            return list;
        }

        /// <summary>
        /// Add an element to the end of a list
        /// </summary>
        public static L<T> Push<T>(this L<T> front, T x) where T : unmanaged {
            var next = new L<T>(front, front.Size + 1);
            next.Data[front.Size] = x;
            return next;
        }

        /// <summary>
        /// Extract first element from the list (head). Return the head and the rest list (tail)
        /// </summary>
        public static (T, L<T>) Decons<T>(this L<T> xs) where T : unmanaged {
            var tail = new L<T>(xs.Size - 1);
            if (tail.Size > 0) {
                var sz = tail.Size * L<T>.ElementSize;
                Buffer.MemoryCopy(xs.Data + 1, tail.Data, sz, sz);
            }
            return (xs.Data[0], tail);
        }

        /// <summary>
        /// Apply a function to every element of a list.
        /// </summary>
        public static L<TY> Map<TX, TY>(this L<TX> xs, delegate*<TX, TY> f)
                where TX : unmanaged where TY : unmanaged {
            return xs.Map(Cf.New(f));
        }

        public static L<TY> Map<TX, TY>(this L<TX> xs, Cf<TX, TY> f)
                where TX : unmanaged where TY : unmanaged {
            var ys = new L<TY>(xs.Size);
            for (var i = 0; i < xs.Size; i++) {
                ys.Data[i] = f.Invoke(xs.Data[i]);
            }
            return ys;
        }

        /// <summary>
        /// Same as map but the function is also applied to the index of each element (starting at zero).
        /// </summary>
        public static L<TY> IndexedMap<TX, TY>(this L<TX> xs, delegate*<int, TX, TY> f)
                where TX : unmanaged where TY : unmanaged {
            return xs.IndexedMap(Cf.New(f));
        }

        public static L<TY> IndexedMap<TX, TY>(this L<TX> xs, Cf<int, TX, TY> f)
                where TX : unmanaged where TY : unmanaged {
            var ys = new L<TY>(xs.Size);
            for (var i = 0; i < xs.Size; i++) {
                ys.Data[i] = f.Invoke(i, xs.Data[i]);
            }
            return ys;
        }

        /// <summary>
        /// Reduce a list from the left.
        /// </summary>
        public static TA Foldl<T, TA>(this L<T> xs, delegate*<T, TA, TA> f, TA acc)
                where T : unmanaged {
            return xs.Foldl(Cf.New(f), acc);
        }

        public static TA Foldl<T, TA>(this L<T> xs, Cf<T, TA, TA> f, TA acc)
                where T : unmanaged {
            for (var i = 0; i < xs.Size; i++) {
                acc = f.Invoke(xs.Data[i], acc);
            }
            return acc;
        }

        /// <summary>
        /// Reduce a list from the right.
        /// </summary>
        public static TA Foldr<T, TA>(this L<T> xs, delegate*<T, TA, TA> f, TA acc)
                where T : unmanaged {
            return xs.Foldr(Cf.New(f), acc);
        }

        public static TA Foldr<T, TA>(this L<T> xs, Cf<T, TA, TA> f, TA acc)
                where T : unmanaged {
            for (var i = xs.Size - 1; i >= 0; i--) {
                acc = f.Invoke(xs.Data[i], acc);
            }
            return acc;
        }

        /// <summary>
        /// Keep elements that satisfy the test.
        /// </summary>
        public static L<T> Filter<T>(this L<T> xs, delegate*<T, bool> f)
                where T : unmanaged {
            return xs.Filter(Cf.New(f));
        }

        public static L<T> Filter<T>(this L<T> xs, Cf<T, bool> f)
                where T : unmanaged {
            var bs = xs.Map(f);
            var size = bs.Count(&F.Id);
            var list = new L<T>(size);
            if (size > 0) {
                var index = 0;
                for (var i = 0; i < xs.Size; i++) {
                    if (bs.Data[i]) {
                        list.Data[index++] = xs.Data[i];
                    }
                }
            }

            return list;
        }

        public static int Count<T>(this L<T> xs, delegate*<T, bool> f)
                where T : unmanaged {
            return xs.Count(Cf.New(f));
        }

        public static int Count<T>(this L<T> xs, Cf<T, bool> f)
                where T : unmanaged {
            var size = 0;
            for (var i = 0; i < xs.Size; i++) {
                size += f.Invoke(xs.Data[i]) ? 1 : 0;
            }
            return size;
        }

        public static Maybe<T> First<T>(this L<T> xs, delegate*<T, bool> f) where T : unmanaged {
            return xs.First(Cf.New(f));
        }

        public static Maybe<T> First<T>(this L<T> xs, Cf<T, bool> f) where T : unmanaged {
            for (var i = 0; i < xs.Size; i++) {
                if (f.Invoke(xs.Data[i])) {
                    return Maybe<T>.Just(xs.Data[i]);
                }
            }
            return Maybe<T>.Nothing;
        }

        public static Maybe<T> First<T>(this L<T> xs) where T : unmanaged {
            return xs.At(0);
        }

        public static Maybe<T> Last<T>(this L<T> xs, delegate*<T, bool> f) where T : unmanaged {
            return xs.Last(Cf.New(f));
        }

        public static Maybe<T> Last<T>(this L<T> xs, Cf<T, bool> f) where T : unmanaged {
            for (var i = xs.Size - 1; i >= 0; i--) {
                if (f.Invoke(xs.Data[i])) {
                    return Maybe<T>.Just(xs.Data[i]);
                }
            }
            return Maybe<T>.Nothing;
        }

        public static Maybe<T> Last<T>(this L<T> xs) where T : unmanaged {
            return xs.At(xs.Length() - 1);
        }

        /// <summary>
        /// Filter out certain values. For example, maybe you have a bunch of strings from an untrusted source
        /// and you want to turn them into numbers.
        /// </summary>
        public static L<TY> FilterMap<TX, TY>(this L<TX> xs, delegate*<TX, Maybe<TY>> f)
                where TX : unmanaged where TY : unmanaged {
            return xs.FilterMap(Cf.New(f));
        }

        public static L<TY> FilterMap<TX, TY>(this L<TX> xs, Cf<TX, Maybe<TY>> f)
                where TX : unmanaged where TY : unmanaged {
            var ys = xs.Map(f);
            var size = 0;
            for (var i = 0; i < ys.Size; i++) {
                if (ys.Data[i].Test(out _)) {
                    size++;
                }
            }
            var list = new L<TY>(size);
            if (size > 0) {
                var index = 0;
                for (var i = 0; i < xs.Size; i++) {
                    if (f.Invoke(xs.Data[i]).Test(out var y)) {
                        list.Data[index++] = y;
                    }
                }
            }

            return list;
        }

        public static L<TY> IndexedFilterMap<TX, TY>(this L<TX> xs, delegate*<int, TX, Maybe<TY>> f)
                where TX : unmanaged where TY : unmanaged {
            return xs.IndexedFilterMap(Cf.New(f));
        }

        public static L<TY> IndexedFilterMap<TX, TY>(this L<TX> xs, Cf<int, TX, Maybe<TY>> f)
                where TX : unmanaged where TY : unmanaged {
            return xs.IndexedMap(f).FilterMap(&F.Id);
        }

        /// <summary>
        /// Determine the length of a list.
        /// </summary>
        public static int Length<T>(this L<T> xs) where T : unmanaged {
            return xs.Size;
        }

        /// <summary>
        /// Reverse a list.
        /// </summary>
        public static L<T> Reverse<T>(this L<T> xs) where T : unmanaged {
            var list = new L<T>(xs.Size);
            var index = 0;
            for (var i = xs.Size - 1; i >= 0; i--) {
                list.Data[index++] = xs.Data[i];
            }
            return list;
        }

        /// <summary>
        /// Figure out whether a list contains a value.
        /// </summary>
        public static bool Member<T>(this L<T> xs, T s)
                where T : unmanaged, IEquatable<T> {
            return xs.IndexOf(s).Test(out _);
        }

        /// <summary>
        /// Figure out an index of a value.
        /// </summary>
        public static Maybe<int> IndexOf<T>(this L<T> xs, T s)
                where T : unmanaged, IEquatable<T> {
            for (var i = 0; i < xs.Size; i++) {
                if (s.Equals(xs.Data[i])) {
                    return Maybe<int>.Just(i);
                }
            }
            return Maybe<int>.Nothing;
        }

        /// <summary>
        /// Determine if all elements satisfy some test.
        /// </summary>
        public static bool All<T>(this L<T> xs, delegate*<T, bool> test) where T : unmanaged {
            return xs.All(Cf.New(test));
        }

        public static bool All<T>(this L<T> xs, Cf<T, bool> test) where T : unmanaged {
            for (var i = 0; i < xs.Size; i++) {
                if (!test.Invoke(xs.Data[i])) {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Determine if any elements satisfy some test.
        /// </summary>
        public static bool Any<T>(this L<T> xs, delegate*<T, bool> test) where T : unmanaged {
            return xs.Any(Cf.New(test));
        }

        public static bool Any<T>(this L<T> xs, Cf<T, bool> test) where T : unmanaged {
            for (var i = 0; i < xs.Size; i++) {
                if (test.Invoke(xs.Data[i])) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Put two lists together.
        /// </summary>
        public static L<T> Append<T>(this L<T> xs, L<T> ys) where T : unmanaged {
            if (ys.IsEmpty()) {
                return xs;
            }
            if (xs.IsEmpty()) {
                return ys;
            }
            var list = new L<T>(xs, xs.Size + ys.Size);
            var sz = ys.Size * L<T>.ElementSize;
            Buffer.MemoryCopy(ys.Data, list.Data + xs.Size, sz, sz);
            return list;
        }

        /// <summary>
        /// Concatenate a bunch of lists into a single list:
        /// </summary>
        public static L<T> Concat<T>(this L<L<T>> lists) where T : unmanaged {
            var size = lists.Foldl(&SumLength, 0);
            var list = new L<T>(size);
            var offset = 0;
            for (var i = 0; i < lists.Size; i++) {
                var l = lists.Data[i];
                var sz = l.Size * L<T>.ElementSize;
                Buffer.MemoryCopy(l.Data, list.Data + offset, sz, sz);
                offset += l.Size;
            }
            return list;
        }

        private static int SumLength<T>(L<T> list, int sum) where T : unmanaged {
            return list.Length() + sum;
        }

        /// <summary>
        /// Map a given function onto a list and flatten the resulting lists.
        /// </summary>
        public static L<TB> ConcatMap<TA, TB>(this L<TA> xs, delegate*<TA, L<TB>> f)
                where TA : unmanaged where TB : unmanaged {
            return xs.ConcatMap(Cf.New(f));
        }

        public static L<TB> ConcatMap<TA, TB>(this L<TA> xs, Cf<TA, L<TB>> f)
                where TA : unmanaged where TB : unmanaged {
            return xs.Map(f).Concat();
        }

        /// <summary>
        /// Places the given value between all members of the given list.
        /// </summary>
        public static L<T> Intersperse<T>(this L<T> xs, T sep) where T : unmanaged {
            if (xs.IsEmpty()) {
                return new();
            }
            var list = new L<T>(xs.Length() * 2 - 1);
            list.Data[0] = xs.Data[0];
            var index = 1;
            for (var i = 1; i < xs.Size; i++) {
                list.Data[index++] = sep;
                list.Data[index++] = xs.Data[i];
            }

            return list;
        }

        /// <summary>
        /// Combine two lists, combining them with the given function.
        /// If one list is longer, the extra elements are dropped.
        /// </summary>
        public static L<TR> Map2<TA, TB, TR>(this L<TA> la, L<TB> lb,
            delegate*<TA, TB, TR> f)
                where TA : unmanaged
                where TB : unmanaged
                where TR : unmanaged {
            return la.Map2(lb, Cf.New(f));
        }

        public static L<TR> Map2<TA, TB, TR>(this L<TA> la, L<TB> lb,
            Cf<TA, TB, TR> f)
                where TA : unmanaged
                where TB : unmanaged
                where TR : unmanaged {
            if (la.IsEmpty() || lb.IsEmpty()) {
                return new();
            }
            var list = new L<TR>(Math.Min(la.Length(), lb.Length()));
            for (var i = 0; i < list.Size; i++) {
                list.Data[i] = f.Invoke(la.Data[i], lb.Data[i]);
            }
            return list;
        }

        public static L<TR> Map3<TA, TB, TC, TR>(this L<TA> la, L<TB> lb, L<TC> lc,
            delegate*<TA, TB, TC, TR> f)
                where TA : unmanaged
                where TB : unmanaged
                where TR : unmanaged
                where TC : unmanaged {
            return la.Map3(lb, lc, Cf.New(f));
        }

        public static L<TR> Map3<TA, TB, TC, TR>(this L<TA> la, L<TB> lb, L<TC> lc,
            Cf<TA, TB, TC, TR> f)
                where TA : unmanaged
                where TB : unmanaged
                where TR : unmanaged
                where TC : unmanaged {
            if (la.IsEmpty() || lb.IsEmpty() || lc.IsEmpty()) {
                return new();
            }
            var list = new L<TR>(Math.Min(la.Length(), lb.Length()));
            for (var i = 0; i < list.Size; i++) {
                list.Data[i] = f.Invoke(la.Data[i], lb.Data[i], lc.Data[i]);
            }
            return list;
        }

        public static L<TR> Map4<TA, TB, TC, TD, TR>(this L<TA> la, L<TB> lb, L<TC> lc, L<TD> ld,
            delegate*<TA, TB, TC, TD, TR> f)
                where TA : unmanaged
                where TB : unmanaged
                where TR : unmanaged
                where TC : unmanaged
                where TD : unmanaged {
            return la.Map4(lb, lc, ld, Cf.New(f));
        }

        public static L<TR> Map4<TA, TB, TC, TD, TR>(this L<TA> la, L<TB> lb, L<TC> lc, L<TD> ld,
            Cf<TA, TB, TC, TD, TR> f)
                where TA : unmanaged
                where TB : unmanaged
                where TR : unmanaged
                where TC : unmanaged
                where TD : unmanaged {
            if (la.IsEmpty() || lb.IsEmpty() || lc.IsEmpty() || ld.IsEmpty()) {
                return new();
            }
            var list = new L<TR>(Math.Min(la.Length(), lb.Length()));
            for (var i = 0; i < list.Size; i++) {
                list.Data[i] = f.Invoke(la.Data[i], lb.Data[i], lc.Data[i], ld.Data[i]);
            }
            return list;
        }

        /// <summary>
        /// Sort values from lowest to highest
        /// </summary>
        public static L<T> Sort<T>(this L<T> xs) where T : unmanaged, IComparable<T> {
            return xs.SortBy(&F.Id);
        }

        /// <summary>
        /// Sort values by a derived property.
        /// </summary>
        public static L<T> SortBy<T, TC>(this L<T> xs, delegate*<T, TC> f)
                where T : unmanaged where TC : unmanaged, IComparable<TC> {
            return xs.SortBy(Cf.New(f));
        }

        public static L<T> SortBy<T, TC>(this L<T> xs, Cf<T, TC> f)
                where T : unmanaged where TC : IComparable<TC> {
            var list = new L<T>(xs);
            TopDownSplitMerge(xs, 0, xs.Size, list, f);
            return list;

            static void TopDownSplitMerge(L<T> b, int begin, int end, L<T> a, Cf<T, TC> f) {
                if ((end - begin) <= 1) {
                    return;
                }

                var iMiddle = (end + begin) / 2;
                TopDownSplitMerge(a, begin, iMiddle, b, f);
                TopDownSplitMerge(a, iMiddle, end, b, f);
                TopDownMerge(b, begin, iMiddle, end, a, f);
            }

            static void TopDownMerge(L<T> a, int begin, int middle, int end, L<T> b, Cf<T, TC> f) {
                var i = begin;
                var j = middle;

                for (var k = begin; k < end; k++) {
                    if ((i < middle) && ((j >= end) || (f.Invoke(a.Data[i]).CompareTo(f.Invoke(a.Data[j])) <= 0))) {
                        b.Data[k] = a.Data[i];
                        i += 1;
                    }
                    else {
                        b.Data[k] = a.Data[j];
                        j += 1;
                    }
                }
            }
        }

        /// <summary>
        /// Sort values with a custom comparison function.
        /// </summary>
        public static L<T> SortWith<T>(this L<T> xs, delegate*<T, T, int> comp) where T : unmanaged {
            return xs.SortWith(Cf.New(comp));
        }

        public static L<T> SortWith<T>(this L<T> xs, Cf<T, T, int> comp) where T : unmanaged {
            var list = new L<T>(xs);
            TopDownSplitMerge(xs, 0, xs.Size, list, comp);
            return list;

            static void TopDownSplitMerge(L<T> b, int begin, int end, L<T> a, Cf<T, T, int> comp) {
                if ((end - begin) <= 1) {
                    return;
                }

                var iMiddle = (end + begin) / 2;
                TopDownSplitMerge(a, begin, iMiddle, b, comp);
                TopDownSplitMerge(a, iMiddle, end, b, comp);
                TopDownMerge(b, begin, iMiddle, end, a, comp);
            }

            static void TopDownMerge(L<T> a, int begin, int middle, int end, L<T> b, Cf<T, T, int> comp) {
                var i = begin;
                var j = middle;

                for (var k = begin; k < end; k++) {
                    if ((i < middle) && ((j >= end) || (comp.Invoke(a.Data[i], a.Data[j]) <= 0))) {
                        b.Data[k] = a.Data[i];
                        i += 1;
                    }
                    else {
                        b.Data[k] = a.Data[j];
                        j += 1;
                    }
                }
            }
        }

        /// <summary>
        /// Determine if a list is empty.
        /// </summary>
        public static bool IsEmpty<T>(this L<T> xs) where T : unmanaged {
            return xs.Size == 0;
        }

        /// <summary>
        /// Extract the first element of a list.
        /// </summary>
        public static Maybe<T> Head<T>(this L<T> xs) where T : unmanaged {
            if (xs.IsEmpty()) {
                return Maybe<T>.Nothing;
            }

            return Maybe<T>.Just(xs.Data[0]);
        }

        /// <summary>
        /// Extract the rest of the list.
        /// </summary>
        public static Maybe<L<T>> Tail<T>(this L<T> xs) where T : unmanaged {
            if (xs.IsEmpty()) {
                return Maybe<L<T>>.Nothing;
            }
            var tail = new L<T>(xs.Size - 1);
            if (tail.Size > 0) {
                var sz = tail.Size * L<T>.ElementSize;
                Buffer.MemoryCopy(xs.Data + 1, tail.Data, sz, sz);
            }
            return Maybe<L<T>>.Just(tail);
        }

        /// <summary>
        /// Take the first n members of a list.
        /// </summary>
        public static L<T> Take<T>(this L<T> xs, int n) where T : unmanaged {
            if (xs.IsEmpty() || (n <= 0)) {
                return new();
            }

            return new L<T>(xs, n);
        }

        /// <summary>
        /// Drop the first n members of a list.
        /// </summary>
        public static L<T> Drop<T>(this L<T> xs, int n) where T : unmanaged {
            return new L<T>(new L<T>(xs.Data + n, xs.Size - n));
        }

        /// <summary>
        /// Partition a list based on some test. The first list contains all values that satisfy the test,
        /// and the second list contains all the value that do not.
        /// </summary>
        public static (L<T>, L<T>) Partition<T>(delegate*<T, bool> test, L<T> xs) where T : unmanaged {
            var n = 0;
            for (var i = 0; i < xs.Size; i++) {
                n += test(xs.Data[i]) ? 1 : 0;
            }

            var approved = new L<T>(n);
            var rejected = new L<T>(xs.Size - n);
            var a = 0;
            var r = 0;
            for (var i = 0; i < xs.Size; i++) {
                if (test(xs.Data[i])) {
                    approved.Data[a++] = xs.Data[i];
                }
                else {
                    rejected.Data[r++] = xs.Data[i];
                }
            }
            return (approved, rejected);
        }

        /// <summary>
        /// Decompose a list of tuples into a tuple of lists.
        /// </summary>
        public static (L<TL>, L<TR>) Unzip<TL, TR>(L<(TL, TR)> xs)
                where TL : unmanaged where TR : unmanaged {
            var l = new L<TL>(xs.Size);
            var r = new L<TR>(xs.Size);

            for (var i = 0; i < xs.Size; i++) {
                var t = xs.Data[i];
                l.Data[i] = t.Item1;
                r.Data[i] = t.Item2;
            }

            return (l, r);
        }

        public static T Merge<TL, TR, T>(
            this L<TL> left,
            L<TR> right,
            delegate*<int, TL, T, T> leftStep,
            delegate*<int, TL, TR, T, T> bothStep,
            delegate*<int, TR, T, T> rightStep,
            T acc)
                where TL : unmanaged where TR : unmanaged {
            var sz = Math.Max(left.Length(), right.Length());
            for (var i = 0; i < sz; i++) {
                if (i < left.Size) {
                    if (i < right.Size) {
                        acc = bothStep(i, left.Data[i], right.Data[i], acc);
                    }
                    else {
                        acc = leftStep(i, left.Data[i], acc);
                    }
                }
                else {
                    acc = rightStep(i, right.Data[i], acc);
                }
            }
            return acc;
        }

        public static bool DeepEquals<T>(this L<T> left, L<T> right)
                where T : unmanaged, IEquatable<T> {
            var eL = left.Enumerator;
            var eR = right.Enumerator;
            while (eL.MoveNext() && eR.MoveNext()) {
                if (!eL.Current.Equals(eR.Current)) {
                    return false;
                }
            }
            return !eL.MoveNext() && !eR.MoveNext();
        }

        public static D<TK, TV> ToD<TK, TV>(this L<P<TK, TV>> list)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            var sorted = list.SortBy(&P.Key);
            var duplicates = 0;
            for (var i = 1; i < sorted.Size; i++) {
                if (sorted.Data[i - 1].Key.CompareTo(sorted.Data[i].Key) == 0) {
                    duplicates++;
                }
            }
            if (duplicates > 0) {
                var deduped = new L<P<TK, TV>>(sorted.Size - duplicates);
                deduped.Data[0] = sorted.Data[0];

                var index = 0;
                for (var i = 1; i < sorted.Size; i++) {
                    if (deduped.Data[index].Key.CompareTo(sorted.Data[i].Key) != 0) {
                        deduped.Data[++index] = sorted.Data[i];
                    }
                }

                sorted = deduped;
            }

            return new D<TK, TV>(sorted);
        }

        public static D<TK, TV> ToDMap<T, TK, TV>(this L<T> list, delegate*<T, P<TK, TV>> f)
                where T : unmanaged where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            return new D<TK, TV>(list.Map(f).SortBy(&P.Key));
        }

        public static Maybe<T> At<T>(this L<T> list, int index) where T : unmanaged {
            if ((index >= 0) && (index < list.Size)) {
                return Maybe<T>.Just(list.Data[index]);
            }
            return Maybe<T>.Nothing;
        }

        public static L<T> Replace<T>(this L<T> list, int index, T value) where T : unmanaged {
            if ((index >= 0) && (index < list.Size)) {
                var next = new L<T>(list);
                next.Data[index] = value;
                return next;
            }
            return list;
        }

        public static L<T> Insert<T>(this L<T> list, int index, T value) where T : unmanaged {
            index = Math.Max(0, Math.Min(list.Size, index));
            var next = new L<T>(list.Size + 1);
            if (index > 0) {
                var sz = index * L<T>.ElementSize;
                Buffer.MemoryCopy(list.Data, next.Data, sz, sz);
            }
            next.Data[index] = value;
            if (index < list.Size) {
                var sz = (list.Size - index) * L<T>.ElementSize;
                Buffer.MemoryCopy(list.Data + index, next.Data + index + 1, sz, sz);
            }
            return next;
        }

        public static L<T> Remove<T>(this L<T> list, int index) where T : unmanaged {
            if ((index >= 0) && (index < list.Size)) {
                var next = new L<T>(list.Size - 1);
                if (index > 0) {
                    var sz = index * L<T>.ElementSize;
                    Buffer.MemoryCopy(list.Data, next.Data, sz, sz);
                }
                if (index < list.Size) {
                    var sz = (list.Size - index - 1) * L<T>.ElementSize;
                    Buffer.MemoryCopy(list.Data + index + 1, next.Data + index, sz, sz);
                }
                return next;
            }
            return list;
        }

        public static Maybe<int> FindIndex<T>(this L<T> list, delegate*<T, bool> f)
                where T : unmanaged {
            return list.FindIndex(Cf.New(f));
        }

        public static Maybe<int> FindIndex<T>(this L<T> list, Cf<T, bool> f)
                where T : unmanaged {
            var e = list.Enumerator;
            var index = 0;
            while (e.MoveNext()) {
                if (f.Invoke(e.Current)) {
                    return Maybe<int>.Just(index);
                }
                index++;
            }
            return Maybe<int>.Nothing;
        }
    }
}