using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Rondo.Core.Extras {
    public static class TypeExtension {
        private static readonly Dictionary<Type, bool> _cachedTypes = new();

        public static bool IsUnmanaged(this Type t) {
            if (_cachedTypes.TryGetValue(t, out var result)) {
                return result;
            }

            if (t.IsPrimitive || t.IsPointer || t.IsEnum) {
                result = true;
            }
            else if ( /*t.IsGenericType ||*/ !t.IsValueType || t.IsGenericParameter) {
                result = false;
            }
            else {
                result = t.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                        .All(x => x.FieldType.IsUnmanaged());
            }

            _cachedTypes.Add(t, result);
            return result;
        }
    }
}