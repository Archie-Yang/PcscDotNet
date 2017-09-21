using System;
using System.ComponentModel;
using System.Linq;

namespace PcscDotNet
{
    public static class SCardErrorExtensions
    {
        public static void Throw(this SCardError error)
        {
            throw new PcscException(error);
        }

        public static void ThrowIfAny(this SCardError error, params SCardError[] errors)
        {
            if (errors?.Contains(error) == true)
            {
                throw new PcscException(error);
            }
        }

        public static void ThrowIfNotSuccess(this SCardError error)
        {
            if (error != SCardError.Successs)
            {
                throw new PcscException(error);
            }
        }
    }
}
