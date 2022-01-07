using System;
using System.Collections.Generic;

namespace JC.MiniLisp_Interpreter.Grammar
{
    /// <summary>
    /// EXP ::= bool-val | number | VARIABLE | NUM-OP | LOGICAL-OP | FUN-EXP | FUN-CALL | IF-EXP
    /// </summary>
    public class EXP : IGrammar
    {       
        private Type type;
        private object value;     

        /// <summary>
        /// !Initalized
        /// </summary>
        public bool IsUndefined => type is null || value is null;   

        private bool IsTerminal =>
            type == typeof(bool) || // boolean
            type == typeof(double); // number
            // type == typeof(string);

        public EXP(Type typeofVal, object val)
        {
            type = typeofVal;
            value = val;
        }   

        public EXP()
        {
            // uninited EXP
        }

        public void InitalizeWithExp(EXP exp)
        {
            if(!IsUndefined)
                throw new NotImplementedException("ReInitalize detected! That is not supported yet.");
            type = exp.type;
            value = exp.value;
        } 

        /// <summary>
        /// Try parse the parser stack
        /// </summary>
        /// <param name="stack"></param>
        /// <returns>The stack is substitued</returns>
        public static bool TryParse(Stack<object> stack)
        {
            object top = stack.Peek();

            // for number and bool, scanner has already done the job.

            // Parse these stuffs
            if(
                false
                || top is NUM_OP 
                || top is LOGICAL_OP 
                // || FUN-EXP 
                || top is FUN_CALL
                || top is IF_EXP)
            {
                top = stack.Pop();
                stack.Push(new EXP(top.GetType(), top));
                return true;
            }
            
            // Parse VARIABLES
            if(top is string)
            {
                if(Interpreter.Instance.ContainsVariable((string)top))
                {
                    top = stack.Pop();
                    EXP exp = Interpreter.Instance.GetVariable((string)top);
                    stack.Push(exp);
                    Debug.Log($"[EXP] Push variable of name {top}");
                    return true;
                }
                else
                {
                    Debug.Log($"[EXP] string \"{top}\" is not a variable, remain string.");
                }
            }
            
            return false;
        }

        /// <summary>
        /// Generic evaluate
        /// </summary>
        /// <returns></returns>
        public object Evaluate()
        {
            if(IsUndefined)
                throw new Exception("EXP is undefined!");
            if(IsTerminal)
                return value;
            else
                return ((IGrammar)value).Evaluate();
        }

        /// <summary>
        /// Evaluate as type T.
        /// If output type mismatch, we will throw an exception
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Evaluate<T>()
        {
            object val = Evaluate();
            if(!(val is T))
                throw new Exception($"Evaluate {type}, expect {typeof(T)}, but get {val.GetType()}");
            T t = (T)val;
            Debug.Log($"[EXP] Evaluate {type}, expect {typeof(T)}, and get value {t}");
            return t;
        }        

        public override string ToString()
        {
            return $"EXP with type {type}, value {value}";
        }   
    }
}