using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Threading;

namespace PcscDotNet
{
    public delegate void PcscReaderStatusAction(PcscReaderStatus readerStatus);

    public sealed class PcscReaderStatus : ReadOnlyCollection<PcscReaderState>
    {
        /// <summary>
        /// This special reader name is used to be notified when a reader is added to or removed from the system through `SCardGetStatusChange`.
        /// </summary>
        public const string PnPReaderName = "\\\\?PnP?\\Notification";

        private readonly PcscContext _context;

        private readonly byte[] _readerStates;

        public PcscContext Context => _context;

        public PcscReaderStatus(PcscContext context, IList<string> readerNames) : base(new List<PcscReaderState>(readerNames.Count))
        {
            _context = context;
            _readerStates = context.Pcsc.Provider.AllocateReaderStates(readerNames.Count);
            foreach (var readerName in readerNames)
            {
                Items.Add(new PcscReaderState() { ReaderName = readerName });
            }
        }

        public void Cancel()
        {
            _context.Cancel();
        }

        public PcscReaderStatus Do(PcscReaderStatusAction action)
        {
            action(this);
            return this;
        }

        public unsafe PcscReaderStatus WaitForChanged(int timeout = Timeout.Infinite)
        {
            if (_context.IsDisposed) throw new ObjectDisposedException(nameof(PcscContext), nameof(WaitForChanged));
            var count = Count;
            if (count == 0) return this;
            var provider = _context.Pcsc.Provider;
            fixed (byte* fReaderStates = &_readerStates[0])
            {
                var pReaderStates = fReaderStates;
                try
                {
                    for (var i = 0; i < count; ++i)
                    {
                        provider.WriteReaderState(pReaderStates, i, (void*)provider.AllocateString(Items[i].ReaderName));
                    }
                    provider.SCardGetStatusChange(_context.Handle, timeout, pReaderStates, count).ThrowIfNotSuccess();
                }
                finally
                {
                    for (var i = 0; i < count; ++i)
                    {
                        provider.ReadReaderState(pReaderStates, i, out void* pReaderName, out SCardReaderStates currentState, out SCardReaderStates eventState, out byte[] atr);
                        provider.WriteReaderState(pReaderStates, i, eventState);
                        var readerState = Items[i];
                        readerState.Atr = atr;
                        readerState.EventNumber = ((int)eventState >> 16) & 0x0000FFFF;
                        readerState.State = eventState & (SCardReaderStates)0x0000FFFF;
                        provider.FreeString(pReaderName);
                    }
                }
            }
            return this;
        }
    }
}
