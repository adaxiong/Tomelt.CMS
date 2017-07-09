using System;
using System.IO;

namespace Tomelt {
    public class Program {
        const int ConsoleInputBufferSize = 8192;

        public static int Main(string[] args) {
            Console.SetIn(new StreamReader(Console.OpenStandardInput(ConsoleInputBufferSize)));
            return (int)new TomeltHost(Console.In, Console.Out, args).Run();
        }
    }
}
