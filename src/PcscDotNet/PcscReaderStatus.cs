using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace PcscDotNet
{
    public delegate void PcscReaderStatusAction(PcscReaderStatus readerStatus);

    public abstract class PcscReaderStatus : ReadOnlyCollection<PcscReaderState>
    {
        /// <summary>
        /// This special reader name is used to be notified when a reader is added to or removed from the system through `SCardGetStatusChange`.
        /// </summary>
        public const string PnPReaderName = "\\\\?PnP?\\Notification";

        protected readonly PcscContext Context;

        public PcscReaderStatus(PcscContext context, IList<string> readerNames) : base(new List<PcscReaderState>(readerNames.Count))
        {
            Context = context;
            foreach (var readerName in readerNames)
            {
                Items.Add(new PcscReaderState() { ReaderName = readerName });
            }
        }

        public void Cancel()
        {
            Context.Cancel();
        }

        public PcscReaderStatus Do(PcscReaderStatusAction action)
        {
            action(this);
            return this;
        }

        public abstract PcscReaderStatus WaitForChanged(int timeout = Timeout.Infinite);
    }
}
