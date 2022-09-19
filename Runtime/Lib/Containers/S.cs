using System;
using System.Collections.Generic;
using Rondo.Core.Extras;

namespace Rondo.Core.Lib.Containers {
    public readonly struct S : IEquatable<S>, IComparable<S> {
        private static readonly List<string> _toString = new() { "" };
        private static readonly Dictionary<string, int> _toHash = new() { { "", 0 } };

        private readonly int _hash;

        public static S Empty => new(0);

        private S(int hash) {
            _hash = hash;
        }

        public static explicit operator string(S s) {
            Assert.Bounds(_toString, s._hash, "String hash is invalid");
            return _toString[s._hash];
        }

        public static explicit operator S(string str) {
            if (str == null) {
                str = "";
            }

            if (!_toHash.TryGetValue(str, out var hash)) {
                hash = _toString.Count;
                _toString.Add(str);
                _toHash.Add(str, hash);
            }

            return new S(hash);
        }

        public bool Equals(S other) {
            return _hash == other._hash;
        }

        public override bool Equals(object obj) {
            return obj is S other && Equals(other);
        }

        public override int GetHashCode() {
            return _hash;
        }

        public static bool operator ==(S a, S b) {
            return a.Equals(b);
        }

        public static bool operator !=(S a, S b) {
            return !(a == b);
        }

        public int CompareTo(S other) {
            return _hash.CompareTo(other._hash);
        }

        public override string ToString() {
            return (string)this;
        }
    }
}