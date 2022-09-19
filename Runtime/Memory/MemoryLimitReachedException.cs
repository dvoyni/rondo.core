using System;

namespace Rondo.Core.Memory {
    internal class MemoryLimitReachedException : Exception {
        public readonly int RequiredSize;

        public MemoryLimitReachedException(int requiredSize) {
            RequiredSize = requiredSize;
        }
    }
}