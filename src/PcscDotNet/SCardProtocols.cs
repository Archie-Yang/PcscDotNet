using System;

namespace PcscDotNet
{
    /// <summary>
    /// Acceptable protocols for the connection.
    /// </summary>
    [Flags]
    public enum SCardProtocols : uint
    {
        /// <summary>
        /// Use the default transmission parameters and card clock frequency.
        /// (For WinSCard in Windows platform.)
        /// </summary>
        Default = 0x80000000,
        /// <summary>
        /// Raw is the active protocol.
        /// (For WinSCard in Windows platform.)
        /// </summary>
        Raw = 0x00010000,
        /// <summary>
        /// Raw is the active protocol, use with memory type cards.
        /// (For pcsc-lite in Linux and OS X platforms.)
        /// </summary>
        RawPcscLite = 0x00000004,
        /// <summary>
        /// T=0 is the active protocol.
        /// </summary>
        T0 = 0x00000001,
        /// <summary>
        /// T=1 is the active protocol.
        /// </summary>
        T1 = 0x00000002,
        /// <summary>
        /// T=15 is the active protocol.
        /// (For pcsc-lite in Linux and OS X platforms.)
        /// </summary>
        T15 = 0x00000008,
        /// <summary>
        /// This is the mask of ISO defined transmission protocols.
        /// </summary>
        Tx = T0 | T1,
        /// <summary>
        /// There is no active protocol.
        /// </summary>
        Undefined = 0x00000000
    }
}