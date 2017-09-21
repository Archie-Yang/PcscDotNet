namespace PcscDotNet
{
    /// <summary>
    /// Extension method of SCardReaderGroup.
    /// </summary>
    public static class SCardReaderGroupExtensions
    {
        /// <summary>
        /// Defined value of SCardReaderGroup.All.
        /// </summary>
        private const string All = "SCard$AllReaders\000";

        /// <summary>
        /// Defined value of SCardReaderGroup.Default.
        /// </summary>
        private const string Default = "SCard$DefaultReaders\000";

        /// <summary>
        /// Defined value of SCardReaderGroup.Local.
        /// </summary>
        private const string Local = "SCard$LocalReaders\000";

        /// <summary>
        /// Defined value of SCardReaderGroup.System.
        /// </summary>
        private const string System = "SCard$SystemReaders\000";

        /// <summary>
        /// Gets defined value of SCardReaderGroup.
        /// </summary>
        /// <param name="group">SCardReaderGroup value.</param>
        /// <returns>Defined value.</returns>
        public static string GetDefinedValue(this SCardReaderGroup group)
        {
            switch (group)
            {
                case SCardReaderGroup.All:
                    return All;
                case SCardReaderGroup.Defualt:
                    return Default;
                case SCardReaderGroup.Local:
                    return Local;
                case SCardReaderGroup.System:
                    return System;
                default:
                    return null;
            }
        }
    }
}