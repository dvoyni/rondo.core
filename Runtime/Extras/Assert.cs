using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Rondo.Core.Extras {
    public class AssertException : Exception {
        public AssertException(string message) : base(message) { }
    }

    public static class Assert {
        [Conditional("DEBUG")]
        public static void Fail(string message) {
            throw new AssertException(message);
        }

        [Conditional("DEBUG")]
        public static void That(bool condition, string message) {
            if (!condition) {
                Fail(message);
            }
        }

        [Conditional("DEBUG")]
        public static void NotNull(object obj, string message) {
            if (obj == null) {
                Fail(message);
            }
        }

        [Conditional("DEBUG")]
        public static unsafe void NotNull(void* obj, string message) {
            if (obj == null) {
                Fail(message);
            }
        }

        [Conditional("DEBUG")]
        public static void Contains<T>(IReadOnlyList<T> list, T item, string message) {
            NotNull(list, message);
            if (!list.Contains(item)) {
                Fail(message);
            }
        }

        [Conditional("DEBUG")]
        public static void DoesNotContain<T>(IReadOnlyList<T> list, T item, string message) {
            NotNull(list, message);
            if (list.Contains(item)) {
                Fail(message);
            }
        }

        [Conditional("DEBUG")]
        public static void ContainsKey<K, V>(IReadOnlyDictionary<K, V> dict, K key, string message) {
            NotNull(dict, message);
            if (!dict.ContainsKey(key)) {
                Fail(message);
            }
        }

        [Conditional("DEBUG")]
        public static void DoesNotContainKey<K, V>(IReadOnlyDictionary<K, V> dict, K key, string message) {
            NotNull(dict, message);
            if (dict.ContainsKey(key)) {
                Fail(message);
            }
        }

        [Conditional("DEBUG")]
        public static void Bounds<T>(ICollection<T> list, int x, string message) {
            if ((x < 0) || (x >= list.Count)) {
                Fail(message);
            }
        }

        [Conditional("DEBUG")]
        public static void Bounds<T>(T[,] array, int x, int y, string message) {
            if ((y < 0) || (y >= array.GetLength(0)) || (x < 0) || (x >= array.GetLength(1))) {
                Fail(message);
            }
        }

        [Conditional("DEBUG")]
        public static void All<T>(IEnumerable<T> items, Action<T> assert) {
            foreach (var item in items) {
                assert(item);
            }
        }
    }
}