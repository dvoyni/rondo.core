using System;
using System.Collections.Generic;
using Rondo.Core.Memory;

namespace Rondo.Core.Extras {
    public static class TypeExtension {
        private static readonly Dictionary<Type, bool> _cachedTypes = new();

        public static bool IsUnmanaged(this Type t) {
            if (_cachedTypes.TryGetValue(t, out var result)) {
                return result;
            }
            result = Mem.Manager.IsUnmanaged(t);
            _cachedTypes.Add(t, result);
            return result;
        }
    }
}