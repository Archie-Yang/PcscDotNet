namespace PcscDotNet
{
    /// <summary>
    /// Usage shared for IPcscProvider.
    /// </summary>
    public static class PcscProvider
    {
        /// <summary>
        /// Length designator for auto-allocation memory from the Smart Card Resource Manager.
        /// </summary>
        public const int SCardAutoAllocate = -1;

        public unsafe static string GetReaderGroupNames(IPcscProvider provider, SCardContext handle, PcscExceptionHandler onException)
        {
            byte* pGroupNames = null;
            var charCount = SCardAutoAllocate;
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

        public unsafe static string GetReaderNames(IPcscProvider provider, SCardContext handle, string group, PcscExceptionHandler onException)
        {
            string readerNames = null;
            byte* pReaderNames;
            var charCount = SCardAutoAllocate;
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
    }
}
