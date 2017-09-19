using System;

namespace PcscDotNet
{
    public class PcscContext : IDisposable
    {
        private SCardContext _context;

        private readonly Pcsc _pcsc;

        private readonly IPcscProvider _provider;

        public bool IsDisposed { get; private set; } = false;

        public Pcsc Pcsc => _pcsc;

        public PcscContext(Pcsc pcsc)
        {
            _pcsc = pcsc;
            _provider = _pcsc.Provider;
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
            SCardContext context;
            _provider.SCardEstablishContext(scope, null, null, &context).ThrowIfNotSuccess();
            _context = context;
            return this;
        }

        public PcscContext Release()
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(PcscContext), nameof(Release));
            if (_context.HasValue)
            {
                _provider.SCardReleaseContext(_context).ThrowIfNotSuccess();
                _context = SCardContext.Default;
            }
            return this;
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