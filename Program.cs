using System;
using System.Collections.Generic;
using System.IO;

namespace JC.MiniLisp_Interpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            bool useFileInput = args.Length != 0;
            while(true)
            {
                Console.WriteLine("-+-+- 1101 Compiler Final - MiniLisp Interpreter +-+-+");
                Console.WriteLine("--------------------------------------------------------------------------");
                Console.WriteLine("Please input your MiniLisp code:");
                string input = useFileInput ? ReadFileInput(args[0]) : ReadInput();
                Console.WriteLine("--------------------------------------------------------------------------");
                Console.WriteLine("Here is the output:");
                Evaluate(input);
                Console.WriteLine("--------------------------------------------------------------------------");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                useFileInput = false;
            }
        }

        /// <summary>
        /// Read user input from terminal
        /// </summary>
        /// <returns></returns>
        private static string ReadInput()
        {
            // Read input
            string input = "";
            while(true)
            {
                string buffer = Console.ReadLine();
                if(buffer == null)
                {
                    return input;
                }
                input += buffer + "\n";
            }            
        }

        /// <summary>
        /// Read input from file
        /// </summary>
        /// <returns></returns>
        private static string ReadFileInput(string filePath)
        {
            try
            {
                string input = File.ReadAllText(filePath);
                Console.WriteLine(input);
                return input;
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine($"[Error] Input file found: {filePath}");
                return "";
            }
        }

        /// <summary>
        /// Evaluate the miniLisp code
        /// </summary>
        /// <returns></returns>
        private static string Evaluate(string lispCode)
        {
            Interpreter interpreter = new Interpreter(lispCode);
            return interpreter.Evaluate();
        }
    }
}
