using System;

namespace PcscDotNet
{
    public static class Pcsc<T> where T : class, IPcscProvider, new()
    {
        private static readonly Pcsc _instance = new Pcsc(new T());

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