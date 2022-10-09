using System;
using System.Text;
using Rondo.Core.Lib.Containers;

namespace Rondo.Core.Memory {
    public static unsafe partial class Serializer {
        internal delegate int SerializeDelegate(void* data, ref byte* buf, Info info);

        public static int Serialize<T>(T data, void* buffer) where T : unmanaged {
            var bytes = (byte*)buffer;
            return Serialize(&data, ref bytes);
        }

        private static int Serialize<T>(T* data, ref byte* buf) where T : unmanaged {
            return Serialize(typeof(T), data, ref buf);
        }

        private static int Serialize(Type type, void* data, ref byte* buf) {
            if (type.IsPrimitive || type.IsEnum) {
                var sz = Mem.SizeOf(type);
                if (buf != null) {
                    Buffer.MemoryCopy(data, buf, sz, sz);
                    buf += sz;
                }
                return sz;
            }

            var info = GetInfo(type);
            return info.Serialize(data, ref buf, info);
        }

        // ReSharper disable once UnusedParameter.Local
        private static int __LSerialize<T>(void* pL, ref byte* buf, Info _)
                where T : unmanaged {
            var size = 0;
            var l = *(L<T>*)pL;
            var e = l.Enumerator;
            while (e.MoveNext()) {
                size += BoolSerialize(true, ref buf);
                var c = e.Current;
                size += Serialize(&c, ref buf);
            }
            size += BoolSerialize(false, ref buf);
            return size;
        }

        // ReSharper disable once UnusedParameter.Local
        private static int __DSerialize<TK, TV>(void* pD, ref byte* buf, Info _)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            var d = *(D<TK, TV>*)pD;
            var x = d.Foldl(&DSerializeIterator, new DSerializeData(buf, 0));
            buf = x.Buf;
            return x.Size + BoolSerialize(false, ref buf);
        }

        private static byte[] _strBuffer = new byte[1024];

        // ReSharper disable once UnusedParameter.Local
        private static int __SSerialize(void* pS, ref byte* buf, Info _) {
            var s = (string)*(S*)pS;
            var len = Encoding.UTF8.GetByteCount(s);
            var sz = Serialize(&len, ref buf);
            if (len > 0) {
                var bufLen = _strBuffer.Length;
                while (bufLen < len) {
                    bufLen *= 2;
                }
                if (bufLen != _strBuffer.Length) {
                    _strBuffer = new byte[bufLen];
                }
                Encoding.UTF8.GetBytes(s, 0, s.Length, _strBuffer, 0);
                if (buf != null) {
                    fixed (byte* src = _strBuffer) {
                        Buffer.MemoryCopy(src, buf, len, len);
                    }
                    buf += len;
                }
            }

            return len + sz;
        }

        private static int __StructSerialize<T>(void* pStruct, ref byte* buf, Info info)
                where T : unmanaged {
            var size = 0;
            var sz = Mem.SizeOf<T>();
            if (buf != null) {
                Buffer.MemoryCopy(pStruct, buf, sz, sz);
                buf += sz;
            }
            size += sz;

            for (var i = 0; i < info.CollectionFields.Length; i++) {
                var f = info.CollectionFields[i];
                size += Serialize(f.Type, (byte*)pStruct + f.Offset, ref buf);
            }

            return size;
        }

        private static int BoolSerialize(bool v, ref byte* buf) {
            return Serialize(&v, ref buf);
        }

        private static DSerializeData DSerializeIterator<TK, TV>(TK k, TV v, DSerializeData d)
                where TK : unmanaged where TV : unmanaged {
            var buf = d.Buf;
            var size = d.Size;
            size += BoolSerialize(true, ref buf);
            size += Serialize(&k, ref buf);
            size += Serialize(&v, ref buf);
            return new DSerializeData(buf, size);
        }

        public readonly struct DSerializeData {
            public readonly byte* Buf;
            public readonly int Size;

            public DSerializeData(byte* buf, int size) {
                Buf = buf;
                Size = size;
            }
        }
    }
}