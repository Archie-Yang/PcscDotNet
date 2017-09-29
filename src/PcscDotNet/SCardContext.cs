namespace PcscDotNet
{
    public struct SCardContext
    {
        public static readonly SCardContext Default = default(SCardContext);

        public unsafe bool HasValue => Value != null;

        public unsafe void* Value;
    }
}
