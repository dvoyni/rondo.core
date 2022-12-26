using System;
using System.Collections;
using System.Collections.Generic;

namespace Rondo.Core.Lib {
    public readonly struct Dict<TK, TV> : IReadOnlyDictionary<TK, TV> {
        // ReSharper disable once CollectionNeverUpdated.Local
        private static readonly Dictionary<TK, TV> _empty = new();

        private readonly Dictionary<TK, TV> _data;

        internal Dict(Dictionary<TK, TV> data) {
            _data = data;
        }

        public IEnumerator<KeyValuePair<TK, TV>> GetEnumerator() {
            return _data?.GetEnumerator() ?? _empty.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public int Count => _data?.Count ?? 0;

        public bool ContainsKey(TK key) {
            return _data?.ContainsKey(key) ?? false;
        }

        public bool TryGetValue(TK key, out TV value) {
            value = default;
            return _data?.TryGetValue(key, out value) ?? false;
        }

        public TV this[TK key] => _data[key];

        public IEnumerable<TK> Keys => ((IEnumerable<TK>)_data?.Keys) ?? Array.Empty<TK>();

        public IEnumerable<TV> Values => ((IEnumerable<TV>)_data?.Values) ?? Array.Empty<TV>();
    }
}