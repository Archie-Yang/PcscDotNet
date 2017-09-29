namespace PcscDotNet
{
    /// <summary>
    /// The return values of smart card functions.
    /// </summary>
    public enum SCardError : uint
    {
        /// <summary>
        /// There was an error trying to set the smart card file object pointer.
        /// </summary>
        BadSeek = 0x80100029,
        /// <summary>
        /// The client attempted a smart card operation in a remote session, such as a client session running on a terminal server, and the operating system in use does not support smart card redirection.
        /// </summary>
        BrokenPipe = 0x00000109,
        /// <summary>
        /// The requested item could not be found in the cache.
        /// </summary>
        CacheItemNotFound = 0x80100070,
        /// <summary>
        /// The requested cache item is too old and was deleted from the cache.
        /// </summary>
        CacheItemStale = 0x80100071,
        /// <summary>
        /// The new cache item exceeds the maximum per-item size defined for the cache.
        /// </summary>
        CacheItemTooBig = 0x80100072,
        /// <summary>
        /// The action was cancelled by an SCardCancel request.
        /// </summary>
        Cancelled = 0x80100002,
        /// <summary>
        /// The action was cancelled by the user.
        /// </summary>
        CancelledByUser = 0x8010006E,
        /// <summary>
        /// The system could not dispose of the media in the requested manner.
        /// </summary>
        CantDispose = 0x8010000E,
        /// <summary>
        /// No PIN was presented to the smart card.
        /// </summary>
        CardNotAuthenticated = 0x8010006F,
        /// <summary>
        /// The smart card does not meet minimal requirements for support.
        /// </summary>
        CardUnsupported = 0x8010001C,
        /// <summary>
        /// The requested certificate could not be obtained.
        /// </summary>
        CertificateUnavailable = 0x8010002D,
        /// <summary>
        /// The card cannot be accessed because the maximum number of PIN entry attempts has been reached.
        /// </summary>
        ChvBlocked = 0x8010006C,
        /// <summary>
        /// A communications error with the smart card has been detected. Retry the operation.
        /// </summary>
        CommDataLost = 0x8010002F,
        /// <summary>
        /// An internal communications error has been detected.
        /// </summary>
        CommError = 0x80100013,
        /// <summary>
        /// The reader driver did not produce a unique reader name.
        /// </summary>
        DuplicateReader = 0x8010001B,
        /// <summary>
        /// The end of the smart card file has been reached.
        /// </summary>
        Eof = 0x8010006D,
        /// <summary>
        /// The identified directory does not exist in the smart card.
        /// </summary>
        DirNotFound = 0x80100023,
        /// <summary>
        /// The identified file does not exist in the smart card.
        /// </summary>
        FileNotFound = 0x80100024,
        /// <summary>
        /// The requested order of object creation is not supported.
        /// </summary>
        IccCreateOrder = 0x80100021,
        /// <summary>
        /// No Primary Provider can be found for the smart card.
        /// </summary>
        IccInstallation = 0x80100020,
        /// <summary>
        /// The data buffer to receive returned data is too small for the returned data.
        /// </summary>
        InsufficientBuffer = 0x80100008,
        /// <summary>
        /// An internal consistency check failed.
        /// </summary>
        InternalError = 0x80100001,
        /// <summary>
        /// An ATR obtained from the registry is not a valid ATR string.
        /// </summary>
        InvalidAtr = 0x80100015,
        /// <summary>
        /// The supplied PIN is incorrect.
        /// </summary>
        InvalidChv = 0x8010002A,
        /// <summary>
        /// The supplied handle was invalid.
        /// </summary>
        InvalidHandle = 0x80100003,
        /// <summary>
        /// One or more of the supplied parameters could not be properly interpreted.
        /// </summary>
        InvalidParameter = 0x80100004,
        /// <summary>
        /// Registry startup information is missing or invalid.
        /// </summary>
        InvalidTarget = 0x80100005,
        /// <summary>
        /// One or more of the supplied parameters values could not be properly interpreted.
        /// </summary>
        InvalidValue = 0x80100011,
        /// <summary>
        /// Access is denied to this file.
        /// </summary>
        NoAccess = 0x80100027,
        /// <summary>
        /// The supplied path does not represent a smart card directory.
        /// </summary>
        NoDir = 0x80100025,
        /// <summary>
        /// The supplied path does not represent a smart card file.
        /// </summary>
        NoFile = 0x80100026,
        /// <summary>
        /// The requested key container does not exist on the smart card.
        /// </summary>
        NoKeyContainer = 0x80100030,
        /// <summary>
        /// Not enough memory available to complete this command.
        /// </summary>
        NoMemory = 0x80100006,
        /// <summary>
        /// The smart card PIN cannot be cached.
        /// </summary>
        NoPinCache = 0x80100033,
        /// <summary>
        /// Cannot find a smart card reader.
        /// </summary>
        NoReadersAvailable = 0x8010002E,
        /// <summary>
        /// The Smart Card Resource Manager is not running.
        /// </summary>
        NoService = 0x8010001D,
        /// <summary>
        /// The operation requires a smart card, but no smart card is currently in the device.
        /// </summary>
        NoSmartCard = 0x8010000C,
        /// <summary>
        /// The requested certificate does not exist.
        /// </summary>
        NoSuchCertificate = 0x8010002C,
        /// <summary>
        /// The reader or smart card is not ready to accept commands.
        /// </summary>
        NotReady = 0x80100010,
        /// <summary>
        /// An attempt was made to end a non-existent transaction.
        /// </summary>
        NotTransacted = 0x80100016,
        /// <summary>
        /// The smart card PIN cache has expired.
        /// </summary>
        PinCacheExpired = 0x80100032,
        /// <summary>
        /// The PCI Receive buffer was too small.
        /// </summary>
        PciTooSmall = 0x80100019,
        /// <summary>
        /// The requested protocols are incompatible with the protocol currently in use with the smart card.
        /// </summary>
        ProtoMismatch = 0x8010000F,
        /// <summary>
        /// The specified reader is not currently available for use.
        /// </summary>
        ReaderUnavailable = 0x80100017,
        /// <summary>
        /// The reader driver does not meet minimal requirements for support.
        /// </summary>
        ReaderUnsupported = 0x8010001A,
        /// <summary>
        /// The smart card is read only and cannot be written to.
        /// </summary>
        ReadOnlyCard = 0x80100034,
        /// <summary>
        /// The smart card has been removed, so that further communication is not possible.
        /// </summary>
        RemovedCard = 0x80100069,
        /// <summary>
        /// The smart card has been reset, so any shared state information is invalid.
        /// </summary>
        ResetCard = 0x80100068,
        /// <summary>
        /// Access was denied because of a security violation.
        /// </summary>
        SecurityViolation = 0x8010006A,
        /// <summary>
        /// The Smart Card Resource Manager is too busy to complete this operation.
        /// </summary>
        ServerTooBusy = 0x80100031,
        /// <summary>
        /// The Smart Card Resource Manager has shut down.
        /// </summary>
        ServiceStopped = 0x8010001E,
        /// <summary>
        /// The smart card cannot be accessed because of other connections outstanding.
        /// </summary>
        SharingViolation = 0x8010000B,
        /// <summary>
        /// The operation has been aborted to allow the server application to exit.
        /// </summary>
        Shutdown = 0x80100018,
        /// <summary>
        /// No error was encountered.
        /// </summary>
        Successs = 0x00000000,
        /// <summary>
        /// The action was cancelled by the system, presumably to log off or shut down.
        /// </summary>
        SystemCancelled = 0x80100012,
        /// <summary>
        /// The user-specified timeout value has expired.
        /// </summary>
        Timeout = 0x8010000A,
        /// <summary>
        /// An unexpected card error has occurred.
        /// </summary>
        Unexpected = 0x8010001F,
        /// <summary>
        /// The specified smart card name is not recognized.
        /// </summary>
        UnknownCard = 0x8010000D,
        /// <summary>
        /// An internal error has been detected, but the source is unknown.
        /// </summary>
        UnknownError = 0x80100014,
        /// <summary>
        /// The specified reader name is not recognized.
        /// </summary>
        UnknownReader = 0x80100009,
        /// <summary>
        /// An unrecognized error code was returned from a layered component.
        /// </summary>
        UnknownResMng = 0x8010002B,
        /// <summary>
        /// Power has been removed from the smart card, so that further communication is not possible.
        /// </summary>
        UnpoweredCard = 0x80100067,
        /// <summary>
        /// The smart card is not responding to a reset.
        /// </summary>
        UnresponsiveCard = 0x80100066,
        /// <summary>
        /// The reader cannot communicate with the smart card, due to ATR configuration conflicts.
        /// </summary>
        UnsupportedCard = 0x80100065,
        /// <summary>
        /// This smart card does not support the requested feature.
        /// </summary>
        UnsupportedFeature = 0x80100022,
        /// <summary>
        /// An internal consistency timer has expired.
        /// </summary>
        WaitedTooLong = 0x80100007,
        /// <summary>
        /// The smart card does not have enough memory to store the information.
        /// </summary>
        WriteTooMany = 0x80100028,
        /// <summary>
        /// The card cannot be accessed because the wrong PIN was presented.
        /// </summary>
        WrongChv = 0x8010006B
    }
}
