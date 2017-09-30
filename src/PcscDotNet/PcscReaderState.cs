namespace PcscDotNet
{
    public sealed class PcscReaderState
    {
        public byte[] Atr { get; internal set; }

        public int EventNumber { get; internal set; }

        public string ReaderName { get; internal set; }

        public SCardReaderStates State { get; internal set; }
    }
}
