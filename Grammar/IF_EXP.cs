using System.Collections.Generic;
using System.Linq;
using JC.MiniLisp_Interpreter.Utility;

namespace JC.MiniLisp_Interpreter.Grammar
{
    /// <summary>
    /// IF-EXP ::= (if TEST-EXP THEN-EXP ELSE-EXP)
    ///     TEST-EXP ::= EXP
    ///     THEN-EXP ::= EXP
    ///     ELSE-EXP ::= EXP
    /// </summary>
    public class IF_EXP : IGrammar
    {
        public EXP testExp;
        public EXP thenExp;
        public EXP elseExp;

        public IF_EXP(EXP test, EXP then, EXP elseexp)
        {
            testExp = test;
            thenExp = then;
            elseExp = elseexp;
        }


        /// <summary>
        /// Try parse the parser stack
        /// </summary>
        /// <param name="stack"></param>
        /// <returns>The stack is substitued</returns>
        public static bool TryParse(Stack<object> stack)
        {
            var match = stack.MatchFuncName("if");
            if(match is null)
                return false;

            Debug.Log($"[IF] match op={match[0]} param={match.Count-1}");
            
            // param count check
            if(match.Count - 1 != 3)
                throw new System.Exception($"\"if\" Need exact 3 arguments, but got {match.Count-1}.");
            
            // type check
            for(int i = 1; i < match.Count; i++)
            {
                if( !(match[i] is EXP) )
                    throw new System.Exception($"Syntax error, expect EXP but get \"{match[i]}\"({match[i].GetType()}).");
            }

            stack.Push( new IF_EXP((EXP)match[1], (EXP)match[2], (EXP)match[3]) );            
            return true;            
        }


        public object Evaluate()
        {
            if(testExp.Evaluate<bool>())
            {
                return thenExp.Evaluate();
            }
            else
            {
                return elseExp.Evaluate();
            }
        }    
      
    }
}