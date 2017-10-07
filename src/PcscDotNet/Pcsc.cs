using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace PcscDotNet
{
    public class Pcsc
    {
        public IPcscProvider Provider { get; private set; }

        internal unsafe static string GetReaderGroupNames(IPcscProvider provider, SCardContext handle, PcscExceptionHandler onException)
        {
            byte* pGroupNames = null;
            var charCount = PcscProvider.SCardAutoAllocate;
            try
            {
                provider.SCardListReaderGroups(handle, &pGroupNames, &charCount).ThrowIfNotSuccess(onException);
                return provider.AllocateString(pGroupNames, charCount);
            }
            finally
            {
                if (pGroupNames != null) provider.SCardFreeMemory(handle, pGroupNames).ThrowIfNotSuccess(onException);
            }
        }

        internal unsafe static string GetReaderNames(IPcscProvider provider, SCardContext handle, string group, PcscExceptionHandler onException)
        {
            string readerNames = null;
            byte* pReaderNames;
            var charCount = PcscProvider.SCardAutoAllocate;
            var err = provider.SCardListReaders(handle, group, &pReaderNames, &charCount);
            try
            {
                switch (err)
                {
                    case SCardError.NoReadersAvailable:
                        // In Windows, it seems to still return a `NULL` character with `SCardError.Success` status even none of reader names is found.
                        break;
                    case SCardError.Successs:
                        /*
                           Providers can use ANSI (e.g., WinSCard and pcsc-lite) or Unicode (e.g., WinSCard) for the encoding of characters.
                           In ANSI, `charCount` means the byte count of string.
                           In Unicode, `charCount` means the number of Unicode characters of string.
                        */
                        readerNames = provider.AllocateString(pReaderNames, charCount);
                        break;
                    default:
                        err.Throw(onException);
                        break;
                }
            }
            finally
            {
                if (pReaderNames != null) provider.SCardFreeMemory(handle, pReaderNames).ThrowIfNotSuccess(onException);
            }
            return readerNames;
        }

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
            var groupNames = GetReaderGroupNames(Provider, SCardContext.Default, onException);
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
            var readerNames = GetReaderNames(Provider, SCardContext.Default, group, onException);
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
