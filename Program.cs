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
                Debug.Print("-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+", ConsoleColor.Red);
                Debug.Print("-+-+- 1101 Compiler Final - MiniLisp Interpreter +-+-+", ConsoleColor.Yellow);
                Debug.Print("-+-+-             Crafted by JCxYIS              +-+-+", ConsoleColor.Green);
                Debug.Print("-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+", ConsoleColor.Cyan);

                Debug.Print("Please input your MiniLisp code:", ConsoleColor.Magenta);
                string input = useFileInput ? ReadFileInput(args[0]) : ReadInput();

                Debug.Print("------------------------------------------------------", ConsoleColor.Blue);
                Debug.Print("OK, Here is your output:", ConsoleColor.Magenta);

                Evaluate(input);

                Debug.Print("------------------------------------------------------", ConsoleColor.Blue);
                Debug.Print("Press any key to continue...", ConsoleColor.Magenta);
                Debug.Print("or press Esc to quit :)", ConsoleColor.Magenta);

                if(Console.ReadKey().Key == ConsoleKey.Escape)
                    return;

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
        private static void Evaluate(string lispCode)
        {
            try
            {
                Interpreter interpreter = new Interpreter(lispCode);        
                string output = interpreter.Evaluate();
                // Console.Write(output); // "This is virtual IO"
            }
            catch (Exception e)
            {
                Debug.Print("Error! " + e, ConsoleColor.Red);
            }
        }
    }
}
