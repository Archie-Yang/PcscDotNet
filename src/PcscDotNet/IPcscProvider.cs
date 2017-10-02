using System;
using System.Collections.Generic;
using System.Text;

namespace PcscDotNet
{
    public interface IPcscProvider
    {
        byte[] AllocateReaderStates(int count);

        unsafe void* AllocateString(string value);

        unsafe string AllocateString(void* ptr, int length);

        unsafe void FreeString(void* ptr);

        unsafe void ReadReaderState(void* pReaderStates, int index, out void* pReaderName, out SCardReaderStates currentState, out SCardReaderStates eventState, out byte[] atr);

        SCardError SCardCancel(SCardContext hContext);

        unsafe SCardError SCardEstablishContext(SCardScope dwScope, void* pvReserved1, void* pvReserved2, SCardContext* phContext);

        unsafe SCardError SCardFreeMemory(SCardContext hContext, void* pvMem);

        unsafe SCardError SCardGetStatusChange(SCardContext hContext, int dwTimeout, void* rgReaderStates, int cReaders);

        SCardError SCardIsValidContext(SCardContext hContext);

        unsafe SCardError SCardListReaderGroups(SCardContext hContext, void* mszGroups, int* pcchGroups);

        unsafe SCardError SCardListReaders(SCardContext hContext, string mszGroups, void* mszReaders, int* pcchReaders);

        SCardError SCardReleaseContext(SCardContext hContext);

        unsafe void WriteReaderState(void* pReaderStates, int index, SCardReaderStates currentState);

        unsafe void WriteReaderState(void* pReaderStates, int index, void* pReaderName);
    }
}
