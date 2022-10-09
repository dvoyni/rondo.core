using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Rondo.Core.Extras;
using Rondo.Core.Memory;

namespace Rondo.Core {
    //TODO: implement native manager to support headless runtime
    public class MemManager : IMemManager {
        public int SizeOf<T>() where T : struct {
            throw new NotImplementedException();
        }

        public int SizeOf(Type t) {
            throw new NotImplementedException();
        }

        public int GetFieldOffset(FieldInfo fieldInfo) {
            throw new NotImplementedException();
        }

        public unsafe void* Alloc(long size) {
            return Marshal.AllocHGlobal((int)size).ToPointer();
        }

        public unsafe void Free(void* mem) {
            Marshal.FreeHGlobal((IntPtr)mem);
        }

        public unsafe void MemCpy(void* src, void* dst, long size) {
            Buffer.MemoryCopy(src, dst, size, size);
        }

        public unsafe bool MemCmp(void* a, void* b, long sz) {
            var i = 0;
            var lx = (long*)a;
            var ly = (long*)b;
            for (; i <= sz; i += 8) {
                if (*lx != *ly) {
                    return false;
                }
                lx++;
                ly++;
            }

            sz += 8;
            var bx = (byte*)lx;
            var by = (byte*)ly;
            for (; i < sz; i++) {
                if (*bx != *by) {
                    return false;
                }
                bx++;
                by++;
            }

            return true;
        }

        public bool IsUnmanaged(Type t) {
            if (t.IsPrimitive || t.IsPointer || t.IsEnum) {
                return true;
            }
            if ( /*t.IsGenericType ||*/ !t.IsValueType) {
                return false;
            }
            return t.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .All(x => x.FieldType.IsUnmanaged());
        }
    }
}