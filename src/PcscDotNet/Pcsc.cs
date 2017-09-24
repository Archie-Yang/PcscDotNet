using System;
using System.Collections.Generic;

namespace PcscDotNet
{
    public class Pcsc
    {
        private static readonly char[] _nullCharacter = new char[] { '\0' };

        private readonly IPcscProvider _provider;

        public IPcscProvider Provider => _provider;

        public Pcsc(IPcscProvider provider)
        {
            _provider = provider;
        }

        public PcscContext CreateContext()
        {
            return new PcscContext(this);
        }

        public PcscContext EstablishContext(SCardScope scope)
        {
            return new PcscContext(this, scope);
        }

        public string[] GetReaderNames(SCardReaderGroup group = SCardReaderGroup.NotSpecified)
        {
            return GetReaderNames(SCardContext.Default, group);
        }

        internal unsafe string[] GetReaderNames(SCardContext handle, SCardReaderGroup group = SCardReaderGroup.NotSpecified)
        {
            var provider = _provider;
            var encoding = provider.CharacterEncoding;
            var groupValue = group.GetDefinedValue();
            var nullCharacter = _nullCharacter;
            var charCount = 0;
            /*
               Currently, can not find the better way using auto-allocate to receive reader names with different encoding.
               Different providers may use different encoding, it's could be out of standard text encodings.
               Find `NULL` characters using back forward method is slower, and the order of reader names becomes descending also.
               > If you have any idea, welcome to share, thanks!
            */
            var err = provider.SCardListReaders(handle, groupValue, null, &charCount);
            if (err != SCardError.NoReadersAvailable) err.ThrowIfNotSuccess();
            for (; ; )
            {
                var bufferLength = encoding.GetMaxByteCount(charCount);
                var buffer = new byte[bufferLength];
                fixed (byte* hBuffer = &buffer[0])
                {
                    var pBuffer = hBuffer;
                    err = provider.SCardListReaders(handle, groupValue, pBuffer, &charCount);
                    switch (err)
                    {
                        case SCardError.InsufficientBuffer:
                            // Buffer is too small, may due to newer reader is introduced to current context.
                            continue;
                        case SCardError.NoReadersAvailable:
                            // In Windows, it seems to still return a `NULL` character with `SCardError.Success` status even none of reader names is found.
                            return new string[0];
                        default:
                            err.ThrowIfNotSuccess();
                            return encoding.GetString(pBuffer, bufferLength).TrimEnd(nullCharacter).Split(nullCharacter, StringSplitOptions.RemoveEmptyEntries);
                    }
                }
            }
        }
    }
}
