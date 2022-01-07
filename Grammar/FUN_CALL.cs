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
        private FUN_EXP fun_exp;
        private List<EXP> param;

        public FUN_CALL(FUN_EXP fun_exp, List<EXP> param)
        {
            this.fun_exp = fun_exp;
            this.param = param;
        }
        public FUN_CALL(FUN_EXP fun_exp, List<object> param)
        {
            this.fun_exp = fun_exp;
            List<EXP> paramExp = new List<EXP>();
            for(int i = 0; i < param.Count; i++)
            {
                if(param[i] is EXP)
                    paramExp.Add((EXP)param[i]);
                else
                    throw new Exception($"FUN_CALL: PARAM should be EXP, but get {param[i].GetType()}");
            }
            this.param = paramExp;
        }

        /// <summary>
        /// Try parse the parser stack
        /// </summary>
        /// <param name="stack"></param>
        /// <returns>The stack is substitued</returns>
        public static bool TryParse(Stack<object> stack)
        {
            // reversed stack (list)
            List<object> stackReverse = new List<object>(stack);

            // match (FUN-EXP PARAM*)
            var match1 = stack.MatchFuncType(typeof(FUN_EXP));
            if(match1 != null)
            {
                Debug.Log($"[FUN_CALL] Match (FUN-EXP PARAM*) with param count={match1.Count-1}");

                FUN_EXP fun = (FUN_EXP)match1[0];
                match1.RemoveAt(0);                
                stack.Push(new FUN_CALL(fun, match1));

                return true;
            }

            // match (FUN-NAME PARAM*)
            // handled in EXP (this kind of fun is treated as a variable)

            return false;
        }

        public object Evaluate()
        {            
            return fun_exp.Evaluate(param);
            // return null;
        }

        public override string ToString()
        {
            return $"FUNC_CALL";
        }
    }
}