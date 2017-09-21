namespace PcscDotNet
{
    /// <summary>
    /// Smart card reader groups defined to the system.
    /// </summary>
    public enum SCardReaderGroup
    {
        /// <summary>
        /// Group used when no group name is provided when listing readers.
        /// Returns a list of all readers, regardless of what group or groups the readers are in.
        /// </summary>
        All,
        /// <summary>
        /// Default group to which all readers are added when introduced into the system.
        /// </summary>
        Defualt,
        /// <summary>
        /// Unused legacy value.
        /// This is an internally managed group that cannot be modified by using any reader group APIs.
        /// It is intended to be used for enumeration only.
        /// </summary>
        Local,
        /// <summary>
        /// List all readers in the system.
        /// (Same as All.)
        /// </summary>
        NotSpecified = All,
        /// <summary>
        /// Unused legacy value.
        /// This is an internally managed group that cannot be modified by using any reader group APIs.
        /// It is intended to be used for enumeration only.
        /// </summary>
        System
    }
}