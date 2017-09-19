using System;

namespace PcscDotNet
{
    public class PcscContext
    {
        private SCardContext _context;

        private readonly Pcsc _pcsc;

        private readonly IPcscProvider _provider;

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

        public unsafe void Establish(SCardScope scope)
        {
            SCardContext context;
            _provider.SCardEstablishContext(scope, null, null, &context).ThrowIfNotSuccess();
            _context = context;
        }

        public void Release()
        {
            _provider.SCardReleaseContext(_context).ThrowIfNotSuccess();
            _context = default(SCardContext);
        }
    }
}