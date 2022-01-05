using System;

namespace JC.MiniLisp_Interpreter
{
    public class Debug
    {
        /// <summary>
        /// If true, only white-colored output will be printed
        /// </summary>
        public static bool UseQuiet = false;

        /// <summary>
        /// If true, Debug.Log will print out logs.
        /// </summary>
        public static bool UseDebug = false;

        /// <summary>
        /// Debug Log
        /// </summary>
        /// <param name="log"></param>
        public static void Log(object log)
        {
            if(UseDebug)
            {
                Print(log, ConsoleColor.DarkGray);
            }
        }

        /// <summary>
        /// Colorful Console.WriteLine
        /// </summary>
        /// <param name="log"></param>
        /// <param name="color"></param>
        public static void Print(object log, ConsoleColor color = ConsoleColor.White)
        {
            if(UseQuiet && color != ConsoleColor.White)
                return;
            
            Console.ForegroundColor = color;
            Console.WriteLine(log);
            Console.ResetColor();
        }
    }
}