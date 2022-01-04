using System;

namespace JC.MiniLisp_Interpreter
{
    public class Debug
    {
        const bool UseDebug = true;

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
            Console.ForegroundColor = color;
            Console.WriteLine(log);
            Console.ResetColor();
        }
    }
}