using System;

namespace PcscDotNet.ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = Pcsc<WinSCard>.EstablishContext(SCardScope.User))
            {
                Console.WriteLine(context.IsEstablished);
                context.Release();
                Console.WriteLine(context.IsEstablished);
            }
            Console.WriteLine("Hello World!");
        }
    }
}
