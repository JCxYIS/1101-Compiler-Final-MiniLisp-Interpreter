using System;
using System.Collections.Generic;
using JC.MiniLisp_Interpreter.Utility;

namespace JC.MiniLisp_Interpreter.Grammar
{
    /// <summary>
    /// NOTE: in provided test_case, I discovered that "fun" is replaced with "lambda" :/
    /// FUN-EXP ::= (fun FUN_IDs FUN-BODY)
    ///     FUN-IDs ::= (id*)
    ///     FUN-BODY ::= EXP
    ///     // FUN-CALL ::= (FUN-EXP PARAM*) | (FUN-NAME PARAM*)
    ///     PARAM ::= EXP
    ///     LAST-EXP ::= EXP
    ///     FUN-NAME ::= id
    /// </summary>
    public class FUN_EXP : IGrammar
    {        
        /// <summary>
        /// -1: not feeding
        /// 0: feeding first (if no "("), otherwise it's "(" and we finish feeding
        /// 1: feeding first (if "(" has read), and we keep reading until ")" present
        /// </summary>
        int feedingFunIdIndex = 0;

        /// <summary>
        /// Add a counter to see when
        /// </summary>
        int funBodyIndex = 1;

        /// <summary>
        /// FUN-IDs: original id (FUN-ID) and its "wrapped" id
        /// a.k.a. Local Variable
        /// </summary>
        public Dictionary<string, string> FUN_IDS = new Dictionary<string, string>();

        /// <summary>
        /// FUN-BODY
        /// </summary>
        public EXP FUN_BODY;

        public FUN_EXP()
        {
            
        }

        /// <summary>
        /// Should we start fun mode?
        /// </summary>
        /// <param name="stack"></param>
        /// <returns></returns>
        public static FUN_EXP CheckFunMode(Stack<object> stack)
        {
            object top = stack.Peek();
            if(stack.IsStringAndEndWith("func") || stack.IsStringAndEndWith("lambda"))
            {
                stack.Pop(); // "func" or "lambda"\
                // stack.Pop(); // "("
                return new FUN_EXP();
            }
            return null;
        }

        /// <summary>
        /// hahaha so funny ;(
        /// </summary>
        /// <param name="stack"></param>
        /// <returns></returns>
        public bool FeedingFUN_IDs(Stack<object> stack)
        {
            if(feedingFunIdIndex == -1)
            {
                return false;
            }
            else if(feedingFunIdIndex == 0)
            {
                if(stack.IsStringAndEndWith("("))
                {
                    feedingFunIdIndex = 1;
                }
                else
                {
                    AddLocalVar((string)stack.Peek());
                    feedingFunIdIndex = -1;
                }
            }
            else if(feedingFunIdIndex == 1)
            {
                if(stack.IsStringAndEndWith(")"))
                {
                    feedingFunIdIndex = -1;
                }
                else
                {
                    AddLocalVar((string)stack.Peek());
                }
            }
            stack.Pop();
            Debug.Log("[FunConstructor] FeedingFunIdIndex="+feedingFunIdIndex);
            return true;
        }

        /// <summary>
        /// Should the pushed token be the end of this fun constructor?
        /// </summary>
        /// <param name="stack"></param>
        /// <returns></returns>
        public bool ScanEndOfConstructor(Stack<object> stack)
        {
            if(stack.IsStringAndEndWith("("))
                funBodyIndex++;
            else if(stack.IsStringAndEndWith(")"))
                funBodyIndex--;

            Debug.Log("[FunConstructor] FunBodyIndex="+funBodyIndex);
            return funBodyIndex == 0;
        }

        /// <summary>
        /// Check the top of stack is EXP, complete the construction
        /// </summary>
        /// <param name="stack"></param>
        public void EndOfConstructor(Stack<object> stack)
        {
            object o = stack.Pop();
            if(!(o is EXP))
                throw new Exception("FUN_BODY should be EXP, but get "+o.GetType());
            FUN_BODY = (EXP)o;

            stack.Pop(); // "(" 
            stack.Push(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawId"></param>
        private void AddLocalVar(string rawId)
        {
            if(FUN_IDS.ContainsKey(rawId))
                throw new Exception($"Local Variable \"{rawId}\" is already exist.");
            
            FUN_IDS.Add(rawId, $"LocalVar_{GetHashCode()}_{rawId}");
            Debug.Log($"[FUNC_EXP] Add Local Var {rawId} => {FUN_IDS[rawId]}");
        }

        /// <summary>
        /// Find the real name of a local var
        /// Pass original id if no such local var.
        /// </summary>
        /// <param name="rawId"></param>
        /// <param name="newId"></param>
        /// <returns></returns>
        public string GetLocalVar(string rawId)
        {
            if(FUN_IDS.ContainsKey(rawId))    
            {
                Debug.Log($"[FUNC_EXP] Get Local Var {rawId} => {FUN_IDS[rawId]}");
                return FUN_IDS[rawId];
            }
            
            return rawId;
        }

        public object Evaluate()
        {            
            // TODO eval
            return FUN_BODY.Evaluate();
        }

        public override string ToString()
        {
            return $"FUNC_EXP with {FUN_IDS.Count} arguments";
        }
    }
}