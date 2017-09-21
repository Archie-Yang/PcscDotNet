namespace PcscDotNet
{
    public interface IPcscProvider
    {
        unsafe SCardError SCardEstablishContext(SCardScope dwScope, void* pvReserved1, void* pvReserved2, SCardContext* phContext);

        SCardError SCardIsValidContext(SCardContext hContext);

        SCardError SCardReleaseContext(SCardContext hContext);
    }
}