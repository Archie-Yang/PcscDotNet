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

        public static PcscContext EstablishContext(SCardScope scope, PcscExceptionHandler onException = null)
        {
            return Instance.EstablishContext(scope, onException);
        }

        public static IEnumerable<string> GetReaderGroupNames(PcscExceptionHandler onException = null)
        {
            return Instance.GetReaderGroupNames(onException);
        }

        public static IEnumerable<string> GetReaderNames(SCardReaderGroup group = SCardReaderGroup.NotSpecified, PcscExceptionHandler onException = null)
        {
            return Instance.GetReaderNames(group.GetDefinedValue(), onException);
        }

        public static IEnumerable<string> GetReaderNames(string group, PcscExceptionHandler onException = null)
        {
            return Instance.GetReaderNames(group, onException);
        }
    }
}
