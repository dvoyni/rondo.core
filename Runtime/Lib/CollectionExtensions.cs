using System;
using System.Collections.Generic;
using System.Linq;

namespace Rondo.Core.Lib {
    public static class CollectionExtensions {
        public static Maybe<T> GetFirst<T>(this IEnumerable<T> e) {
            return e.GetFirst(_ => true);
        }

        public static Maybe<T> GetFirst<T>(this IEnumerable<T> e, Func<T, bool> test) {
            foreach (var x in e) {
                if (test(x)) {
                    return Maybe<T>.Just(x);
                }
            }
            return Maybe<T>.Nothing;
        }

        public static Maybe<T> GetLast<T>(this IEnumerable<T> e) {
            return e.GetLast(_ => true);
        }

        public static Maybe<T> GetLast<T>(this IEnumerable<T> e, Func<T, bool> test) {
            if (e is IReadOnlyList<T> list) {
                for (var i = list.Count - 1; i >= 0; i--) {
                    if (test(list[i])) {
                        return Maybe<T>.Just(list[i]);
                    }
                }
                return Maybe<T>.Nothing;
            }

            foreach (var x in e.Reverse()) {
                if (test(x)) {
                    return Maybe<T>.Just(x);
                }
            }
            return Maybe<T>.Nothing;
        }

        public static Maybe<int> GetFirstIndex<T>(this IEnumerable<T> e, Func<T, bool> test) {
            var index = 0;
            foreach (var x in e) {
                if (test(x)) {
                    return Maybe<int>.Just(index);
                }
                index++;
            }
            return Maybe<int>.Nothing;
        }

        public static Maybe<int> GetLastIndex<T>(this IEnumerable<T> e, Func<T, bool> test) {
            if (e is IReadOnlyList<T> list) {
                for (var i = list.Count - 1; i >= 0; i--) {
                    if (test(list[i])) {
                        return Maybe<int>.Just(i);
                    }
                }
                return Maybe<int>.Nothing;
            }

            var index = 0;
            foreach (var x in e.Reverse()) {
                if (test(x)) {
                    return Maybe<int>.Just(index);
                }
                index++;
            }
            return Maybe<int>.Nothing;
        }

        public static Maybe<T> At<T>(this IEnumerable<T> e, int n) {
            if (n < 0) {
                return Maybe<T>.Nothing;
            }
            if (e is IReadOnlyList<T> list) {
                if (n >= list.Count) {
                    return Maybe<T>.Nothing;
                }
                return Maybe<T>.Just(list[n]);
            }
            if (e is IReadOnlyCollection<T> collection) {
                if (n >= collection.Count) {
                    return Maybe<T>.Nothing;
                }
            }
            var index = 0;
            foreach (var x in e) {
                if (index == n) {
                    return Maybe<T>.Just(x);
                }
                index++;
            }

            return Maybe<T>.Nothing;
        }

        public static IEnumerable<T> Update<T>(this IEnumerable<T> e, Func<T, Maybe<T>> f) {
            if (e is Arr<T> arr) {
                for (var i = 0; i < arr.Count; i++) {
                    var n = f(arr[i]);
                    if (n.Test(out var m)) {
                        arr = arr.SetItem(i, m);
                    }
                }
                return arr;
            }

            return e.Select(x => f(x).Match(x1 => x1, () => x));
        }

        public static IEnumerable<T1> SelectWhere<T, T1>(this IEnumerable<T> e, Func<T, Maybe<T1>> f) {
            return e.Select(f).FilterJust();
        }

        public static IEnumerable<T> FilterJust<T>(this IEnumerable<Maybe<T>> e) {
            return e.Where(x => x.Test(out _)).Select(x => {
                x.Test(out var n);
                return n;
            });
        }

        public static Arr<T> ToArr<T>(this IEnumerable<T> e) {
            return new Arr<T>(e.ToArray());
        }

        public static Dict<TK, TV> ToDict<T, TK, TV>(this IEnumerable<T> e, Func<T, TK> k, Func<T, TV> v) {
            return new Dict<TK, TV>(e.ToDictionary(k, v));
        }
    }
}