using System.Collections.Generic;
using JC.MiniLisp_Interpreter.Utility;

namespace JC.MiniLisp_Interpreter.Grammar
{
    /// <summary>
    /// DEF-STMT ::= (define VARIABLE EXP)
    ///     VARIABLE ::= id
    /// </summary>
    public class DEF_STMT : IGrammar
    {
        public string VARIABLE;
        public EXP exp;

        public DEF_STMT(string var, EXP exp)
        {
            var.CheckLegalId();
            VARIABLE = var;
            Interpreter.Instance.SetVariable(VARIABLE, exp);
            this.exp = exp;
        }

        public static bool TryParse(Stack<object> stack)
        {
            var match = stack.MatchFuncName("define");
            if(match is null)
                return false;
            
            // length check
            if(match.Count != 3)
                throw new System.Exception($"\"define\" Need exact 2 arguments, but got {match.Count-1}.");

            // type checking
            if(match[1] is string && match[2] is EXP)
            {
                stack.Push(new DEF_STMT((string)match[1], (EXP)match[2]));
                return true;
            }
            else
            {
                return false;
            }

        }

        public object Evaluate()
        {            
            return null;
        }
    }
}