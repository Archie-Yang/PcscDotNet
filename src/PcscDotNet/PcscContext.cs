using System;
using System.Collections.Generic;

namespace PcscDotNet
{
    public class PcscContext : IDisposable
    {
        public SCardContext Handle { get; private set; }

        public bool IsDisposed { get; private set; } = false;

        public bool IsEstablished => Handle.HasValue;

        public IPcscProvider Provider { get; private set; }

        public PcscContext(IPcscProvider provider)
        {
            Provider = provider;
        }

        ~PcscContext()
        {
            Dispose();
        }

        public void Cancel(PcscExceptionHandler onException = null)
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(PcscContext), nameof(Cancel));
            Provider.SCardCancel(Handle).ThrowIfNotSuccess(onException);
        }

        public PcscConnection Connect(string readerName, SCardShare shareMode, SCardProtocols protocols, PcscExceptionHandler onException = null)
        {
            return CreateConnection(readerName).Connect(shareMode, protocols, onException);
        }

        public PcscConnection CreateConnection(string readerName)
        {
            return new PcscConnection(this, readerName);
        }

        public void Dispose()
        {
            if (IsDisposed) return;
            ReleaseInternal();
            IsDisposed = true;
            GC.SuppressFinalize(this);
        }

        public unsafe PcscContext Establish(SCardScope scope, PcscExceptionHandler onException = null)
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(PcscContext), nameof(Establish));
            if (IsEstablished) throw new InvalidOperationException("Context has been established.");
            SCardContext handle;
            Provider.SCardEstablishContext(scope, null, null, &handle).ThrowIfNotSuccess(onException);
            Handle = handle;
            return this;
        }

        public IEnumerable<string> GetReaderGroupNames(PcscExceptionHandler onException = null)
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(PcscContext), nameof(GetReaderGroupNames));
            var groupNames = Provider.GetReaderGroupNames(Handle, onException);
            if (groupNames == null) yield break;
            for (int offset = 0, offsetNull, length = groupNames.Length; ;)
            {
                if (offset >= length || (offsetNull = groupNames.IndexOf('\0', offset)) <= offset) yield break;
                yield return groupNames.Substring(offset, offsetNull - offset);
                offset = offsetNull + 1;
            }
        }

        public IEnumerable<string> GetReaderNames(SCardReaderGroup group = SCardReaderGroup.NotSpecified, PcscExceptionHandler onException = null)
        {
            return GetReaderNames(group.GetDefinedValue(), onException);
        }

        public IEnumerable<string> GetReaderNames(string group, PcscExceptionHandler onException = null)
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(PcscContext), nameof(GetReaderNames));
            var readerNames = Provider.GetReaderNames(Handle, group, onException);
            if (readerNames == null) yield break;
            for (int offset = 0, offsetNull, length = readerNames.Length; ;)
            {
                if (offset >= length || (offsetNull = readerNames.IndexOf('\0', offset)) <= offset) yield break;
                yield return readerNames.Substring(offset, offsetNull - offset);
                offset = offsetNull + 1;
            }
        }

        public PcscReaderStatus GetStatus(params string[] readerNames)
        {
            return new PcscReaderStatus(this, readerNames).WaitForChanged();
        }

        public PcscReaderStatus GetStatus(IEnumerable<string> readerNames, PcscExceptionHandler onException = null)
        {
            return new PcscReaderStatus(this, readerNames).WaitForChanged(onException: onException);
        }

        public PcscContext Release(PcscExceptionHandler onException = null)
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(PcscContext), nameof(Release));
            ReleaseInternal(onException);
            return this;
        }

        public PcscContext Validate(PcscExceptionHandler onException = null)
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(PcscContext), nameof(Validate));
            Provider.SCardIsValidContext(Handle).ThrowIfNotSuccess(onException);
            return this;
        }

        private void ReleaseInternal(PcscExceptionHandler onException = null)
        {
            if (!IsEstablished) return;
            Provider.SCardReleaseContext(Handle).ThrowIfNotSuccess(onException);
            Handle = SCardContext.Default;
        }
    }
}
