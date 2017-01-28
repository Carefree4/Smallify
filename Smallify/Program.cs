using System;
using System.IO;
using System.Linq;

namespace Smallify
{
    internal class Program
    {
        private const string SmalledExtensionPrefix = ".smallify";

        public static void Main(string[] args)
        {
            const string banner = "######################Smallify######################" + 
                                  "# Will grab the first lines of a file and put it   #" +
                                  "# in a new file at the original file path with the #" +
                                  "#          extension prefix of .smallify           #" +
                                  "####################################################";
            Console.Write(banner + "\n\n\n");

            var filePath = GetFilePath();
            var numberOfLines = GetNumberOfLines();

            try
            {
                using (var sr = new StreamReader(filePath))
                {
                    using (var sw = new StreamWriter(GetSmalledFilePath(filePath)))
                    {
                        Console.WriteLine("Starting to write files... (# = 100 lines)");
                        Console.Write("[");
                        for (var i = 1; i < numberOfLines; i++)
                        {
                            sw.WriteLine(sr.ReadLine());

                            if (i % 100 == 0)
                                Console.Write("#");
                        }
                        Console.Write("]\n Done!");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\nAn error occurred, process canceled:");
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

        private static string GetSmalledFilePath(string path)
        {
            var filePath = path.Split('\\');

            // newPath to everything BUT the filename
            var newPath = "";
            for (var i = 0; i < filePath.Length - 1; i++)
            {
                var s = filePath[i];
                newPath += s + "\\";
            }

            // Give filename prefix
            var fileName = filePath[filePath.Length - 1];
            if (fileName.Contains('.'))
            {
                var extStart = fileName.LastIndexOf('.');
                newPath += fileName.Insert(extStart, SmalledExtensionPrefix);
            }
            else
            {
                newPath += fileName + SmalledExtensionPrefix;
            }


            return newPath;
        }

        private static string GetFilePath()
        {
            var filePath = string.Empty;
            while (string.IsNullOrWhiteSpace(filePath))
            {
                Console.WriteLine("Large file path: ");
                filePath = Console.ReadLine();
            }

            return filePath;
        }

        private static int GetNumberOfLines()
        {
            var numberOfLines = 0;
            while (numberOfLines <= 0)
            {
                Console.WriteLine("How many lines to copy?");
                int.TryParse(Console.ReadLine(), out numberOfLines);
            }

            return numberOfLines;
        }
    }
}
