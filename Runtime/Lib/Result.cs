using System;

namespace Rondo.Core.Lib {
    public readonly struct Result<TValue, TError> {
        private readonly TValue _value;
        private readonly TError _error;
        private readonly bool _isOk;

        public Result(TValue value): this(true, value, default) { }

        public Result(TError error) : this(false, default, error) { }

        internal Result(bool isOk, TValue result, TError error) {
            _isOk = isOk;
            _value = result;
            _error = error;
        }
        
        public bool Success(out TValue result, out TError error) {
            result = _value;
            error = _error;
            return _isOk;
        }

        public TX Match<TX>(Func<TValue, TX> onOk, Func<TError, TX> onError) {
            return _isOk ? onOk(_value) : onError(_error);
        }
    }

    public static class Result {
        public static Result<TValue, TError> Ok<TValue, TError>(TValue result) {
            return new Result<TValue, TError>(true, result, default);
        }

        public static Result<TValue, TError> Err<TValue, TError>(TError error) {
            return new Result<TValue, TError>(false, default, error);
        }

        public static Result<TValue, Exception> Ex<TValue>(Exception ex) {
            return Err<TValue, Exception>(ex);
        }
    }
}