namespace PcscDotNet
{
    public interface IPcscProvider
    {
        byte[] AllocateIORequest(int informationLength);

        byte[] AllocateReaderStates(int count);

        unsafe void* AllocateString(string value);

        unsafe string AllocateString(void* ptr, int length);

        unsafe void FreeString(void* ptr);

        unsafe void ReadIORequest(void* pIORequest, out SCardProtocols protocol, out byte[] information);

        unsafe void ReadReaderState(void* pReaderStates, int index, out void* pReaderName, out SCardReaderStates currentState, out SCardReaderStates eventState, out byte[] atr);

        SCardError SCardBeginTransaction(SCardHandle hCard);

        SCardError SCardCancel(SCardContext hContext);

        unsafe SCardError SCardConnect(SCardContext hContext, string szReader, SCardShare dwShareMode, SCardProtocols dwPreferredProtocols, SCardHandle* phCard, SCardProtocols* pdwActiveProtocol);

        unsafe SCardError SCardControl(SCardHandle hCard, int dwControlCode, void* lpInBuffer, int nInBufferSize, void* lpOutBuffer, int nOutBufferSize, int* lpBytesReturned);

        int SCardCtlCode(SCardControlFunction code);

        SCardError SCardDisconnect(SCardHandle hCard, SCardDisposition dwDisposition);

        SCardError SCardEndTransaction(SCardHandle hCard, SCardDisposition dwDisposition);

        unsafe SCardError SCardEstablishContext(SCardScope dwScope, void* pvReserved1, void* pvReserved2, SCardContext* phContext);

        unsafe SCardError SCardFreeMemory(SCardContext hContext, void* pvMem);

        unsafe SCardError SCardGetStatusChange(SCardContext hContext, int dwTimeout, void* rgReaderStates, int cReaders);

        SCardError SCardIsValidContext(SCardContext hContext);

        unsafe SCardError SCardListReaderGroups(SCardContext hContext, void* mszGroups, int* pcchGroups);

        unsafe SCardError SCardListReaders(SCardContext hContext, string mszGroups, void* mszReaders, int* pcchReaders);

        unsafe SCardError SCardReconnect(SCardHandle hCard, SCardShare dwShareMode, SCardProtocols dwPreferredProtocols, SCardDisposition dwInitialization, SCardProtocols* pdwActiveProtocol);

        SCardError SCardReleaseContext(SCardContext hContext);

        unsafe SCardError SCardTransmit(SCardHandle hCard, void* pioSendPci, byte* pbSendBuffer, int cbSendLength, void* pioRecvPci, byte* pbRecvBuffer, int* pcbRecvLength);

        unsafe void WriteIORequest(void* pIORequest, SCardProtocols protocol, int totalLength, byte[] information);

        unsafe void WriteReaderState(void* pReaderStates, int index, SCardReaderStates currentState);

        unsafe void WriteReaderState(void* pReaderStates, int index, void* pReaderName);
    }
}
