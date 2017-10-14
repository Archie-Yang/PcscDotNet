using System;

namespace PcscDotNet
{
    /// <summary>
    /// Extension methods for SCardError.
    /// </summary>
    public static class SCardErrorExtensions
    {
        public static void Throw(this SCardError error, PcscExceptionHandler onException = null)
        {
            var exception = new PcscException(error);
            if (onException != null)
            {
                onException(exception);
                if (!exception.ThrowIt) return;
            }
            throw exception;
        }

        public static void ThrowIfNotSuccess(this SCardError error, PcscExceptionHandler onException = null)
        {
            if (error != SCardError.Successs)
            {
                Throw(error, onException);
            }
        }
    }
}
