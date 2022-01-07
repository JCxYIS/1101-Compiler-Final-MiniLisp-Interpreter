using System;
using System.Collections.Generic;
using System.IO;

namespace JC.MiniLisp_Interpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            // Parse passed args
            string filePath = "";
            bool runOnce = false;
            foreach(string arg in args)
            {
                // Console.WriteLine($"\"{arg}\"");
                if(arg == "--quiet")
                {
                    Debug.UseQuiet = true;
                }
                else if(arg == "--debug")
                {
                    Debug.UseDebug = true;
                }
                else if(arg == "--once")
                {
                    runOnce = true;
                }
                else if(arg == "--help")
                {
                    Console.WriteLine("== Version 1.0 ==");
                    Console.WriteLine("Available Args:");
                    Console.WriteLine("--debug : Debug Mode");
                    Console.WriteLine("--help  : lol");
                    Console.WriteLine("--once  : run only once, no recursive");
                    Console.WriteLine("--quiet : Quite Mode");
                    Console.WriteLine("(file path) : Read Mini-Lisp code from file");
                    return;
                }
                else
                {
                    filePath += arg;
                }
            }

            // Main Program :D
            while(true)
            {
                Debug.Print("-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+", ConsoleColor.Red);
                Debug.Print("-+-+- 1101 Compiler Final - MiniLisp Interpreter +-+-+", ConsoleColor.Yellow);
                Debug.Print("-+-+-           Handcrafted by JCxYIS            +-+-+", ConsoleColor.Green);
                Debug.Print("-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+", ConsoleColor.Cyan);

                Debug.Print("Please input your MiniLisp code:", ConsoleColor.Magenta);
                string input = string.IsNullOrEmpty(filePath) ? ReadInput() : ReadFileInput(args[0]);

                Debug.Print("------------------------------------------------------", ConsoleColor.Blue);
                Debug.Print("OK, Here is your output:", ConsoleColor.Magenta);

                Evaluate(input);

                Debug.Print("------------------------------------------------------", ConsoleColor.Blue);
                Debug.Print("Press any key to continue...", ConsoleColor.Magenta);
                Debug.Print("or press Esc to quit :)", ConsoleColor.Magenta);

                if(runOnce || Console.ReadKey().Key == ConsoleKey.Escape)
                    return;

                Console.Clear();
                filePath = "";
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
                Debug.Print(input, ConsoleColor.Gray);
                return input;
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine($"[Error] Input file not found: {filePath}");
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
                Debug.Print($"[Error] {e.Message}\n=== STACK TRACE ===\n{e}", ConsoleColor.Red);
            }
        }
    }
}
