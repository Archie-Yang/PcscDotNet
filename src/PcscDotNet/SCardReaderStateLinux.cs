using System;
using System.Runtime.InteropServices;

namespace PcscDotNet
{
    /// <summary>
    /// This structure is used by functions for tracking smart cards within readers.
    /// (For Linux.)
    /// </summary>
    public struct SCardReaderStateLinux : ISCardReaderState
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
        public IntPtr CurrentState;

        /// <summary>
        /// Current state of the reader, as known by the smart card resource manager, combination, as a bitmask. 
        /// </summary>
        public IntPtr EventState;

        /// <summary>
        /// Number of bytes in the returned `ATR`.
        /// </summary>
        public IntPtr AtrLength;

        /// <summary>
        /// ATR of the inserted card.
        /// </summary>
        public unsafe fixed byte Atr[33];

        unsafe byte[] ISCardReaderState.Atr
        {
            get
            {
                var atrLength = (int)AtrLength;
                if (atrLength <= 0) return null;
                var atr = new byte[atrLength];
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
                return (SCardReaderStates)CurrentState;
            }
            set
            {
                CurrentState = (IntPtr)value;
            }
        }

        SCardReaderStates ISCardReaderState.EventState
        {
            get
            {
                return (SCardReaderStates)EventState;
            }
            set
            {
                EventState = (IntPtr)value;
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
