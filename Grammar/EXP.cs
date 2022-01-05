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

        private bool IsTerminal =>
            type == typeof(bool) || // boolean
            type == typeof(double); // number
            // type == typeof(string);

        public EXP(Type typeofVal, object val)
        {
            type = typeofVal;
            value = val;
        }

        public object Evaluate()
        {
            if(IsTerminal)
                return value;
            else
                return ((IGrammar)value).Evaluate();
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
            if(/* top is VARIABLE ||*/ top is NUM_OP || top is LOGICAL_OP /*|| FUN-OP || FUN-CALL || IF-EXP*/)
            {
                top = stack.Pop();
                stack.Push(new EXP(top.GetType(), top));
                return true;
            }
            return false;
        }

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