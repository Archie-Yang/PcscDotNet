namespace PcscDotNet
{
    public class PcscConnection
    {
        public PcscContext Context { get; private set; }

        public IPcscProvider Provider { get; private set; }

        public string ReaderName { get; private set; }

        public PcscConnection(PcscContext context, string readerName)
        {
            Provider = (Context = context).Provider;
            ReaderName = readerName;
        }
    }
}
