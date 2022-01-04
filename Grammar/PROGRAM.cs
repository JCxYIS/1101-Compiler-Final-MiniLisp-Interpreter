
using System.Collections.Generic;

namespace JC.MiniLisp_Interpreter.Grammar
{
    public class PROGRAM : IGrammar
    {
        private STMT stmt;

        public PROGRAM(STMT stmt)
        {
            this.stmt = stmt;
        }

        public Stack<object> TryParse(Stack<object> stack)
        {
            throw new System.NotImplementedException();
        }

        public object Evaluate()
        {
            return stmt.Evaluate();
        }
    }
}