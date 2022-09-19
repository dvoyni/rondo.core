using System;
using Rondo.Core.Extras;
using Rondo.Core.Memory;

namespace Rondo.Core.Lib.Containers {
    public readonly unsafe struct WithError<T> : IStringify where T : unmanaged {
        private readonly T _result;
        private readonly S _error;

        public WithError(T result, S error) {
            _result = result;
            _error = error;
        }

        public static WithError<T> Ok(T result) {
            return new WithError<T>(result, S.Empty);
        }

        public static WithError<T> Error(string error) {
            Assert.That(!string.IsNullOrEmpty(error), "Error cannot be empty string");
            return new WithError<T>(default, (S)error);
        }

        public static WithError<T> Exception(Exception ex) {
            return Error($"{ex.Message}\n{ex.StackTrace}");
        }

        public bool Success(out T result, out string error) {
            result = _result;
            error = (string)_error;
            return _error == S.Empty;
        }

        public TX Match<TX>(delegate*<T, TX> success, delegate*<S, TX> error) {
            if (_error == S.Empty) {
                return error(_error);
            }
            return success(_result);
        }

        public void Match(delegate*<T, void> success, delegate*<S, void> error) {
            if (_error == S.Empty) {
                error(_error);
            }
            success(_result);
        }

        public string Stringify(string offset) {
            return Success(out var result, out var error)
                    ? $"{offset}Ok({Serializer.Stringify(result)})"
                    : $"{offset}Error({error})";
        }

#if DEBUG
        public override string ToString() {
            return Serializer.Stringify(this);
        }
#endif
    }
}