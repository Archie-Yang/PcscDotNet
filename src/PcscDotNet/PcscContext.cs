using System;

namespace PcscDotNet
{
    public class PcscContext : IDisposable
    {
        private SCardContext _handle;

        private readonly Pcsc _pcsc;

        private readonly IPcscProvider _provider;

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

        public unsafe PcscContext Establish(SCardScope scope)
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(PcscContext), nameof(Establish));
            if (IsEstablished) throw new InvalidOperationException("Context has been established.");
            SCardContext handle;
            _provider.SCardEstablishContext(scope, null, null, &handle).ThrowIfNotSuccess();
            _handle = handle;
            return this;
        }

        public PcscContext Release()
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(PcscContext), nameof(Release));
            if (IsEstablished)
            {
                _provider.SCardReleaseContext(_handle).ThrowIfNotSuccess();
                _handle = SCardContext.Default;
            }
            return this;
        }

        public void Validate()
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(PcscContext), nameof(Validate));
            _provider.SCardIsValidContext(_handle).ThrowIfNotSuccess();
        }

        public void Dispose()
        {
            if (IsDisposed) return;
            Release();
            GC.SuppressFinalize(this);
            IsDisposed = true;
        }
    }
}