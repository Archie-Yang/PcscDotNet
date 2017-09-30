using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace PcscDotNet
{
    public sealed class PcscReaderStatus<TIScardReaderState> : PcscReaderStatus where TIScardReaderState : struct, ISCardReaderState
    {
        private readonly TIScardReaderState[] _readerStates;

        private unsafe static void CopyAndFree(IPcscProvider provider, ref TIScardReaderState src, PcscReaderState dest)
        {
            var state = src.EventState;
            src.CurrentState = state;
            dest.Atr = src.Atr;
            dest.EventNumber = ((int)state >> 16) & 0x0000FFFF;
            dest.State = state & (SCardReaderStates)0x0000FFFF;
            provider.FreeString(src.Reader);
        }

        public PcscReaderStatus(PcscContext context, IList<string> readerNames) : base(context, readerNames)
        {
            _readerStates = new TIScardReaderState[readerNames.Count];
        }

        public unsafe override PcscReaderStatus WaitForChanged(int timeout = Timeout.Infinite)
        {
            if (Context.IsDisposed) throw new ObjectDisposedException(nameof(PcscContext), nameof(WaitForChanged));
            var provider = Context.Pcsc.Provider;
            var readerStates = _readerStates;
            var length = readerStates.Length;
            GCHandle hReaderStates;
            try
            {
                for (var i = 0; i < length; ++i)
                {
                    readerStates[i].Reader = (void*)provider.AllocateString(Items[i].ReaderName);
                }
                hReaderStates = GCHandle.Alloc(readerStates, GCHandleType.Pinned);
                provider.SCardGetStatusChange(Context.Handle, timeout, (void*)hReaderStates.AddrOfPinnedObject(), length).ThrowIfNotSuccess();
            }
            finally
            {
                if (hReaderStates.IsAllocated) hReaderStates.Free();
                for (var i = 0; i < length; ++i)
                {
                    CopyAndFree(provider, ref readerStates[i], Items[i]);
                }
            }
            return this;
        }
    }
}
