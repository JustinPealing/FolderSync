using System;

namespace FolderSync
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                PrintUsage();
            }
            new FolderSync(args[2]).CopyFiles(args[0], args[1]);
        }

        private static void PrintUsage() =>
            Console.WriteLine("FolderSync <source> <desintation> <record>");
    }
}
