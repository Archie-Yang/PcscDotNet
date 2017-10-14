using System;

namespace PcscDotNet
{
    /// <summary>
    /// Reader action I/O functions.
    /// </summary>
    public enum SCardControlFunction
    {
        /// <summary>
        /// IOCTL_CCID_ESCAPE.
        /// </summary>
        CcidEscape = 3500,
        /// <summary>
        /// IOCTL_SMARTCARD_CONFISCATE.
        /// </summary>
        SmartCardConfiscate = 4,
        /// <summary>
        /// IOCTL_SMARTCARD_EJECT.
        /// </summary>
        SmartCardEject = 6,
        /// <summary>
        /// IOCTL_SMARTCARD_GET_ATTRIBUTE.
        /// </summary>
        SmartCardGetAttribute = 2,
        /// <summary>
        /// IOCTL_SMARTCARD_GET_FEATURE_REQUEST.
        /// </summary>
        SmartCardGetFeatureRequest = 3400,
        /// <summary>
        /// IOCTL_SMARTCARD_GET_LAST_ERROR.
        /// </summary>
        SmartCardGetLastError = 15,
        /// <summary>
        /// IOCTL_SMARTCARD_GET_PERF_CNTR.
        /// </summary>
        SmartCardGetPerfCntr = 16,
        /// <summary>
        /// IOCTL_SMARTCARD_GET_STATE.
        /// </summary>
        SmartCardGetState = 14,
        /// <summary>
        /// IOCTL_SMARTCARD_IS_ABSENT.
        /// </summary>
        SmartCardIsAbsent = 11,
        /// <summary>
        /// IOCTL_SMARTCARD_IS_PRESENT.
        /// </summary>
        SmartCardIsPresent = 10,
        /// <summary>
        /// IOCTL_SMARTCARD_POWER.
        /// </summary>
        SmartCardPower = 1,
        /// <summary>
        /// IOCTL_SMARTCARD_READ.
        /// </summary>
        [Obsolete]
        SmartCardRead = 8,
        /// <summary>
        /// IOCTL_SMARTCARD_SET_ATTRIBUTE.
        /// </summary>
        SmartCardSetAttribute = 3,
        /// <summary>
        /// IOCTL_SMARTCARD_SET_PROTOCOL.
        /// </summary>
        SmartCardSetProtocol = 12,
        /// <summary>
        /// IOCTL_SMARTCARD_SWALLOW.
        /// </summary>
        SmartCardSwallow = 7,
        /// <summary>
        /// IOCTL_SMARTCARD_TRANSMIT.
        /// </summary>
        SmartCardTransmit = 5,
        /// <summary>
        /// IOCTL_SMARTCARD_WRITE.
        /// </summary>
        [Obsolete]
        SmartCardWrite = 9
    }
}
