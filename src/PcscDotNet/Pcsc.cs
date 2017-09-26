using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace PcscDotNet
{
    public class Pcsc
    {
        private static readonly char[] _nullCharacter = new char[] { '\0' };

        private readonly IPcscProvider _provider;

        public IPcscProvider Provider => _provider;

        public Pcsc(IPcscProvider provider)
        {
            _provider = provider;
        }

        public PcscContext CreateContext()
        {
            return new PcscContext(this);
        }

        public PcscContext EstablishContext(SCardScope scope)
        {
            return new PcscContext(this, scope);
        }

        public List<string> GetReaderNames(SCardReaderGroup group = SCardReaderGroup.NotSpecified)
        {
            return GetReaderNames(SCardContext.Default, group);
        }

        internal unsafe List<string> GetReaderNames(SCardContext handle, SCardReaderGroup group)
        {
            var readerNames = new List<string>();
            var provider = _provider;
            byte* pReaderNames;
            var charCount = PcscProvider.SCardAutoAllocate;
            var err = provider.SCardListReaders(handle, group.GetDefinedValue(), &pReaderNames, &charCount);
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
                           `Marshal.PtrToStringUni`: Copies the specific number of Unicode characters back.
                           `Marshal.PtrToStringAnsi`: Copies the specific byte count of ANSI string back.
                        */
                        var readerNamesString = provider.UseUnicode ? Marshal.PtrToStringUni((IntPtr)pReaderNames, charCount) : Marshal.PtrToStringAnsi((IntPtr)pReaderNames, charCount);
                        for (int offset = 0, offsetNull, length = readerNamesString.Length; ;)
                        {
                            if (offset >= length || (offsetNull = readerNamesString.IndexOf('\0', offset)) <= offset) break;
                            readerNames.Add(readerNamesString.Substring(offset, offsetNull - offset));
                            offset = offsetNull + 1;
                        }
                        break;
                    default:
                        err.Throw();
                        break;
                }
            }
            finally
            {
                if (pReaderNames != null) provider.SCardFreeMemory(handle, pReaderNames).ThrowIfNotSuccess();
            }
            return readerNames;
        }
    }
}
