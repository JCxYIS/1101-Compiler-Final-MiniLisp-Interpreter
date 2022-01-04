
using System.Collections.Generic;

namespace JC.MiniLisp_Interpreter.Grammar
{
    /// <summary>
    /// STMT ::= EXP | DEF-STMT | PRINT-STMT
    /// </summary>
    public class STMT : IGrammar
    {
        private IGrammar value;

        public STMT(IGrammar exp)
        {
            value = exp;
        }

        /// <summary>
        /// Try parse the parser stack
        /// </summary>
        /// <param name="stack"></param>
        /// <returns>The stack is substitued</returns>
        public static bool TryParse(Stack<object> stack)
        {
            if(stack.Peek() is EXP || stack.Peek() is PRINT_STMT) // TODO DEF-STMT
            {
                // TODO pop the stack and substitue
                return true;
            }

            return false;
        }

        public object Evaluate()
        {
            return value.Evaluate();
        }
    }
}