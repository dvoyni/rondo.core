using System;

namespace Rondo.Core.Memory {
    public readonly struct Ref : IEquatable<Ref> {
        private readonly int _hash;

        public static Ref Null => new();

        internal Ref(int hash) {
            _hash = hash;
        }

        public bool Equals(Ref other) {
            return _hash == other._hash;
        }

        public override bool Equals(object obj) {
            return obj is Ref other && Equals(other);
        }

        public override int GetHashCode() {
            return _hash;
        }

#if DEBUG
        public override string ToString() {
            return $"{nameof(Ref)}(Hash:{_hash})";
        }
#endif
    }
}