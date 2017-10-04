using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

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
            return new PcscContext(this);
        }

        public PcscContext EstablishContext(SCardScope scope)
        {
            return new PcscContext(this, scope);
        }

        public IEnumerable<string> GetReaderGroupNames()
        {
            var groupNames = GetReaderGroupNames(SCardContext.Default);
            if (groupNames == null) yield break;
            for (int offset = 0, offsetNull, length = groupNames.Length; ;)
            {
                if (offset >= length || (offsetNull = groupNames.IndexOf('\0', offset)) <= offset) yield break;
                yield return groupNames.Substring(offset, offsetNull - offset);
                offset = offsetNull + 1;
            }
        }

        public IEnumerable<string> GetReaderNames(SCardReaderGroup group = SCardReaderGroup.NotSpecified)
        {
            return GetReaderNames(group.GetDefinedValue());
        }

        public IEnumerable<string> GetReaderNames(string group)
        {
            var readerNames = GetReaderNames(SCardContext.Default, group);
            if (readerNames == null) yield break;
            for (int offset = 0, offsetNull, length = readerNames.Length; ;)
            {
                if (offset >= length || (offsetNull = readerNames.IndexOf('\0', offset)) <= offset) yield break;
                yield return readerNames.Substring(offset, offsetNull - offset);
                offset = offsetNull + 1;
            }
        }

        internal unsafe string GetReaderGroupNames(SCardContext handle)
        {
            byte* pGroupNames = null;
            var charCount = PcscProvider.SCardAutoAllocate;
            try
            {
                Provider.SCardListReaderGroups(handle, &pGroupNames, &charCount).ThrowIfNotSuccess();
                return Provider.AllocateString(pGroupNames, charCount);
            }
            finally
            {
                if (pGroupNames != null) Provider.SCardFreeMemory(handle, pGroupNames).ThrowIfNotSuccess();
            }
        }

        internal unsafe string GetReaderNames(SCardContext handle, string group)
        {
            string readerNames = null;
            byte* pReaderNames;
            var charCount = PcscProvider.SCardAutoAllocate;
            var err = Provider.SCardListReaders(handle, group, &pReaderNames, &charCount);
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
                        readerNames = Provider.AllocateString(pReaderNames, charCount);
                        break;
                    default:
                        err.Throw();
                        break;
                }
            }
            finally
            {
                if (pReaderNames != null) Provider.SCardFreeMemory(handle, pReaderNames).ThrowIfNotSuccess();
            }
            return readerNames;
        }
    }
}
