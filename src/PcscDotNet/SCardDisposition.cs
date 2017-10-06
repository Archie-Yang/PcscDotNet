namespace PcscDotNet
{
    /// <summary>
    /// Action to take on the card in the connected reader on close.
    /// </summary>
    public enum SCardDisposition
    {
        /// <summary>
        /// Eject the card on close.
        /// </summary>
        Eject = 3,
        /// <summary>
        /// Don't do anything special on close.
        /// </summary>
        Leave = 0,
        /// <summary>
        /// Reset the card on close (warm reset).
        /// </summary>
        Reset = 1,
        /// <summary>
        /// Power down the card on close (cold reset).
        /// </summary>
        Unpower = 2
    }
}
