using System;
using System.Collections.Generic;

namespace PcscDotNet
{
    public static class Pcsc<TIPcscProvider> where TIPcscProvider : IPcscProvider, new()
    {
        public static Pcsc Instance { get; private set; } = new Pcsc(new TIPcscProvider());

        public static PcscContext CreateContext()
        {
            return Instance.CreateContext();
        }

        public static PcscContext EstablishContext(SCardScope scope)
        {
            return Instance.EstablishContext(scope);
        }

        public static IEnumerable<string> GetReaderGroupNames()
        {
            return Instance.GetReaderGroupNames();
        }

        public static IEnumerable<string> GetReaderNames(SCardReaderGroup group = SCardReaderGroup.NotSpecified)
        {
            return Instance.GetReaderNames(group.GetDefinedValue());
        }

        public static IEnumerable<string> GetReaderNames(string group)
        {
            return Instance.GetReaderNames(group);
        }
    }
}
