using System.Collections.Generic;
using System.Linq;
using JC.MiniLisp_Interpreter.Utility;

namespace JC.MiniLisp_Interpreter.Grammar
{
    /// <summary>
    /// LOGICAL-OP ::= AND-OP | OR-OP | NOT-OP
    ///     AND-OP ::= (and EXP EXP+)
    ///     OR-OP ::= (or EXP EXP+)
    ///     NOT-OP ::= (not EXP)
    /// </summary>
    public class LOGICAL_OP : IGrammar
    {
        public string op;
        public List<EXP> elements;

        public LOGICAL_OP(string op, params EXP[] elements)
        {
            this.op = op;
            this.elements = elements.ToList();

            //  "and", "or" support more than 2 elements
            int len = elements.Length;
            if(op == "and" || op == "or")
            {
                if(len < 2)
                    throw new System.Exception($"\"{op}\" Need 2 or more arguments, but got {len}.");
            }
            else
            {
                // "not 
                if(len != 1)
                    throw new System.Exception($"\"{op}\" Need exact 1 arguments, but got {len}.");
            }

        }


        /// <summary>
        /// Try parse the parser stack
        /// </summary>
        /// <param name="stack"></param>
        /// <returns>The stack is substitued</returns>
        public static bool TryParse(Stack<object> stack)
        {
            var match = stack.MatchFuncName("and", "or", "not");
            if(match is null)
                return false;

            Debug.Log($"[LOGICAL_OP] match op={match[0]} param={match.Count-1}");
            
            // make new 
            string funcName = match[0].ToString();
            List<EXP> exp = new List<EXP>();
            for(int i = 1; i < match.Count; i++)
            {
                if( !(match[i] is EXP) )
                    throw new System.Exception($"Syntax error, expect EXP but get \"{match[i]}\"({match[i].GetType()}).");
                EXP e = (EXP)match[i];
                exp.Add(e);
            }
            stack.Push( new LOGICAL_OP(funcName, exp.ToArray()) );
            
            return true;            
        }


        private EXP l => elements[0];
        private EXP r => elements[1];
        public object Evaluate()
        {
            switch(op)
            {
                case "and": return AndAll();
                case "or":  return OrAll();
                case "not": return !l.Evaluate<bool>();
            }
            throw new System.Exception("Num op messed up");
        }    

        private bool AndAll()
        {
            bool result = true;
            foreach(var e in elements)
            {
                result &= e.Evaluate<bool>();
            }
            return result;
        }    
        private bool OrAll()
        {
            bool result = false;
            foreach(var r in elements)
            {
                result |= r.Evaluate<bool>();
            }
            return result;
        }      
    }
}