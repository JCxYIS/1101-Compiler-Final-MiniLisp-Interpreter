
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

        public Stack<object> TryParse(Stack<object> stack)
        {
            throw new System.NotImplementedException();
        }

        public object Evaluate()
        {
            Console.WriteLine(r.Evaluate());
            // no return value
            return null;
        }
    }
}