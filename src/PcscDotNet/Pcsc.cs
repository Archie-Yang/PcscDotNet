using System;

namespace PcscDotNet
{
    public class Pcsc
    {
        private readonly IPcscProvider _provider;

        public IPcscProvider Provider => _provider;

        public Pcsc(IPcscProvider provider)
        {
            _provider = provider;
        }

        public PcscContext CreateContext()
        {
            return new PcscContext(this);
        }

        public PcscContext EstablishContext(SCardScope scope)
        {
            return new PcscContext(this, scope);
        }
    }
}
