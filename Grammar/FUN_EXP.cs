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
        /// Note that the EXP has no definition here.
        /// </summary>
        public Dictionary<string, EXP> FUN_IDS = new Dictionary<string, EXP>();

        public List<EXP> FUN_IDS_inOrder = new List<EXP>();

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
                Debug.Log("[FunConstructor] Fun Mode Start");
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
                    Debug.Log("[FunConstructor] FeedingFunIdIndex=0, Get \"(\" so => 1");
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
                    Debug.Log("[FunConstructor] FeedingFunIdIndex=1, Get \")\" so => -1");
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
            stack.Pop(); // ")"
            object o = stack.Pop();
            if(!(o is EXP))
                throw new Exception("FUN_BODY should be EXP, but get "+o.GetType());
            FUN_BODY = (EXP)o;

            stack.Pop(); // "(" 
            stack.Push(this);
        }

        /// <summary>
        /// Register new local var,
        /// store as undefined EXP.
        /// </summary>
        /// <param name="id"></param>
        private void AddLocalVar(string id)
        {
            if(FUN_IDS.ContainsKey(id))
                throw new Exception($"Local Variable \"{id}\" is already exist.");
            
            // fill in an uninitalized EXP
            EXP undefinedExp = new EXP();
            FUN_IDS.Add(id, undefinedExp);
            FUN_IDS_inOrder.Add(undefinedExp);
            Debug.Log($"[FUNC_EXP] Add Local Variable {id} (Note: this will be stored as undefined EXP)");
        }

        /// <summary>
        /// Find the real name of a local var
        /// Pass null if no such local var.
        /// </summary>
        /// <param name="rawId"></param>
        /// <param name="newId"></param>
        /// <returns></returns>
        public EXP HasLocalVar(string rawId)
        {
            if(FUN_IDS.ContainsKey(rawId))    
            {
                Debug.Log($"[FUNC_EXP] Get Local Variable {rawId}");
                return FUN_IDS[rawId];
            }
            Debug.Log($"[FUNC_EXP] Cannot get Local Variable rawId={rawId}");
            return null;            
        }

        public object Evaluate()
        {            
            Debug.Log("[FUN_EXP] WARNING! You call FUN_EXP.Evaluate() without any param, this might cause error since the local var are still undefined.");
            return FUN_BODY.Evaluate();
        }

        public object Evaluate(List<EXP> param)
        {
            // param length check
            if(param.Count != FUN_IDS_inOrder.Count)
            {
                throw new Exception($"Function param count mismatch: expect {param.Count} but get {FUN_IDS_inOrder.Count}.");
            }

            // plug in the params
            for(int i = 0; i < param.Count; i++)
            {
                FUN_IDS_inOrder[i].InitalizeWithExp(param[i]);
            }

            return FUN_BODY.Evaluate();
        }

        public override string ToString()
        {
            return $"FUN_EXP with {FUN_IDS.Count} arguments";
        }
    }
}