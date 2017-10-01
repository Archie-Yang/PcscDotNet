using System;
using System.Collections.Generic;
using System.Linq;

namespace PcscDotNet
{
    public class PcscContext : IDisposable
    {
        private SCardContext _handle;

        private readonly Pcsc _pcsc;

        private readonly IPcscProvider _provider;

        public SCardContext Handle => _handle;

        public bool IsDisposed { get; private set; } = false;

        public bool IsEstablished => _handle.HasValue;

        public Pcsc Pcsc => _pcsc;

        public PcscContext(Pcsc pcsc)
        {
            _pcsc = pcsc;
            _provider = pcsc.Provider;
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
            _provider.SCardCancel(_handle).ThrowIfNotSuccess();
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
            _provider.SCardEstablishContext(scope, null, null, &handle).ThrowIfNotSuccess();
            _handle = handle;
            return this;
        }

        public IEnumerable<string> GetReaderGroupNames()
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(PcscContext), nameof(GetReaderGroupNames));
            var groupNames = _pcsc.GetReaderGroupNames(_handle);
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
            var readerNames = _pcsc.GetReaderNames(_handle, group);
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
            return _provider.AllocateReaderStatus(this, readerNames).WaitForChanged();
        }

        public PcscReaderStatus GetStatus(params string[] readerNames)
        {
            return _provider.AllocateReaderStatus(this, readerNames).WaitForChanged();
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
            _provider.SCardIsValidContext(_handle).ThrowIfNotSuccess();
            return this;
        }

        private void ReleaseInternal()
        {
            if (!IsEstablished) return;
            _provider.SCardReleaseContext(_handle).ThrowIfNotSuccess();
            _handle = SCardContext.Default;
        }
    }
}
