using System;

namespace PcscDotNet
{
    public static class Pcsc<TIPcscProvider> where TIPcscProvider : class, IPcscProvider, new()
    {
        private static readonly Pcsc _instance = new Pcsc(new TIPcscProvider());

        public static Pcsc Instance => _instance;

        public static PcscContext CreateContext()
        {
            return new PcscContext(_instance);
        }

        public static PcscContext EstablishContext(SCardScope scope)
        {
            return new PcscContext(_instance, scope);
        }
    }
}