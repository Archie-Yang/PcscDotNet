using System;

namespace PcscDotNet
{
    public class PcscConnection : IDisposable
    {
        public const int DefaultControlBufferSize = 256;

        public const int DefaultTransmitBufferSize = 256;

        public const int ExtendedTransmitBufferSize = 65536;

        public PcscContext Context { get; private set; }

        public int ControlBufferSize { get; set; } = DefaultControlBufferSize;

        public SCardHandle Handle { get; private set; }

        public bool IsConnect => Handle.HasValue;

        public bool IsDisposed { get; private set; } = false;

        public SCardProtocols Protocol { get; private set; } = SCardProtocols.Undefined;

        public IPcscProvider Provider { get; private set; }

        public string ReaderName { get; private set; }

        public SCardShare ShareMode { get; private set; } = SCardShare.Undefined;

        public int TransmitBufferSize { get; set; } = DefaultTransmitBufferSize;

        public PcscConnection(PcscContext context, string readerName)
        {
            Provider = (Context = context).Provider;
            ReaderName = readerName;
        }

        ~PcscConnection()
        {
            Dispose(false);
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

        public byte[] Control(int code, params byte[] send)
        {
            return Control(code, send, ControlBufferSize);
        }

        public byte[] Control(int code, byte[] send, PcscExceptionHandler onException = null)
        {
            return Control(code, send, ControlBufferSize, onException);
        }

        public unsafe byte[] Control(int code, byte[] send, int bufferSize, PcscExceptionHandler onException = null)
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(PcscConnection), nameof(Control));
            if (bufferSize <= 0)
            {
                if (send == null)
                {
                    Provider.SCardControl(Handle, code, null, 0, null, 0, &bufferSize).ThrowIfNotSuccess(onException);
                }
                else
                {
                    fixed (byte* fSend = send)
                    {
                        Provider.SCardControl(Handle, code, fSend, send.Length, null, 0, &bufferSize).ThrowIfNotSuccess(onException);
                    }
                }
                return new byte[0];
            }
            else
            {
                var recv = new byte[bufferSize];
                fixed (byte* fRecv = recv)
                {
                    if (send == null)
                    {
                        Provider.SCardControl(Handle, code, null, 0, fRecv, bufferSize, &bufferSize).ThrowIfNotSuccess(onException);
                    }
                    else
                    {
                        fixed (byte* fSend = send)
                        {
                            Provider.SCardControl(Handle, code, fSend, send.Length, fRecv, bufferSize, &bufferSize).ThrowIfNotSuccess(onException);
                        }
                    }
                }
                if (bufferSize <= 0) return new byte[0];
                Array.Resize(ref recv, bufferSize);
                return recv;
            }
        }

        public byte[] Control(SCardControlFunction function, params byte[] send)
        {
            return Control(Provider.SCardCtlCode(function), send, ControlBufferSize);
        }

        public byte[] Control(SCardControlFunction function, byte[] send, PcscExceptionHandler onException = null)
        {
            return Control(Provider.SCardCtlCode(function), send, ControlBufferSize, onException);
        }

        public byte[] Control(SCardControlFunction function, byte[] send, int bufferSize, PcscExceptionHandler onException = null)
        {
            return Control(Provider.SCardCtlCode(function), send, bufferSize, onException);
        }

        public PcscConnection Disconnect(SCardDisposition disposition = SCardDisposition.Leave, PcscExceptionHandler onException = null)
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(PcscConnection), nameof(Disconnect));
            DisconnectInternal(disposition, onException);
            return this;
        }

        public void Dispose()
        {
            Dispose(true);
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

        public unsafe byte[] Transmit(params byte[] send)
        {
            return Transmit(send, null, TransmitBufferSize);
        }

        public unsafe byte[] Transmit(byte[] send, PcscExceptionHandler onException = null)
        {
            return Transmit(send, null, TransmitBufferSize, onException);
        }

        public unsafe byte[] Transmit(byte[] send, int bufferSize, PcscExceptionHandler onException = null)
        {
            return Transmit(send, null, bufferSize, onException);
        }

        public unsafe byte[] Transmit(byte[] send, byte[] sendInformation, PcscExceptionHandler onException = null)
        {
            return Transmit(send, sendInformation, TransmitBufferSize, onException);
        }

        public unsafe byte[] Transmit(byte[] send, int recvInformationLength, out byte[] recvInformation, PcscExceptionHandler onException = null)
        {
            return Transmit(send, null, TransmitBufferSize, recvInformationLength, out recvInformation, onException);
        }

        public unsafe byte[] Transmit(byte[] send, int bufferSize, int recvInformationLength, out byte[] recvInformation, PcscExceptionHandler onException = null)
        {
            return Transmit(send, null, bufferSize, recvInformationLength, out recvInformation, onException);
        }

        public unsafe byte[] Transmit(byte[] send, byte[] sendInformation, int recvInformationLength, out byte[] recvInformation, PcscExceptionHandler onException = null)
        {
            return Transmit(send, sendInformation, TransmitBufferSize, recvInformationLength, out recvInformation, onException);
        }

        public unsafe byte[] Transmit(byte[] send, byte[] sendInformation, int bufferSize, PcscExceptionHandler onException = null)
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(PcscConnection), nameof(Transmit));
            var recv = new byte[bufferSize];
            var sendIORequest = Provider.AllocateIORequest(sendInformation?.Length ?? 0);
            fixed (byte* fSend = send, fRecv = recv, fSendIORequest = sendIORequest)
            {
                var pSendIORequest = fSendIORequest;
                Provider.WriteIORequest(pSendIORequest, Protocol, sendIORequest.Length, sendInformation);
                Provider.SCardTransmit(Handle, pSendIORequest, fSend, send.Length, null, fRecv, &bufferSize).ThrowIfNotSuccess(onException);
            }
            Array.Resize(ref recv, bufferSize);
            return recv;
        }

        public unsafe byte[] Transmit(byte[] send, byte[] sendInformation, int bufferSize, int recvInformationLength, out byte[] recvInformation, PcscExceptionHandler onException = null)
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(PcscConnection), nameof(Transmit));
            var recv = new byte[bufferSize];
            var sendIORequest = Provider.AllocateIORequest(sendInformation?.Length ?? 0);
            var recvIORequest = Provider.AllocateIORequest(recvInformationLength);
            fixed (byte* fSend = send, fRecv = recv, fSendIORequest = sendIORequest, fRecvIORequest = recvIORequest)
            {
                var pSendIORequest = fSendIORequest;
                var pRecvIORequest = fRecvIORequest;
                Provider.WriteIORequest(pSendIORequest, Protocol, sendIORequest.Length, sendInformation);
                Provider.WriteIORequest(pRecvIORequest, SCardProtocols.Undefined, recvIORequest.Length, null);
                Provider.SCardTransmit(Handle, pSendIORequest, fSend, send.Length, pRecvIORequest, fRecv, &bufferSize).ThrowIfNotSuccess(onException);
                SCardProtocols protocol;
                Provider.ReadIORequest(pRecvIORequest, out protocol, out recvInformation);
                Protocol = protocol;
            }
            Array.Resize(ref recv, bufferSize);
            return recv;
        }

        private void DisconnectInternal(SCardDisposition disposition = SCardDisposition.Leave, PcscExceptionHandler onException = null)
        {
            if (!IsConnect) return;
            Provider.SCardDisconnect(Handle, disposition).ThrowIfNotSuccess(onException);
            Handle = SCardHandle.Default;
            ShareMode = SCardShare.Undefined;
            Protocol = SCardProtocols.Undefined;
        }

        private void Dispose(bool isSuppressFinalize)
        {
            if (!IsDisposed)
            {
                DisconnectInternal();
                IsDisposed = true;
            }
            if (isSuppressFinalize) GC.SuppressFinalize(this);
        }
    }
}
