
using System.Collections.Generic;

namespace JC.MiniLisp_Interpreter.Grammar
{
    /// <summary>
    /// PROGRAM ::= STMT+
    /// </summary>
    public class PROGRAM : IGrammar
    {
        private List<STMT> stmts;

        public PROGRAM(List<STMT> stmts)
        {
            this.stmts = stmts;
        }

        
        public static PROGRAM ParseAll(Stack<object> stack)
        {
            List<STMT> stmts = new List<STMT>();
            while(stack.Count > 0)
            {
                var o = stack.Pop();
                if(o is not STMT)
                {
                    throw new System.Exception("Found non-STMTS");
                }
                stmts.Add((STMT)o);
            }

            PROGRAM program = new PROGRAM(stmts);
            stack.Push(program);
            return program;
        }

        public object Evaluate()
        {
            foreach(var stmt in stmts)
            {
                Debug.Log("[PROGRAM] Evaluate "+stmt);
                stmt.Evaluate();   
            }
            return null;
        }
    }
}