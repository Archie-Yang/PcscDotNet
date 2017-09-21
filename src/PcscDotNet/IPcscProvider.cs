using System;
using System.Text;

namespace PcscDotNet
{
    public interface IPcscProvider
    {
        Encoding CharacterEncoding { get; }

        unsafe SCardError SCardEstablishContext(SCardScope dwScope, void* pvReserved1, void* pvReserved2, SCardContext* phContext);

        unsafe SCardError SCardFreeMemory(SCardContext hContext, void* pvMem);

        SCardError SCardIsValidContext(SCardContext hContext);

        SCardError SCardReleaseContext(SCardContext hContext);
    }
}