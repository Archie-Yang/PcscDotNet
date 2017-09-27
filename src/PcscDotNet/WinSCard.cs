using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PcscDotNet
{
    public class WinSCard : IPcscProvider
    {
        /// <summary>
        /// The name of the DLL.
        /// </summary>
        public const string DllName = "WinSCard.dll";

        [DllImport(DllName)]
        public static extern SCardError SCardCancel(SCardContext hContext);

        [DllImport(DllName)]
        public unsafe static extern SCardError SCardEstablishContext(SCardScope dwScope, void* pvReserved1, void* pvReserved2, SCardContext* phContext);

        [DllImport(DllName)]
        public unsafe static extern SCardError SCardFreeMemory(SCardContext hContext, void* pvMem);

        [DllImport(DllName)]
        public static extern SCardError SCardIsValidContext(SCardContext hContext);

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        public unsafe static extern SCardError SCardListReaders(SCardContext hContext, string mszGroups, void* mszReaders, int* pcchReaders);

        [DllImport(DllName)]
        public static extern SCardError SCardReleaseContext(SCardContext hContext);

        unsafe string IPcscProvider.AllocateString(void* ptr, int length)
        {
            return Marshal.PtrToStringUni((IntPtr)ptr, length);
        }

        SCardError IPcscProvider.SCardCancel(SCardContext hContext)
        {
            return SCardCancel(hContext);
        }

        unsafe SCardError IPcscProvider.SCardEstablishContext(SCardScope dwScope, void* pvReserved1, void* pvReserved2, SCardContext* phContext)
        {
            return SCardEstablishContext(dwScope, pvReserved1, pvReserved2, phContext);
        }

        unsafe SCardError IPcscProvider.SCardFreeMemory(SCardContext hContext, void* pvMem)
        {
            return SCardFreeMemory(hContext, pvMem);
        }

        SCardError IPcscProvider.SCardIsValidContext(SCardContext hContext)
        {
            return SCardIsValidContext(hContext);
        }

        unsafe SCardError IPcscProvider.SCardListReaders(SCardContext hContext, string mszGroups, void* mszReaders, int* pcchReaders)
        {
            return SCardListReaders(hContext, mszGroups, mszReaders, pcchReaders);
        }

        SCardError IPcscProvider.SCardReleaseContext(SCardContext hContext)
        {
            return SCardReleaseContext(hContext);
        }
    }
}