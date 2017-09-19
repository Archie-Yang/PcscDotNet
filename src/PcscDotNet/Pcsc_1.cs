using System;

namespace PcscDotNet
{
    public static class Pcsc<T> where T : class, IPcscProvider, new()
    {
        private static readonly Pcsc _instance = new Pcsc(new T());

        public static Pcsc Instance => _instance;
    }
}