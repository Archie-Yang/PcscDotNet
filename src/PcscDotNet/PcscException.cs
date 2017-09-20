using System;
using System.ComponentModel;

namespace PcscDotNet
{
    public class PcscException : Win32Exception
    {
        public SCardError Error => (SCardError)NativeErrorCode;

        public PcscException() { }

        public PcscException(int error) : base(error) { }

        public PcscException(SCardError error) : base((int)error) { }

        public PcscException(string message) : base(message) { }

        public PcscException(int error, string message) : base(error, message) { }

        public PcscException(string message, Exception innerException) : base(message, innerException) { }
    }
}
