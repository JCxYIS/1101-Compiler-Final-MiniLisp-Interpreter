using System;
using System.Collections.Generic;
using JC.MiniLisp_Interpreter.Utility;

namespace JC.MiniLisp_Interpreter.Grammar
{
    /// <summary>
    /// FUN-CALL ::= (FUN-EXP PARAM*) | (FUN-NAME PARAM*)
    ///     PARAM ::= EXP
    ///     LAST-EXP ::= EXP
    ///     FUN-NAME ::= id
    /// </summary>
    public class FUN_CALL : IGrammar
    {        

        public FUN_CALL()
        {

        }

        /// <summary>
        /// Try parse the parser stack
        /// </summary>
        /// <param name="stack"></param>
        /// <returns>The stack is substitued</returns>
        public static bool TryParse(Stack<object> stack)
        {
            // (FUN-EXP PARAM*)
            if(stack.Peek() is FUN_EXP)
            {
                
            }
            return false;
        }

        public object Evaluate()
        {            
            // TODO eval
            // return .Evaluate();
            return null;
        }

        public override string ToString()
        {
            return $"FUNC_CALL";
        }
    }
}