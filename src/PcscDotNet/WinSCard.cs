using System;
using System.Runtime.InteropServices;

namespace PcscDotNet
{
    public class WinSCard : IPcscProvider
    {
        public const string DllName = "WinSCard";

        [DllImport(DllName)]
        public unsafe static extern SCardError SCardEstablishContext(SCardScope dwScope, void* pvReserved1, void* pvReserved2, SCardContext* phContext);

        [DllImport(DllName)]
        public static extern SCardError SCardReleaseContext(SCardContext hContext);

        unsafe SCardError IPcscProvider.SCardEstablishContext(SCardScope dwScope, void* pvReserved1, void* pvReserved2, SCardContext* phContext)
        {
            return SCardEstablishContext(dwScope, pvReserved1, pvReserved2, phContext);
        }

        SCardError IPcscProvider.SCardReleaseContext(SCardContext hContext)
        {
            return SCardReleaseContext(hContext);
        }
    }
}