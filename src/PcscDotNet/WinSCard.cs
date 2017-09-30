using System;
using System.Collections.Generic;
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

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        public unsafe static extern SCardError SCardGetStatusChange(SCardContext hContext, int dwTimeout, SCardReaderState* rgReaderStates, int cReaders);

        [DllImport(DllName)]
        public static extern SCardError SCardIsValidContext(SCardContext hContext);

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        public unsafe static extern SCardError SCardListReaders(SCardContext hContext, string mszGroups, void* mszReaders, int* pcchReaders);

        [DllImport(DllName)]
        public static extern SCardError SCardReleaseContext(SCardContext hContext);

        PcscReaderStatus IPcscProvider.AllocateReaderStatus(PcscContext context, IList<string> readerNames)
        {
            return new PcscReaderStatus<SCardReaderState>(context, readerNames);
        }

        unsafe void* IPcscProvider.AllocateString(string value)
        {
            return (void*)Marshal.StringToHGlobalUni(value);
        }

        unsafe string IPcscProvider.AllocateString(void* ptr, int length)
        {
            return Marshal.PtrToStringUni((IntPtr)ptr, length);
        }

        unsafe void IPcscProvider.FreeString(void* ptr)
        {
            Marshal.FreeHGlobal((IntPtr)ptr);
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

        unsafe SCardError IPcscProvider.SCardGetStatusChange(SCardContext hContext, int dwTimeout, void* rgReaderStates, int cReaders)
        {
            return SCardGetStatusChange(hContext, dwTimeout, (SCardReaderState*)rgReaderStates, cReaders);
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
