using Rondo.Core.Memory;

namespace Rondo.Core.Extras {
    public static class Access {
        public static void __DomainReload() {
            Serializer.__DomainReload();
            Mem.__DomainReload();
        }
    }
}