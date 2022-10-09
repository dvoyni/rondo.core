using System;
using System.Reflection;

namespace Rondo.Core.Memory {
    public interface IMemManager {
        int SizeOf<T>() where T : struct;
        int SizeOf(Type t);
        int GetFieldOffset(FieldInfo fieldInfo);
        unsafe void* Alloc(long size);
        unsafe void Free(void* mem);
        unsafe void MemCpy(void* src, void* dst, long size);
        unsafe bool MemCmp(void* a, void* b, long size);
        bool IsUnmanaged(Type t);
    }
}