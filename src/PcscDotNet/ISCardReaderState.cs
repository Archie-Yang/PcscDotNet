namespace PcscDotNet
{
    /// <summary>
    /// This interface is used by functions for tracking smart cards within readers.
    /// </summary>
    public interface ISCardReaderState
    {
        /// <summary>
        /// ATR of the inserted card.
        /// </summary>
        byte[] Atr { get; }

        /// <summary>
        /// Current state of the reader, as seen by the application, combination, as a bitmask. 
        /// </summary>
        SCardReaderStates CurrentState { get; set; }

        /// <summary>
        /// Current state of the reader, as known by the smart card resource manager, combination, as a bitmask. 
        /// </summary>
        SCardReaderStates EventState { get; set; }

        /// <summary>
        /// A pointer to the name of the reader being monitored.
        /// Set the value of this member to "\\\\?PnP?\\Notification" and the values of all other members to zero to be notified of the arrival of a new smart card reader.
        /// </summary>
        unsafe void* Reader { get; set; }
    }
}
