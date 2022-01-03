using System;

namespace JC.MiniLisp_Interpreter.Grammar
{
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

        public T Evaluate<T>()
        {
            return (T)Evaluate();
        }
    }
}