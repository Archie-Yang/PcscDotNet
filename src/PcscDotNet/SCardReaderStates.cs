using System;

namespace PcscDotNet
{
    /// <summary>
    /// Reader states.
    /// </summary>
    [Flags]
    public enum SCardReaderStates
    {
        /// <summary>
        /// This implies that there is a card in the reader with an ATR matching one of the target cards.
        /// If this bit is set, `Present` will also be set.
        /// This bit is only returned on the `SCardLocateCard()` service.
        /// </summary>
        AtrMatch = 0x00000040,
        /// <summary>
        /// This implies that there is a difference between the state believed by the application, and the state known by the Service Manager.
        /// When this bit is set, the application may assume a significant state change has occurred on this reader.
        /// </summary>
        Changed = 0x00000002,
        /// <summary>
        /// This implies that there is not card in the reader.
        /// If this bit is set, all the following bits will be clear.
        /// </summary>
        Empty = 0x00000010,
        /// <summary>
        /// This implies that the card in the reader is allocated for exclusive use by another application.
        /// If this bit is set, `Present` will also be set.
        /// </summary>
        Exclusive = 0x00000080,
        /// <summary>
        /// The application requested that this reader be ignored.
        /// No other bits will be set.
        /// </summary>
        Ignore = 0x00000001,
        /// <summary>
        /// This implies that the card in the reader is in use by one or more other applications, but may be connected to in shared mode.
        /// If this bit is set, `Present` will also be set.
        /// </summary>
        InUse = 0x00000100,
        /// <summary>
        /// This implies that the card in the reader is unresponsive or not supported by the reader or software.
        /// </summary>
        Mute = 0x00000200,
        /// <summary>
        /// This implies that there is a card in the reader.
        /// </summary>
        Present = 0x00000020,
        /// <summary>
        /// This implies that the actual state of this reader is not available.
        /// If this bit is set, then all the following bits are clear.
        /// </summary>
        Unavailable = 0x00000008,
        /// <summary>
        /// The application is unaware of the current state, and would like to know.
        /// The use of this value results in an immediate return from state transition monitoring services.
        /// This is represented by all bits set to zero.
        /// </summary>
        Unaware = 0x00000000,
        /// <summary>
        /// This implies that the given reader name is not recognized by the Service Manager.
        /// If this bit is set, then `Changed` and `Ignore` will also be set.
        /// </summary>
        Unknown = 0x00000004,
        /// <summary>
        /// This implies that the card in the reader has not been powered up.
        /// </summary>
        Unpowered = 0x00000400
    }
}