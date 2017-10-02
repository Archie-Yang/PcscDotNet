namespace PcscDotNet
{
    /// <summary>
    /// A handle that identifies the resource manager context.
    /// </summary>
    public struct SCardContext
    {
        /// <summary>
        /// Default value.
        /// </summary>
        public static readonly SCardContext Default = default(SCardContext);

        /// <summary>
        /// Returns true if `Value` is not null; otherwise, false.
        /// </summary>
        public unsafe bool HasValue => Value != null;

        /// <summary>
        /// Handle value.
        /// </summary>
        public unsafe void* Value;
    }
}
