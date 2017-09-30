using System;
using System.Collections.Generic;
using System.Text;

namespace PcscDotNet
{
    public interface IPcscProvider
    {
        PcscReaderStatus AllocateReaderStatus(PcscContext context, IList<string> readerNames);

        unsafe void* AllocateString(string value);

        unsafe string AllocateString(void* ptr, int length);

        unsafe void FreeString(void* ptr);

        SCardError SCardCancel(SCardContext hContext);

        unsafe SCardError SCardEstablishContext(SCardScope dwScope, void* pvReserved1, void* pvReserved2, SCardContext* phContext);

        unsafe SCardError SCardFreeMemory(SCardContext hContext, void* pvMem);

        unsafe SCardError SCardGetStatusChange(SCardContext hContext, int dwTimeout, void* rgReaderStates, int cReaders);

        SCardError SCardIsValidContext(SCardContext hContext);

        unsafe SCardError SCardListReaders(SCardContext hContext, string mszGroups, void* mszReaders, int* pcchReaders);

        SCardError SCardReleaseContext(SCardContext hContext);
    }
}
