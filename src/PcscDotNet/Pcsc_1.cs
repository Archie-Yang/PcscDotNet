using System;
using System.Collections.Generic;

namespace PcscDotNet
{
    public static class Pcsc<TIPcscProvider> where TIPcscProvider : IPcscProvider, new()
    {
        private static readonly Pcsc _instance = new Pcsc(new TIPcscProvider());

        public static Pcsc Instance => _instance;

        public static PcscContext CreateContext()
        {
            return _instance.CreateContext();
        }

        public static PcscContext EstablishContext(SCardScope scope)
        {
            return _instance.EstablishContext(scope);
        }

        public static IEnumerable<string> GetReaderGroupNames()
        {
            return _instance.GetReaderGroupNames();
        }

        public static IEnumerable<string> GetReaderNames(SCardReaderGroup group = SCardReaderGroup.NotSpecified)
        {
            return _instance.GetReaderNames(group.GetDefinedValue());
        }

        public static IEnumerable<string> GetReaderNames(string group)
        {
            return _instance.GetReaderNames(group);
        }
    }
}
