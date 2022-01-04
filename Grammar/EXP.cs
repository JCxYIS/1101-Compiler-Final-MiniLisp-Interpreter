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

        public static Stack<object> TryParse(Stack<object> stack)
        {
            throw new NotImplementedException();
        }

        public T Evaluate<T>()
        {
            return (T)Evaluate();
        }        

        
    }
}