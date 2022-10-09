using System;
using System.Collections.Generic;

namespace Rondo.Core.Memory {
    public readonly struct Ts : IEquatable<Ts>, IComparable<Ts> {
        private static readonly List<Type> _types = new() { null };
        private static readonly Dictionary<Type, Ts> _typeToHashed = new();

        private readonly int _hash;
        public readonly int Size;

        internal Ts(int hash, int size) {
            _hash = hash;
            Size = size;
        }

        public static explicit operator Ts(Type type) {
            if (!_typeToHashed.TryGetValue(type, out var th)) {
                var size = Mem.SizeOf(type);
                th = new Ts(_types.Count, size);

                _types.Add(type);
                _typeToHashed.Add(type, th);
            }

            return th;
        }

        public static Ts OfUnmanaged(Type type) {
            if (!_typeToHashed.TryGetValue(type, out var th)) {
                th = new Ts(_types.Count, 0);
                _types.Add(type);
                _typeToHashed.Add(type, th);
            }
            return th;
        }

        public static explicit operator Type(Ts ts) {
            return _types[ts.GetHashCode()];
        }

        public bool Equals(Ts other) {
            return _hash == other._hash;
        }

        public override bool Equals(object obj) {
            return obj is Ts other && Equals(other);
        }

        public override int GetHashCode() {
            return _hash;
        }

        public static bool operator ==(Ts a, Ts b) {
            return a.Equals(b);
        }

        public static bool operator !=(Ts a, Ts b) {
            return !(a == b);
        }

        public int CompareTo(Ts other) {
            return _hash.CompareTo(other._hash);
        }

#if DEBUG
        public override string ToString() {
            return $"{nameof(Ts)}(Hash:{_hash}, Size:{Size}, Value:{(Type)this})";
        }
#endif
    }
}