using System;
using System.Collections.Generic;

namespace PcscDotNet
{
    public class Pcsc
    {
        public IPcscProvider Provider { get; private set; }

        public Pcsc(IPcscProvider provider)
        {
            Provider = provider;
        }

        public PcscContext CreateContext()
        {
            return new PcscContext(Provider);
        }

        public PcscContext EstablishContext(SCardScope scope, PcscExceptionHandler onException = null)
        {
            return CreateContext().Establish(scope, onException);
        }

        public IEnumerable<string> GetReaderGroupNames(PcscExceptionHandler onException = null)
        {
            var groupNames = Provider.GetReaderGroupNames(SCardContext.Default, onException);
            if (groupNames == null) yield break;
            for (int offset = 0, offsetNull, length = groupNames.Length; ;)
            {
                if (offset >= length || (offsetNull = groupNames.IndexOf('\0', offset)) <= offset) yield break;
                yield return groupNames.Substring(offset, offsetNull - offset);
                offset = offsetNull + 1;
            }
        }

        public IEnumerable<string> GetReaderNames(SCardReaderGroup group = SCardReaderGroup.NotSpecified, PcscExceptionHandler onException = null)
        {
            return GetReaderNames(group.GetDefinedValue(), onException);
        }

        public IEnumerable<string> GetReaderNames(string group, PcscExceptionHandler onException = null)
        {
            var readerNames = Provider.GetReaderNames(SCardContext.Default, group, onException);
            if (readerNames == null) yield break;
            for (int offset = 0, offsetNull, length = readerNames.Length; ;)
            {
                if (offset >= length || (offsetNull = readerNames.IndexOf('\0', offset)) <= offset) yield break;
                yield return readerNames.Substring(offset, offsetNull - offset);
                offset = offsetNull + 1;
            }
        }
    }
}
