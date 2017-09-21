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
                context.Validate();
                context.Release();
                Console.WriteLine(context.IsEstablished);
                try
                {
                    context.Validate();
                }
                catch (PcscException ex)
                {
                    Console.WriteLine($"0x{ex.NativeErrorCode:X8}: {ex.Message}");
                }
            }
            Console.WriteLine("Hello World!");
        }
    }
}
