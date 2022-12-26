using System;
using System.Collections;
using System.Collections.Generic;

namespace Rondo.Core.Lib {
    public readonly struct Arr<T> : IReadOnlyList<T> {
        private readonly T[] _data;

        internal Arr(T[] data) {
            _data = data;
        }

        public Arr(T a0) {
            _data = new[] { a0 };
        }

        public Arr(T a0, T a1) {
            _data = new[] { a0, a1 };
        }

        public Arr(T a0, T a1, T a2) {
            _data = new[] { a0, a1, a2 };
        }

        public Arr(T a0, T a1, T a2, T a3) {
            _data = new[] { a0, a1, a2, a3 };
        }

        public Arr(T a0, T a1, T a2, T a3, T a4) {
            _data = new[] { a0, a1, a2, a3, a4 };
        }

        public Arr(T a0, T a1, T a2, T a3, T a4, T a5) {
            _data = new[] { a0, a1, a2, a3, a4, a5 };
        }

        public Arr(T a0, T a1, T a2, T a3, T a4, T a5, T a6) {
            _data = new[] { a0, a1, a2, a3, a4, a5, a6 };
        }

        public Arr(T a0, T a1, T a2, T a3, T a4, T a5, T a6, T a7) {
            _data = new[] { a0, a1, a2, a3, a4, a5, a6, a7 };
        }

        public Arr(T a0, T a1, T a2, T a3, T a4, T a5, T a6, T a7, T a8) {
            _data = new[] { a0, a1, a2, a3, a4, a5, a6, a7, a8 };
        }

        public Arr(T a0, T a1, T a2, T a3, T a4, T a5, T a6, T a7, T a8, T a9) {
            _data = new[] { a0, a1, a2, a3, a4, a5, a6, a7, a8, a9 };
        }

        public IEnumerator<T> GetEnumerator() {
            var data = _data;
            if (data == null) {
                data = Array.Empty<T>();
            }
            return ((IEnumerable<T>)data).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public int Count => _data?.Length ?? 0;

        public T this[int index] => _data[index];

        public Arr<T> SetItem(int index, T item) {
            var copy = new T[_data.Length];
            Array.Copy(_data, copy, _data.Length);
            copy[index] = item;
            return new Arr<T>(copy);
        }

        public Arr<T> Add(T item) {
            if (Count == 0) {
                return new Arr<T>(item);
            }

            var copy = new T[_data.Length + 1];
            Array.Copy(_data, copy, _data.Length);
            copy[_data.Length] = item;
            return new Arr<T>(copy);
        }

        public Arr<T> Remove(T item) {
            if (Count == 0) {
                return this;
            }

            var index = Array.IndexOf(_data, item);
            if (index < 0) {
                return this;
            }

            var copy = new T[_data.Length - 1];
            Array.Copy(_data, 0, copy, 0, index);
            Array.Copy(_data, index + 1, copy, index, _data.Length - index - 1);
            return new Arr<T>(copy);
        }
    }
}