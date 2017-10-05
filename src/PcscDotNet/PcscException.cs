using System;
using System.ComponentModel;

namespace PcscDotNet
{
    public delegate void PcscExceptionHandler(PcscException error);

    public sealed class PcscException : Win32Exception
    {
        public SCardError Error { get; private set; }

        public bool ThrowIt { get; set; } = true;

        public PcscException(SCardError error) : base((int)error)
        {
            Error = error;
        }
    }
}
