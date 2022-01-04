
using System;
using System.Collections.Generic;

namespace JC.MiniLisp_Interpreter.Grammar
{
    public class PRINT_STMT : IGrammar
    {
        public EXP r;

        public PRINT_STMT(EXP exp)
        {
            r = exp;
        }

        /// <summary>
        /// Try parse the parser stack
        /// </summary>
        /// <param name="stack"></param>
        /// <returns>The stack is substitued</returns>
        public static bool TryParse(Stack<object> stack)
        {
            // if(stack.Peek() is STMT)
            // {
            //     // TODO pop the stack and transform to PROGRAM
            //     return true;
            // }

            return false;
        }

        public object Evaluate()
        {
            Console.WriteLine(r.Evaluate());
            // no return value
            return null;
        }
    }
}