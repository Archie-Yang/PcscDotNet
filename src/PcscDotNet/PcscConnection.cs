using System;

namespace PcscDotNet
{
    public class PcscConnection : IDisposable
    {
        public PcscContext Context { get; private set; }

        public SCardHandle Handle { get; private set; }

        public bool IsConnect => Handle.HasValue;

        public bool IsDisposed { get; private set; } = false;

        public SCardProtocols Protocols { get; private set; } = SCardProtocols.Undefined;

        public IPcscProvider Provider { get; private set; }

        public string ReaderName { get; private set; }

        public SCardShare ShareMode { get; private set; } = SCardShare.Undefined;

        public PcscConnection(PcscContext context, string readerName)
        {
            Provider = (Context = context).Provider;
            ReaderName = readerName;
        }

        ~PcscConnection()
        {
            Dispose();
        }

        public PcscConnection BeginTransaction(PcscExceptionHandler onException = null)
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(PcscConnection), nameof(BeginTransaction));
            Provider.SCardBeginTransaction(Handle).ThrowIfNotSuccess(onException);
            return this;
        }

        public unsafe PcscConnection Connect(SCardShare shareMode, SCardProtocols protocols, PcscExceptionHandler onException = null)
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(PcscConnection), nameof(Connect));
            SCardHandle handle;
            Provider.SCardConnect(Context.Handle, ReaderName, shareMode, protocols, &handle, &protocols).ThrowIfNotSuccess(onException);
            Handle = handle;
            ShareMode = shareMode;
            Protocols = protocols;
            return this;
        }

        public PcscConnection Disconnect(SCardDisposition disposition = SCardDisposition.Leave, PcscExceptionHandler onException = null)
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(PcscConnection), nameof(Disconnect));
            DisconnectInternal(disposition, onException);
            return this;
        }

        public void Dispose()
        {
            if (IsDisposed) return;
            DisconnectInternal();
            IsDisposed = true;
            GC.SuppressFinalize(this);
        }

        public PcscConnection EndTransaction(SCardDisposition disposition = SCardDisposition.Leave, PcscExceptionHandler onException = null)
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(PcscConnection), nameof(EndTransaction));
            Provider.SCardEndTransaction(Handle, disposition).ThrowIfNotSuccess(onException);
            return this;
        }

        public unsafe PcscConnection Reconnect(SCardShare shareMode, SCardProtocols protocols, SCardDisposition initializationDisposition = SCardDisposition.Leave, PcscExceptionHandler onException = null)
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(PcscConnection), nameof(Reconnect));
            Provider.SCardReconnect(Handle, shareMode, protocols, initializationDisposition, &protocols).ThrowIfNotSuccess(onException);
            ShareMode = shareMode;
            Protocols = protocols;
            return this;
        }

        private void DisconnectInternal(SCardDisposition disposition = SCardDisposition.Leave, PcscExceptionHandler onException = null)
        {
            if (!IsConnect) return;
            Provider.SCardDisconnect(Handle, disposition).ThrowIfNotSuccess(onException);
            Handle = SCardHandle.Default;
            ShareMode = SCardShare.Undefined;
            Protocols = SCardProtocols.Undefined;
        }
    }
}
