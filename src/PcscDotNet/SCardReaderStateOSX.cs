using System;
using System.Runtime.InteropServices;

namespace PcscDotNet
{
    /// <summary>
    /// This structure is used by functions for tracking smart cards within readers.
    /// (For OS X.)
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SCardReaderStateOSX : ISCardReaderState
    {
        /// <summary>
        /// A pointer to the name of the reader being monitored.
        /// Set the value of this member to "\\\\?PnP?\\Notification" and the values of all other members to zero to be notified of the arrival of a new smart card reader.
        /// </summary>
        public unsafe void* Reader;

        /// <summary>
        /// Not used by the smart card subsystem.
        /// This member is used by the application.
        /// </summary>
        public unsafe void* UserData;

        /// <summary>
        /// Current state of the reader, as seen by the application, combination, as a bitmask. 
        /// </summary>
        public SCardReaderStates CurrentState;

        /// <summary>
        /// Current state of the reader, as known by the smart card resource manager, combination, as a bitmask. 
        /// </summary>
        public SCardReaderStates EventState;

        /// <summary>
        /// Number of bytes in the returned `ATR`.
        /// </summary>
        public int AtrLength;

        /// <summary>
        /// ATR of the inserted card.
        /// </summary>
        public unsafe fixed byte Atr[33];

        unsafe byte[] ISCardReaderState.Atr
        {
            get
            {
                if (AtrLength <= 0) return null;
                var atr = new byte[AtrLength];
                fixed (byte* fAtr = Atr)
                {
                    Marshal.Copy((IntPtr)fAtr, atr, 0, atr.Length);
                }
                return atr;
            }
        }

        SCardReaderStates ISCardReaderState.CurrentState
        {
            get
            {
                return CurrentState;
            }
            set
            {
                CurrentState = value;
            }
        }

        SCardReaderStates ISCardReaderState.EventState
        {
            get
            {
                return EventState;
            }
            set
            {
                EventState = value;
            }
        }

        unsafe void* ISCardReaderState.Reader
        {
            get
            {
                return Reader;
            }
            set
            {
                Reader = value;
            }
        }
    }
}
