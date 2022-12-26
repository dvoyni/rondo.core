using System;
using System.Collections.Generic;

namespace Rondo.Core.Lib {
    public readonly struct Maybe<T> : IEquatable<Maybe<T>> {
        private readonly bool _assigned;
        private readonly T _value;

        private Maybe(T value) {
            _assigned = true;
            _value = value;
        }

        public bool Test(out T value) {
            value = _value;
            return _assigned;
        }

        public static Maybe<T> Nothing => new();

        public static Maybe<T> Just(T value) {
            return new Maybe<T>(value);
        }

        public TX Match<TX>(Func<T, TX> just, Func<TX> nothing) {
            return Test(out var v) ? just(v) : nothing();
        }

        public bool Equals(Maybe<T> other) {
            if (_assigned != other._assigned) {
                return false;
            }

            return EqualityComparer<T>.Default.Equals(_value, other._value);
        }
    }
}