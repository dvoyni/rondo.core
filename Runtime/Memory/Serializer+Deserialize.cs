using System;
using System.Text;
using Rondo.Core.Lib.Containers;

namespace Rondo.Core.Memory {
    public static unsafe partial class Serializer {
        internal delegate void DeserializeDelegate(void* data, ref byte* buf, Info info);

        public static WithError<T> Deserialize<T>(void* buf) where T : unmanaged {
            try {
                T value = default;
                var bytes = (byte*)buf;
                Deserialize(&value, ref bytes);
                return WithError<T>.Ok(value);
            }
            catch (Exception ex) {
                return WithError<T>.Exception(ex);
            }
        }

        private static void Deserialize<T>(T* value, ref byte* buf) where T : unmanaged {
            var ptr = (void*)value;
            Deserialize(typeof(T), ptr, ref buf);
        }

        private static void Deserialize(Type type, void* value, ref byte* buf) {
            if (type.IsPrimitive || type.IsEnum) {
                var sz = Mem.SizeOf(type);
                Buffer.MemoryCopy(buf, value, sz, sz);
                buf += sz;
                return;
            }

            var info = GetInfo(type);
            info.Deserialize(value, ref buf, info);
        }

        // ReSharper disable once UnusedParameter.Local
        private static void __LDeserialize<T>(void* pL, ref byte* buf, Info _)
                where T : unmanaged {
            var l = new L<T>();
            while (BoolDeserialize(ref buf)) {
                T value;
                Deserialize(&value, ref buf);
                l += value;
            }
            *(L<T>*)pL = l;
        }

        // ReSharper disable once UnusedParameter.Local
        private static void __DDeserialize<TK, TV>(void* pD, ref byte* buf, Info _)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            var d = new D<TK, TV>();

            while (BoolDeserialize(ref buf)) {
                TK key;
                Deserialize(&key, ref buf);
                TV value;
                Deserialize(&value, ref buf);
                d = d.Insert(key, value);
            }

            *(D<TK, TV>*)pD = d;
        }

        // ReSharper disable once UnusedParameter.Local
        private static void __SDeserialize(void* pS, ref byte* buf, Info _) {
            var len = 0;
            Deserialize(&len, ref buf);
            if (len > 0) {
                var bufLen = _strBuffer.Length;
                while (bufLen < len) {
                    bufLen *= 2;
                }
                if (bufLen != _strBuffer.Length) {
                    _strBuffer = new byte[bufLen];
                }

                fixed (byte* dst = _strBuffer) {
                    Buffer.MemoryCopy(buf, dst, len, len);
                    buf += len;
                    *(S*)pS = (S)Encoding.UTF8.GetString(dst, len);
                }
            }
            else {
                *(S*)pS = S.Empty;
            }
        }

        private static void __StructDeserialize<T>(void* pStruct, ref byte* buf, Info info)
                where T : unmanaged {
            var sz = Mem.SizeOf<T>();
            Buffer.MemoryCopy(buf, pStruct, sz, sz);
            buf += sz;

            for (var i = 0; i < info.CollectionFields.Length; i++) {
                var f = info.CollectionFields[i];
                Deserialize(f.Type, (byte*)pStruct + f.Offset, ref buf);
            }
        }

        private static bool BoolDeserialize(ref byte* buf) {
            var b = false;
            Deserialize(&b, ref buf);
            return b;
        }
    }
}