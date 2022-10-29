using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Rondo.Core.Lib.Containers;

namespace Rondo.Core.Memory {
    public static unsafe partial class Serializer {
        private static readonly Dictionary<Type, Info> _infos = new();

        internal static void __DomainReload() {
            _infos.Clear();
        }

        public static void __ProduceGenericL<T>() where T : unmanaged {
            byte* buf = null;
            __LSerialize<T>(null, ref buf, default);
            __LDeserialize<T>(null, ref buf, default);
            __LStringify<T>(null, default, "");
            __LClone<T>(null, null, default);
        }

        public static void __ProduceGenericD<TK, TV>()
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            byte* buf = null;
            __DSerialize<TK, TV>(null, ref buf, default);
            __DDeserialize<TK, TV>(null, ref buf, default);
            __DStringify<TK, TV>(null, default, "");
            __DClone<TK, TV>(null, null, default);
        }

        public static void __ProduceGenericStruct<T>() where T : unmanaged {
            byte* buf = null;
            __StructSerialize<T>(null, ref buf, default);
            __StructDeserialize<T>(null, ref buf, default);
            __StructStringify<T>(null, default, "");
            __StructClone<T>(null, null, default);
        }

        public static int GetSize<T>(T data) where T : unmanaged {
            byte* buf = null;
            return Serialize(&data, ref buf);
        }

        private static Info GetInfo(Type type) {
            if (!_infos.TryGetValue(type, out var ds)) {
                var collection = false;
                if (type.IsGenericType) {
                    var genericType = type.GetGenericTypeDefinition();
                    if (genericType == typeof(L<>)) {
                        ds = new(
                            (SerializeDelegate)typeof(Serializer)
                                    .GetMethod(nameof(__LSerialize), BindingFlags.Static | BindingFlags.NonPublic)!
                                    .MakeGenericMethod(type.GetGenericArguments())
                                    .CreateDelegate(typeof(SerializeDelegate)),
                            (DeserializeDelegate)typeof(Serializer)
                                    .GetMethod(nameof(__LDeserialize), BindingFlags.Static | BindingFlags.NonPublic)!
                                    .MakeGenericMethod(type.GetGenericArguments())
                                    .CreateDelegate(typeof(DeserializeDelegate)),
                            (StringifyDelegate)typeof(Serializer)
                                    .GetMethod(nameof(__LStringify), BindingFlags.Static | BindingFlags.NonPublic)!
                                    .MakeGenericMethod(type.GetGenericArguments())
                                    .CreateDelegate(typeof(StringifyDelegate)),
                            (CloneDelegate)typeof(Serializer)
                                    .GetMethod(nameof(__LClone), BindingFlags.Static | BindingFlags.NonPublic)!
                                    .MakeGenericMethod(type.GetGenericArguments())
                                    .CreateDelegate(typeof(CloneDelegate))
                        );
                        collection = true;
                    }
                    else if (genericType == typeof(D<,>)) {
                        ds = new(
                            (SerializeDelegate)typeof(Serializer)
                                    .GetMethod(nameof(__DSerialize), BindingFlags.Static | BindingFlags.NonPublic)!
                                    .MakeGenericMethod(type.GetGenericArguments())
                                    .CreateDelegate(typeof(SerializeDelegate)),
                            (DeserializeDelegate)typeof(Serializer)
                                    .GetMethod(nameof(__DDeserialize), BindingFlags.Static | BindingFlags.NonPublic)!
                                    .MakeGenericMethod(type.GetGenericArguments())
                                    .CreateDelegate(typeof(DeserializeDelegate)),
                            (StringifyDelegate)typeof(Serializer)
                                    .GetMethod(nameof(__DStringify), BindingFlags.Static | BindingFlags.NonPublic)!
                                    .MakeGenericMethod(type.GetGenericArguments())
                                    .CreateDelegate(typeof(StringifyDelegate)),
                            (CloneDelegate)typeof(Serializer)
                                    .GetMethod(nameof(__DClone), BindingFlags.Static | BindingFlags.NonPublic)!
                                    .MakeGenericMethod(type.GetGenericArguments())
                                    .CreateDelegate(typeof(CloneDelegate))
                        );
                        collection = true;
                    }
                }
                if (type == typeof(S)) {
                    ds = new(
                        (SerializeDelegate)typeof(Serializer)
                                .GetMethod(nameof(__SSerialize), BindingFlags.Static | BindingFlags.NonPublic)!
                                .CreateDelegate(typeof(SerializeDelegate)),
                        (DeserializeDelegate)typeof(Serializer)
                                .GetMethod(nameof(__SDeserialize), BindingFlags.Static | BindingFlags.NonPublic)!
                                .CreateDelegate(typeof(DeserializeDelegate)),
                        (StringifyDelegate)typeof(Serializer)
                                .GetMethod(nameof(__SStringify), BindingFlags.Static | BindingFlags.NonPublic)!
                                .CreateDelegate(typeof(StringifyDelegate)),
                        (CloneDelegate)typeof(Serializer)
                                .GetMethod(nameof(__SClone), BindingFlags.Static | BindingFlags.NonPublic)!
                                .CreateDelegate(typeof(CloneDelegate))
                    );
                    collection = true;
                }
                if (!collection) {
                    ds = new(
                        (SerializeDelegate)typeof(Serializer)
                                .GetMethod(nameof(__StructSerialize), BindingFlags.Static | BindingFlags.NonPublic)!
                                .MakeGenericMethod(type)
                                .CreateDelegate(typeof(SerializeDelegate)),
                        (DeserializeDelegate)typeof(Serializer)
                                .GetMethod(nameof(__StructDeserialize), BindingFlags.Static | BindingFlags.NonPublic)!
                                .MakeGenericMethod(type)
                                .CreateDelegate(typeof(DeserializeDelegate)),
                        (StringifyDelegate)typeof(Serializer)
                                .GetMethod(nameof(__StructStringify), BindingFlags.Static | BindingFlags.NonPublic)!
                                .MakeGenericMethod(type)
                                .CreateDelegate(typeof(StringifyDelegate)),
                        (CloneDelegate)typeof(Serializer)
                                .GetMethod(nameof(__StructClone), BindingFlags.Static | BindingFlags.NonPublic)!
                                .MakeGenericMethod(type)
                                .CreateDelegate(typeof(CloneDelegate)),
                        type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                                .Where(IsSerializable)
                                .Select(fi => new CollectionField(
                                    fi.Name, fi.FieldType, Mem.OffsetOf(fi)
                                ))
                                .ToArray(),
                        type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                                .Where(IsCollection)
                                .Select(fi => new CollectionField(
                                    fi.Name, fi.FieldType, Mem.OffsetOf(fi)
                                ))
                                .ToArray()
                    );
                }
                _infos[type] = ds;
            }
            return ds;
        }

        private static bool IsCollection(FieldInfo fi) {
            if (fi.FieldType.IsGenericType) {
                if (fi.FieldType.GetGenericTypeDefinition() == typeof(L<>)) {
                    return true;
                }
                if (fi.FieldType.GetGenericTypeDefinition() == typeof(D<,>)) {
                    return true;
                }
            }
            if (!fi.FieldType.IsPrimitive && fi.FieldType.IsValueType && !fi.FieldType.IsEnum) {
                return true;
            }
            return false;
        }

        private static bool IsSerializable(FieldInfo fi) {
            return fi.FieldType.IsPrimitive || fi.FieldType.IsEnum || IsCollection(fi);
        }

        internal readonly struct Info {
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public readonly SerializeDelegate Serialize;
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public readonly DeserializeDelegate Deserialize;
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public readonly StringifyDelegate Stringify;
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public readonly CloneDelegate Clone;
            public readonly CollectionField[] CollectionFields;
            public readonly CollectionField[] Fields;

            public Info(
                SerializeDelegate serialize,
                DeserializeDelegate deserialize,
                StringifyDelegate stringify,
                CloneDelegate clone,
                CollectionField[] fields = null,
                CollectionField[] collectionFields = null
            ) {
                Serialize = serialize;
                Deserialize = deserialize;
                Stringify = stringify;
                Clone = clone;
                Fields = fields;
                CollectionFields = collectionFields;
            }
        }

        internal readonly struct CollectionField {
            public readonly string Name;
            public readonly Type Type;
            public readonly int Offset;

            public CollectionField(string name, Type type, int offset) {
                Name = name;
                Type = type;
                Offset = offset;
            }
        }
    }
}