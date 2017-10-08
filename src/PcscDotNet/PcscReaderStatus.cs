using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private readonly byte[] _readerStates;

        public PcscContext Context { get; private set; }

        public IPcscProvider Provider { get; private set; }

        public PcscReaderStatus(PcscContext context, IEnumerable<string> readerNames) : base(new List<PcscReaderState>())
        {
            Context = context;
            foreach (var readerName in readerNames)
            {
                Items.Add(new PcscReaderState(readerName));
            }
            _readerStates = (Provider = context.Provider).AllocateReaderStates(Items.Count);
        }

        public void Cancel(PcscExceptionHandler onException = null)
        {
            Context.Cancel(onException);
        }

        public PcscReaderStatus Do(PcscReaderStatusAction action)
        {
            action(this);
            return this;
        }

        public unsafe PcscReaderStatus WaitForChanged(int timeout = Timeout.Infinite, PcscExceptionHandler onException = null)
        {
            if (Context.IsDisposed) throw new ObjectDisposedException(nameof(PcscContext), nameof(WaitForChanged));
            fixed (byte* fReaderStates = _readerStates)
            {
                var pReaderStates = fReaderStates;
                try
                {
                    for (var i = 0; i < Count; ++i)
                    {
                        Provider.WriteReaderState(pReaderStates, i, (void*)Provider.AllocateString(Items[i].ReaderName));
                    }
                    Provider.SCardGetStatusChange(Context.Handle, timeout, pReaderStates, Count).ThrowIfNotSuccess(onException);
                }
                finally
                {
                    for (var i = 0; i < Count; ++i)
                    {
                        Provider.ReadReaderState(pReaderStates, i, out void* pReaderName, out SCardReaderStates currentState, out SCardReaderStates eventState, out byte[] atr);
                        Provider.WriteReaderState(pReaderStates, i, eventState);
                        var readerState = Items[i];
                        readerState.Atr = atr;
                        readerState.EventNumber = ((int)eventState >> 16) & 0x0000FFFF;
                        readerState.State = eventState & (SCardReaderStates)0x0000FFFF;
                        Provider.FreeString(pReaderName);
                    }
                }
            }
            return this;
        }
    }
}
