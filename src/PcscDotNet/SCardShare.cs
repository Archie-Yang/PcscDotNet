namespace PcscDotNet
{
    /// <summary>
    /// Indicates whether other applications may form connections to the card.
    /// </summary>
    public enum SCardShare
    {
        /// <summary>
        /// This application demands direct control of the reader, so it is not available to other applications.
        /// </summary>
        Direct = 3,
        /// <summary>
        /// This application is not willing to share this card with other applications.
        /// </summary>
        Exclusive = 1,
        /// <summary>
        /// This application is willing to share this card with other applications.
        /// </summary>
        Shared = 2,
        /// <summary>
        /// Share mode is undefined, can not be used to connect to card/reader.
        /// </summary>
        Undefined = 0
    }
}