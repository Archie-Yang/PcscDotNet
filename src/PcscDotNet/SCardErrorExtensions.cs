using System;
using System.ComponentModel;

namespace PcscDotNet
{
    public static class SCardErrorExtensions
    {
        public static void ThrowIfNotSuccess(this SCardError error)
        {
            if (error != SCardError.Successs)
            {
                throw new PcscException(error);
            }
        }
    }
}
