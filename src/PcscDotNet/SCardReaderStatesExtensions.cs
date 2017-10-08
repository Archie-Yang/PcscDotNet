namespace PcscDotNet
{
    public static class SCardReaderStatesExtensions
    {
        public static bool IsSet(this SCardReaderStates src, SCardReaderStates states)
        {
            return (src & states) == states;
        }
    }
}
