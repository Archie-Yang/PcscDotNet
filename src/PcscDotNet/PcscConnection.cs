using System;

namespace PcscDotNet
{
    public class PcscConnection : IDisposable
    {
        public PcscContext Context { get; private set; }

        public SCardHandle Handle { get; private set; }

        public bool IsConnect => Handle.HasValue;

        public bool IsDisposed { get; private set; } = false;

        public SCardProtocols Protocol { get; private set; } = SCardProtocols.Undefined;

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

        public unsafe PcscConnection Connect(SCardShare shareMode, SCardProtocols protocol, PcscExceptionHandler onException = null)
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(PcscConnection), nameof(Connect));
            SCardHandle handle;
            Provider.SCardConnect(Context.Handle, ReaderName, shareMode, protocol, &handle, &protocol).ThrowIfNotSuccess(onException);
            Handle = handle;
            ShareMode = shareMode;
            Protocol = protocol;
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

        public unsafe PcscConnection Reconnect(SCardShare shareMode, SCardProtocols protocol, SCardDisposition initializationDisposition = SCardDisposition.Leave, PcscExceptionHandler onException = null)
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(PcscConnection), nameof(Reconnect));
            Provider.SCardReconnect(Handle, shareMode, protocol, initializationDisposition, &protocol).ThrowIfNotSuccess(onException);
            ShareMode = shareMode;
            Protocol = protocol;
            return this;
        }

        private void DisconnectInternal(SCardDisposition disposition = SCardDisposition.Leave, PcscExceptionHandler onException = null)
        {
            if (!IsConnect) return;
            Provider.SCardDisconnect(Handle, disposition).ThrowIfNotSuccess(onException);
            Handle = SCardHandle.Default;
            ShareMode = SCardShare.Undefined;
            Protocol = SCardProtocols.Undefined;
        }
    }
}
