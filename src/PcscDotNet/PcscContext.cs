using System;
using System.Collections.Generic;
using System.Linq;

namespace PcscDotNet
{
    public class PcscContext : IDisposable
    {
        public SCardContext Handle { get; private set; }

        public bool IsDisposed { get; private set; } = false;

        public bool IsEstablished => Handle.HasValue;

        public Pcsc Pcsc { get; private set; }

        public IPcscProvider Provider { get; private set; }

        public PcscContext(Pcsc pcsc)
        {
            Pcsc = pcsc;
            Provider = pcsc.Provider;
        }

        public PcscContext(Pcsc pcsc, SCardScope scope) : this(pcsc)
        {
            Establish(scope);
        }

        ~PcscContext()
        {
            Dispose();
        }

        public void Cancel()
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(PcscContext), nameof(Cancel));
            Provider.SCardCancel(Handle).ThrowIfNotSuccess();
        }

        public void Dispose()
        {
            if (IsDisposed) return;
            ReleaseInternal();
            IsDisposed = true;
            GC.SuppressFinalize(this);
        }

        public unsafe PcscContext Establish(SCardScope scope)
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(PcscContext), nameof(Establish));
            if (IsEstablished) throw new InvalidOperationException("Context has been established.");
            SCardContext handle;
            Provider.SCardEstablishContext(scope, null, null, &handle).ThrowIfNotSuccess();
            Handle = handle;
            return this;
        }

        public IEnumerable<string> GetReaderGroupNames()
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(PcscContext), nameof(GetReaderGroupNames));
            var groupNames = Pcsc.GetReaderGroupNames(Handle);
            if (groupNames == null) yield break;
            for (int offset = 0, offsetNull, length = groupNames.Length; ;)
            {
                if (offset >= length || (offsetNull = groupNames.IndexOf('\0', offset)) <= offset) yield break;
                yield return groupNames.Substring(offset, offsetNull - offset);
                offset = offsetNull + 1;
            }
        }

        public IEnumerable<string> GetReaderNames(SCardReaderGroup group = SCardReaderGroup.NotSpecified)
        {
            return GetReaderNames(group.GetDefinedValue());
        }

        public IEnumerable<string> GetReaderNames(string group)
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(PcscContext), nameof(GetReaderNames));
            var readerNames = Pcsc.GetReaderNames(Handle, group);
            if (readerNames == null) yield break;
            for (int offset = 0, offsetNull, length = readerNames.Length; ;)
            {
                if (offset >= length || (offsetNull = readerNames.IndexOf('\0', offset)) <= offset) yield break;
                yield return readerNames.Substring(offset, offsetNull - offset);
                offset = offsetNull + 1;
            }
        }

        public PcscReaderStatus GetStatus(IEnumerable<string> readerNames)
        {
            return GetStatus(readerNames.ToList());
        }

        public PcscReaderStatus GetStatus(IList<string> readerNames)
        {
            return new PcscReaderStatus(this, readerNames).WaitForChanged();
        }

        public PcscReaderStatus GetStatus(params string[] readerNames)
        {
            return new PcscReaderStatus(this, readerNames).WaitForChanged();
        }

        public PcscContext Release()
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(PcscContext), nameof(Release));
            ReleaseInternal();
            return this;
        }

        public PcscContext Validate()
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(PcscContext), nameof(Validate));
            Provider.SCardIsValidContext(Handle).ThrowIfNotSuccess();
            return this;
        }

        private void ReleaseInternal()
        {
            if (!IsEstablished) return;
            Provider.SCardReleaseContext(Handle).ThrowIfNotSuccess();
            Handle = SCardContext.Default;
        }
    }
}
