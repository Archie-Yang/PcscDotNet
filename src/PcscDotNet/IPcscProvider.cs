using System;
using System.Text;

namespace PcscDotNet
{
    public interface IPcscProvider
    {
        bool UseUnicode { get; }

        unsafe SCardError SCardEstablishContext(SCardScope dwScope, void* pvReserved1, void* pvReserved2, SCardContext* phContext);

        unsafe SCardError SCardFreeMemory(SCardContext hContext, void* pvMem);

        SCardError SCardIsValidContext(SCardContext hContext);

        unsafe SCardError SCardListReaders(SCardContext hContext, string mszGroups, void* mszReaders, int* pcchReaders);

        SCardError SCardReleaseContext(SCardContext hContext);
    }
}