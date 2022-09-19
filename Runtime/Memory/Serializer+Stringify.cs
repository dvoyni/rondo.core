using System;
using System.Globalization;
using System.Text;
using Rondo.Core.Lib.Containers;

namespace Rondo.Core.Memory {
    public interface IStringify {
        string Stringify(string offset);
    }

    public static unsafe partial class Serializer {
        public const string StringifyTab = "  ";
        internal delegate string StringifyDelegate(void* data, Info info, string offset);

        public static string Stringify<T>(T data) where T : unmanaged {
            return Stringify(&data);
        }

        private static string Stringify<T>(T* data, string offset = "") where T : unmanaged {
            return Stringify(typeof(T), data, offset);
        }

        private static string Stringify(Type type, void* data, string offset) {
            if (type.IsPrimitive) {
                if (type == typeof(bool)) {
                    return offset + (*(bool*)data).ToString(CultureInfo.InvariantCulture);
                }
                if (type == typeof(byte)) {
                    return offset + (*(byte*)data).ToString(CultureInfo.InvariantCulture);
                }
                if (type == typeof(sbyte)) {
                    return offset + (*(sbyte*)data).ToString(CultureInfo.InvariantCulture);
                }
                if (type == typeof(short)) {
                    return offset + (*(short*)data).ToString(CultureInfo.InvariantCulture);
                }
                if (type == typeof(ushort)) {
                    return offset + (*(ushort*)data).ToString(CultureInfo.InvariantCulture);
                }
                if (type == typeof(int)) {
                    return offset + (*(int*)data).ToString(CultureInfo.InvariantCulture);
                }
                if (type == typeof(uint)) {
                    return offset + (*(uint*)data).ToString(CultureInfo.InvariantCulture);
                }
                if (type == typeof(long)) {
                    return offset + (*(long*)data).ToString(CultureInfo.InvariantCulture);
                }
                if (type == typeof(ulong)) {
                    return offset + (*(ulong*)data).ToString(CultureInfo.InvariantCulture);
                }
                if (type == typeof(char)) {
                    return offset + (*(char*)data).ToString(CultureInfo.InvariantCulture);
                }
                if (type == typeof(double)) {
                    return offset + (*(double*)data).ToString(CultureInfo.InvariantCulture);
                }
                if (type == typeof(float)) {
                    return offset + (*(float*)data).ToString(CultureInfo.InvariantCulture);
                }
            }
            if (type.IsEnum) {
                //TODO: flags support
                var sValue = Stringify(Enum.GetUnderlyingType(type), data, "");
                var value = int.Parse(sValue);
                var names = Enum.GetNames(type);
                if (value < 0 || value >= names.Length) {
                    return offset + "<Undefined>";
                }
                return offset + names[value];
            }

            var info = GetInfo(type);
            return info.Stringify(data, info, offset);
        }

        // ReSharper disable once UnusedParameter.Local
        private static string __LStringify<T>(void* pL, Info _, string offset)
                where T : unmanaged {
            StringBuilder sb = new();
            sb.Append(offset).Append("[");
            var l = *(L<T>*)pL;
            var e = l.Enumerator;
            var o = offset + StringifyTab;
            var first = true;
            var simple = typeof(T).IsPrimitive || typeof(T).IsEnum;
            while (e.MoveNext()) {
                if (!first) {
                    sb.Append(",");
                }
                var c = e.Current;
                if (simple) {
                    if (!first) {
                        sb.Append(" ");
                    }
                    sb.Append(Stringify(&c, ""));
                }
                else {
                    sb.Append("\n").Append(Stringify(&c, o));
                }
                first = false;
            }

            if (!first && !simple) {
                sb.Append("\n").Append(offset);
            }
            sb.Append("]");
            return sb.ToString();
        }

        // ReSharper disable once UnusedParameter.Local
        private static string __DStringify<TK, TV>(void* pD, Info _, string offset)
                where TK : unmanaged, IComparable<TK> where TV : unmanaged {
            StringBuilder sb = new();
            sb.Append(offset).Append("{");
            var l = *(D<TK, TV>*)pD;
            var e = l.Enumerator;
            var o = offset + StringifyTab;
            var first = true;
            while (e.MoveNext()) {
                if (!first) {
                    sb.Append(",");
                }
                var c = e.Current;
                sb.Append("\n").Append(Stringify(&c.Key, o)).Append(": ").Append(Stringify(&c.Value, o).Substring(o.Length));
                first = false;
            }
            if (!first) {
                sb.Append("\n");
            }
            sb.Append(offset).Append("}");
            return sb.ToString();
        }

        // ReSharper disable once UnusedParameter.Local
        private static string __SStringify(void* pS, Info _, string offset) {
            return $"{offset}\"{(string)*(S*)pS}\"";
        }

        private static string __StructStringify<T>(void* pStruct, Info info, string offset)
                where T : unmanaged {
            if (typeof(IStringify).IsAssignableFrom(typeof(T))) {
                var c = *(T*)pStruct;
                return ((IStringify)c).Stringify(offset);
            }
            StringBuilder sb = new();
            sb.Append(offset).Append(typeof(T).Name).Append(" {");
            var o = offset + StringifyTab;
            for (var i = 0; i < info.Fields.Length; i++) {
                if (i > 0) {
                    sb.Append(",");
                }
                var f = info.Fields[i];
                sb.Append("\n").Append(o).Append(f.Name).Append(" = ");
                sb.Append(Stringify(f.Type, (byte*)pStruct + f.Offset, o).Substring(o.Length));
            }
            if (info.Fields.Length > 0) {
                sb.Append("\n");
            }
            sb.Append(offset).Append("}");
            return sb.ToString();
        }
    }
}