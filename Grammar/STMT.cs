
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

        public Stack<object> TryParse(Stack<object> stack)
        {
            throw new System.NotImplementedException();
        }

        public object Evaluate()
        {
            return value.Evaluate();
        }
    }
}