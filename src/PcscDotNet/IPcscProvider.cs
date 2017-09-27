using System;
using System.Text;

namespace PcscDotNet
{
    public interface IPcscProvider
    {
        unsafe string AllocateString(void* ptr, int length);

        SCardError SCardCancel(SCardContext hContext);

        unsafe SCardError SCardEstablishContext(SCardScope dwScope, void* pvReserved1, void* pvReserved2, SCardContext* phContext);

        unsafe SCardError SCardFreeMemory(SCardContext hContext, void* pvMem);

        SCardError SCardIsValidContext(SCardContext hContext);

        unsafe SCardError SCardListReaders(SCardContext hContext, string mszGroups, void* mszReaders, int* pcchReaders);

        SCardError SCardReleaseContext(SCardContext hContext);
    }
}