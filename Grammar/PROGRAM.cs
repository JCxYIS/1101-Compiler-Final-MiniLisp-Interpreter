
using System.Collections.Generic;

namespace JC.MiniLisp_Interpreter.Grammar
{
    /// <summary>
    /// PROGRAM ::= STMT+
    /// </summary>
    public class PROGRAM : IGrammar
    {
        private STMT stmt;

        public PROGRAM(STMT stmt)
        {
            this.stmt = stmt;
        }

        /// <summary>
        /// Try parse the parser stack
        /// </summary>
        /// <param name="stack"></param>
        /// <returns>The stack is substitued</returns>
        public static bool TryParse(Stack<object> stack)
        {
            if(stack.Peek() is STMT)
            {
                // TODO pop the stack and transform to PROGRAM
                return true;
            }

            return false;
        }

        public object Evaluate()
        {
            return stmt.Evaluate();
        }
    }
}