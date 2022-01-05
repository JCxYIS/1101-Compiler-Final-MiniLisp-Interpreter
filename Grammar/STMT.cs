
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

        public static void ParseAll(Stack<object> stack)
        {
            List<STMT> stmts = new List<STMT>();

            while(stack.Count > 0) 
            {
                object o = stack.Peek();
                if(o is EXP || o is PRINT_STMT || o is DEF_STMT) 
                {
                    IGrammar newGrammar = (IGrammar)stack.Pop();
                    stmts.Add( new STMT(newGrammar) );
                }
            }

            // stmts.Reverse();

            foreach(STMT stmt in stmts)
            {
                stack.Push(stmt); 
            }

            // return false;
        }

        public object Evaluate()
        {
            Debug.Log("[STMT] Evaluate "+value);
            return value.Evaluate();
        }
    }
}