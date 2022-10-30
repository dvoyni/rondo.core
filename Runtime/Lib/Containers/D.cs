using System;
using Rondo.Core.Memory;

namespace Rondo.Core.Lib.Containers {
    public readonly unsafe struct D<TK, TV>
            where TK : unmanaged, IComparable<TK> where TV : unmanaged {
        internal readonly A<P<TK, TV>> Data;

        internal D(D<TK, TV> other) {
            Data = new A<P<TK, TV>>(other.Data);
        }

        internal D(A<P<TK, TV>> data) {
            Data = data;
        }

        public D(P<TK, TV> e0) {
            Data = new A<P<TK, TV>>(e0);
        }

        public D(P<TK, TV> e0, P<TK, TV> e1) {
            Data = new A<P<TK, TV>>(e0, e1).ToD().Data;
        }

        public D(P<TK, TV> e0, P<TK, TV> e1, P<TK, TV> e2) {
            Data = new A<P<TK, TV>>(e0, e1, e2).ToD().Data;
        }

        public D(P<TK, TV> e0, P<TK, TV> e1, P<TK, TV> e2, P<TK, TV> e3) {
            Data = new A<P<TK, TV>>(e0, e1, e2, e3).ToD().Data;
        }

        public D(P<TK, TV> e0, P<TK, TV> e1, P<TK, TV> e2, P<TK, TV> e3, P<TK, TV> e4) {
            Data = new A<P<TK, TV>>(e0, e1, e2, e3, e4).ToD().Data;
        }

        public D(P<TK, TV> e0, P<TK, TV> e1, P<TK, TV> e2, P<TK, TV> e3, P<TK, TV> e4, P<TK, TV> e5) {
            Data = new A<P<TK, TV>>(e0, e1, e2, e3, e4, e5).ToD().Data;
        }

        public D(P<TK, TV> e0, P<TK, TV> e1, P<TK, TV> e2, P<TK, TV> e3, P<TK, TV> e4, P<TK, TV> e5, P<TK, TV> e6) {
            Data = new A<P<TK, TV>>(e0, e1, e2, e3, e4, e5, e6).ToD().Data;
        }

        public D(P<TK, TV> e0, P<TK, TV> e1, P<TK, TV> e2, P<TK, TV> e3, P<TK, TV> e4, P<TK, TV> e5, P<TK, TV> e6, P<TK, TV> e7) {
            Data = new A<P<TK, TV>>(e0, e1, e1, e2, e3, e4, e5, e6, e7).ToD().Data;
        }

        public D(P<TK, TV> e0, P<TK, TV> e1, P<TK, TV> e2, P<TK, TV> e3, P<TK, TV> e4, P<TK, TV> e5, P<TK, TV> e6, P<TK, TV> e7, P<TK, TV> e8) {
            Data = new A<P<TK, TV>>(e0, e1, e2, e3, e4, e5, e6, e7, e8).ToD().Data;
        }

        public A<P<TK, TV>>.E Enumerator => Data.Enumerator;

#if DEBUG
        public override string ToString() {
            return Serializer.Stringify(this);
        }
#endif
    }

    public static unsafe class D {
        internal static void FindIndex<TK, TV>(this D<TK, TV> dict, TK key, out int index, out bool exists)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            //TODO: use separate-in-middle search, since dict.Data is sorted, to reduce complexity from O(N) to O(Log(N))
            var e = dict.Data.Enumerator;
            exists = false;
            index = 0;
            while (e.MoveNext()) {
                var d = e.Current.Key.CompareTo(key);
                if (d == 0) {
                    exists = true;
                    return;
                }
                if (d > 0) {
                    return;
                }
                index++;
            }
        }

        /// <summary>
        /// Create an empty dictionary.
        /// </summary>
        public static D<TK, TV> Empty<TK, TV>()
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            return new D<TK, TV>();
        }

        /// <summary>
        /// Insert a key-value pair into a dictionary. Replaces value when there is a collision.
        /// </summary>
        public static D<TK, TV> Insert<TK, TV>(this D<TK, TV> dict, P<TK, TV> e)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            dict.FindIndex(e.Key, out var index, out var exists);
            if (exists) {
                return new D<TK, TV>(dict.Data.Replace(index, e));
            }
            return new D<TK, TV>(dict.Data.Insert(index, e));
        }

        /// <summary>
        /// Insert a key-value pair into a dictionary. Replaces value when there is a collision.
        /// </summary>
        public static D<TK, TV> Insert<TK, TV>(this D<TK, TV> dict, TK key, TV value)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            return dict.Insert(new(key, value));
        }

        /// <summary>
        /// Update the value of a dictionary for a specific key with a given function.
        /// </summary>
        public static D<TK, TV> Update<TK, TV>(this D<TK, TV> dict, TK key, delegate*<Maybe<TV>, Maybe<TV>> f)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            return dict.Update(key, Cf.New(f));
        }

        public static D<TK, TV> Update<TK, TV>(this D<TK, TV> dict, TK key, Cf<Maybe<TV>, Maybe<TV>> f)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            if (f.Invoke(dict.Get(key)).Test(out var value)) {
                return dict.Insert(new(key, value));
            }
            return dict.Remove(key);
        }

        /// <summary>
        /// Remove a key-value pair from a dictionary. If the key is not found, no changes are made.
        /// </summary>
        public static D<TK, TV> Remove<TK, TV>(this D<TK, TV> dict, TK key)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            dict.FindIndex(key, out var index, out var exists);
            if (!exists) {
                return dict;
            }
            return new D<TK, TV>(dict.Data.Remove(index));
        }

        /// <summary>
        /// Determine if a dictionary is empty.
        /// </summary>
        public static bool IsEmpty<TK, TV>(D<TK, TV> dict)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            return dict.Data.Size == 0;
        }

        /// <summary>
        /// Determine if a key is in a dictionary.
        /// </summary>
        public static bool Member<TK, TV>(TK key, D<TK, TV> dict)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            dict.FindIndex(key, out _, out var exists);
            return exists;
        }

        /// <summary>
        /// Get the value associated with a key. If the key is not found, return Nothing.
        /// This is useful when you are not sure if a key will be in the dictionary.
        /// </summary>
        public static Maybe<TV> Get<TK, TV>(this D<TK, TV> dict, TK key)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            dict.FindIndex(key, out var index, out var exists);
            if (exists) {
                return Maybe<TV>.Just(dict.Data.Data[index].Value);
            }
            return Maybe<TV>.Nothing;
        }

        public static bool Member<TK, TV>(this D<TK, TV> dict, TK key)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            dict.FindIndex(key, out _, out var exists);
            return exists;
        }

        /// <summary>
        /// Determine the number of key-value pairs in the dictionary.
        /// </summary>
        public static int Length<TK, TV>(this D<TK, TV> dict)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            return dict.Data.Length();
        }

        /// <summary>
        /// Get all of the keys in a dictionary, sorted from lowest to highest.
        /// </summary>
        public static A<TK> Keys<TK, TV>(this D<TK, TV> dict)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            return dict.Data.Map(&P.Key);
        }

        /// <summary>
        /// Get all of the values in a dictionary, in the order of their keys.
        /// </summary>
        public static A<TV> Values<TK, TV>(this D<TK, TV> dict)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            return dict.Data.Map(&P.Value);
        }

        /// <summary>
        ///Convert a dictionary into an association list of key-value pairs, sorted by keys. 
        /// </summary>
        public static A<P<TK, TV>> ToL<TK, TV>(this D<TK, TV> dict)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            return new A<P<TK, TV>>(dict.Data);
        }

        /// <summary>
        ///Convert a dictionary into an association list with given map function, sorted by keys. 
        /// </summary>
        public static A<TR> ToLMap<TK, TV, TR>(this D<TK, TV> dict, delegate*<P<TK, TV>, TR> f)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged where TR : unmanaged {
            return dict.ToLMap(Cf.New(f));
        }

        public static A<TR> ToLMap<TK, TV, TR>(this D<TK, TV> dict, Cf<P<TK, TV>, TR> f)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged where TR : unmanaged {
            return dict.Data.Map(f);
        }

        /// <summary>
        /// Apply a function to all values in a dictionary.
        /// </summary>
        public static D<TK, TB> Map<TK, TA, TB>(this D<TK, TA> dict, delegate*<TK, TA, TB> f)
                where TK : unmanaged, IComparable<TK> where TA : unmanaged where TB : unmanaged {
            return dict.Map(Cf.New(f));
        }

        public static D<TK, TB> Map<TK, TA, TB>(this D<TK, TA> dict, Cf<TK, TA, TB> f)
                where TK : unmanaged, IComparable<TK> where TA : unmanaged where TB : unmanaged {
            var list = new A<P<TK, TB>>(dict.Data.Size);
            var index = 0;
            var e = dict.Enumerator;
            while (e.MoveNext()) {
                list.Data[index++] = new(e.Current.Key, f.Invoke(e.Current.Key, e.Current.Value));
            }
            return new D<TK, TB>(list);
        }

        /// <summary>
        /// Fold over the key-value pairs in a dictionary from lowest key to highest key.
        /// </summary>
        public static TR Foldl<TK, TV, TR>(this D<TK, TV> dict, delegate*<TK, TV, TR, TR> f, TR acc)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            return dict.Foldl(Cf.New(f), acc);
        }

        public static TR Foldl<TK, TV, TR>(this D<TK, TV> dict, Cf<TK, TV, TR, TR> f, TR acc)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            var e = dict.Enumerator;
            while (e.MoveNext()) {
                acc = f.Invoke(e.Current.Key, e.Current.Value, acc);
            }
            return acc;
        }

        /// <summary>
        /// Fold over the key-value pairs in a dictionary from highest key to lowest key.
        /// </summary>
        public static TR Foldr<TK, TV, TR>(this D<TK, TV> dict, delegate*<TK, TV, TR, TR> f, TR acc)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            return dict.Foldr(new Cf<TK, TV, TR, TR>(f), acc);
        }

        public static TR Foldr<TK, TV, TR>(this D<TK, TV> dict, Cf<TK, TV, TR, TR> f, TR acc)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            for (var i = dict.Data.Size - 1; i >= 0; i--) {
                var c = dict.Data.Data[i];
                acc = f.Invoke(c.Key, c.Value, acc);
            }
            return acc;
        }

        /// <summary>
        /// Keep only the key-value pairs that pass the given test.
        /// </summary>
        public static D<TK, TV> Filter<TK, TV>(this D<TK, TV> dict, delegate*<TK, TV, bool> f)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            return dict.Filter(Cf.New(f));
        }

        public static D<TK, TV> Filter<TK, TV>(this D<TK, TV> dict, Cf<TK, TV, bool> f)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            var size = 0;
            var e = dict.Enumerator;
            while (e.MoveNext()) {
                if (f.Invoke(e.Current.Key, e.Current.Value)) {
                    size++;
                }
            }
            var list = new A<P<TK, TV>>(size);
            if (size > 0) {
                var index = 0;
                e.Reset();
                while (e.MoveNext()) {
                    if (f.Invoke(e.Current.Key, e.Current.Value)) {
                        list.Data[index++] = e.Current;
                    }
                }
            }

            return new D<TK, TV>(list);
        }

        /// <summary>
        /// Partition a dictionary according to some test.
        /// The first dictionary contains all key-value pairs which passed the test,
        /// and the second contains the pairs that did not.
        /// </summary>
        public static (D<TK, TV>, D<TK, TV>) Partition<TK, TV>(this D<TK, TV> dict, delegate*<TK, TV, bool> f)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            return dict.Partition(Cf.New(f));
        }

        public static (D<TK, TV>, D<TK, TV>) Partition<TK, TV>(this D<TK, TV> dict, Cf<TK, TV, bool> f)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            var size = 0;
            var e = dict.Enumerator;
            while (e.MoveNext()) {
                if (f.Invoke(e.Current.Key, e.Current.Value)) {
                    size++;
                }
            }
            var a = new A<P<TK, TV>>(size);
            var b = new A<P<TK, TV>>(dict.Data.Size - size);

            var indexA = 0;
            var indexB = 0;
            e.Reset();
            while (e.MoveNext()) {
                if (f.Invoke(e.Current.Key, e.Current.Value)) {
                    a.Data[indexA++] = e.Current;
                }
                else {
                    b.Data[indexB++] = e.Current;
                }
            }

            return (new D<TK, TV>(a), new D<TK, TV>(b));
        }

        /// <summary>
        /// Combine two dictionaries. If there is a collision, preference is given to the first dictionary.
        /// </summary>
        public static D<TK, TV> Union<TK, TV>(this D<TK, TV> a, D<TK, TV> b)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            return (a.Data + b.Data).ToD();
        }

        /// <summary>
        /// Keep a key-value pair when its key appears in the second dictionary.
        /// Preference is given to values in the first dictionary.
        /// </summary>
        public static D<TK, TV> Intersect<TK, TV>(this D<TK, TV> a, D<TK, TV> b)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            var e = a.Enumerator;
            var cnt = 0;
            while (e.MoveNext()) {
                b.FindIndex(e.Current.Key, out _, out var exists);
                if (exists) {
                    cnt++;
                }
            }

            var list = new A<P<TK, TV>>(cnt);
            if (cnt > 0) {
                e.Reset();
                var index = 0;
                while (e.MoveNext()) {
                    b.FindIndex(e.Current.Key, out _, out var exists);
                    if (exists) {
                        list.Data[index++] = e.Current;
                    }
                }
            }
            return new D<TK, TV>(list);
        }

        /// <summary>
        /// Keep a key-value pair when its key does not appear in the second dictionary.
        /// </summary>
        public static D<TK, TV> Diff<TK, TV>(this D<TK, TV> a, D<TK, TV> b)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            var e = a.Enumerator;
            var cnt = 0;
            while (e.MoveNext()) {
                b.FindIndex(e.Current.Key, out _, out var exists);
                if (!exists) {
                    cnt++;
                }
            }

            var list = new A<P<TK, TV>>(cnt);
            if (cnt > 0) {
                e.Reset();
                var index = 0;
                while (e.MoveNext()) {
                    b.FindIndex(e.Current.Key, out _, out var exists);
                    if (!exists) {
                        list.Data[index++] = e.Current;
                    }
                }
            }
            return new D<TK, TV>(list);
        }

        /// <summary>
        /// The most general way of combining two dictionaries.
        /// You provide three accumulators for when a given key appears:
        /// 1. Only in the left dictionary.
        /// 2. In both dictionaries.
        /// 3. Only in the right dictionary.
        /// You then traverse all the keys from lowest to highest, building up whatever you want.
        /// </summary>
        public static T Merge<TK, TL, TR, T>(
            this D<TK, TL> leftDict,
            D<TK, TR> rightDict,
            delegate*<TK, TL, T, T> leftStep,
            delegate*<TK, TL, TR, T, T> bothStep,
            delegate*<TK, TR, T, T> rightStep,
            T acc)
                where TK : unmanaged, IComparable<TK> where TL : unmanaged where TR : unmanaged {
            return leftDict.Merge(rightDict, Cf.New(leftStep), Cf.New(bothStep), Cf.New(rightStep), acc);
        }

        public static T Merge<TK, TL, TR, T>(
            this D<TK, TL> leftDict,
            D<TK, TR> rightDict,
            Cf<TK, TL, T, T> leftStep,
            Cf<TK, TL, TR, T, T> bothStep,
            Cf<TK, TR, T, T> rightStep,
            T acc)
                where TK : unmanaged, IComparable<TK> where TL : unmanaged where TR : unmanaged {
            var el = leftDict.Enumerator;
            var er = rightDict.Enumerator;

            var hasLeft = el.MoveNext();
            var hasRight = er.MoveNext();
            while (true) {
                if (hasLeft && hasRight) {
                    var d = el.Current.Key.CompareTo(er.Current.Key);
                    if (d < 0) {
                        acc = leftStep.Invoke(el.Current.Key, el.Current.Value, acc);
                        hasLeft = el.MoveNext();
                    }
                    else if (d > 0) {
                        acc = rightStep.Invoke(er.Current.Key, er.Current.Value, acc);
                        hasRight = er.MoveNext();
                    }
                    else {
                        bothStep.Invoke(el.Current.Key, el.Current.Value, er.Current.Value, acc);
                        hasLeft = el.MoveNext();
                        hasRight = er.MoveNext();
                    }
                }
                else if (hasLeft) {
                    acc = leftStep.Invoke(el.Current.Key, el.Current.Value, acc);
                    hasLeft = el.MoveNext();
                }
                else if (hasRight) {
                    acc = rightStep.Invoke(er.Current.Key, er.Current.Value, acc);
                    hasRight = er.MoveNext();
                }
                else {
                    break;
                }
            }
            return acc;
        }
    }
}