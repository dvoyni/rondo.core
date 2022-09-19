using System;
using Rondo.Core.Lib.Containers;

namespace Rondo.Core.Memory {
    public static unsafe partial class Serializer {
        internal delegate void CloneDelegate(void* inData, void* outData, Info info);

        public static T Clone<T>(T data) where T : unmanaged {
            T result;
            Clone(typeof(T), &data, &result);
            return result;
        }

        private static void Clone(Type type, void* inData, void* outData) {
            if (type == typeof(Ptr)) {
                *(Ptr*)outData = Mem.C.CopyPtr(*(Ptr*)inData);
                return;
            }
            if (type.IsPrimitive || type.IsEnum) {
                var sz = Mem.SizeOf(type);
                Buffer.MemoryCopy(inData, outData, sz, sz);
                return;
            }

            var info = GetInfo(type);
            info.Clone(inData, outData, info);
        }

        // ReSharper disable once UnusedParameter.Local
        internal static void __LClone<T>(void* inL, void* outL, Info _)
                where T : unmanaged {
            *(L<T>*)outL = new L<T>(((L<T>*)inL)->Map(&Clone));
        }

        // ReSharper disable once UnusedParameter.Local
        internal static void __DClone<TK, TV>(void* inD, void* outD, Info _)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            static P<TK, TV> ClonePair(P<TK, TV> p) {
                return new P<TK, TV>(Clone(p.Key), Clone(p.Value));
            }

            *(D<TK, TV>*)outD = new D<TK, TV>(((D<TK, TV>*)inD)->Data.Map(&ClonePair));
        }

        // ReSharper disable once UnusedParameter.Local
        private static void __SClone(void* inS, void* outS, Info _) {
            *(S*)outS = *(S*)inS;
        }

        internal static void __StructClone<T>(void* inStruct, void* outStruct, Info info)
                where T : unmanaged {
            *(T*)outStruct = *(T*)inStruct;

            for (var i = 0; i < info.CollectionFields.Length; i++) {
                var f = info.CollectionFields[i];
                Clone(f.Type, (byte*)inStruct + f.Offset, (byte*)outStruct + f.Offset);
            }
        }
    }
}