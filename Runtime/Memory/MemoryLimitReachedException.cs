using System;

namespace Rondo.Core.Memory {
    public class MemoryLimitReachedException : Exception {
        public readonly int RequiredSize;

        public MemoryLimitReachedException(int requiredSize) {
            RequiredSize = requiredSize;
        }
    }
}