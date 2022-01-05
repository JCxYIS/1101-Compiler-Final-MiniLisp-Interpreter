using System.Collections.Generic;
using System.Linq;
using JC.MiniLisp_Interpreter.Utility;

namespace JC.MiniLisp_Interpreter.Grammar
{
    /// <summary>
    /// NUM-OP ::= PLUS | MINUS | MULTIPLY | DIVIDE | MODULUS | GREATER | SMALLER | EQUAL
    ///     PLUS ::= (+ EXP EXP+)
    ///     MINUS ::= (- EXP EXP)
    ///     MULTIPLY ::= (* EXP EXP+)
    ///     DIVIDE ::= (/ EXP EXP)
    ///     MODULUS ::= (mod EXP EXP)
    ///     GREATER ::= (> EXP EXP)
    ///     SMALLER ::= (< EXP EXP)
    ///     EQUAL ::= (= EXP EXP+)
    /// </summary>
    public class NUM_OP : IGrammar
    {
        public string op;
        public List<EXP> elements;

        public NUM_OP(string op, params EXP[] elements)
        {
            this.op = op;
            this.elements = elements.ToList();

            //  + * = support more than 2 elements
            int len = elements.Length;
            if(op == "+" || op == "*" || op == "=")
            {
                if(len < 2)
                    throw new System.Exception($"\"{op}\" Need 2 or more arguments, but got {len}.");
            }
            else
            {
                if(len != 2)
                    throw new System.Exception($"\"{op}\" Need exact 2 arguments, but got {len}.");
            }

        }


        /// <summary>
        /// Try parse the parser stack
        /// </summary>
        /// <param name="stack"></param>
        /// <returns>The stack is substitued</returns>
        public static bool TryParse(Stack<object> stack)
        {
            var match = stack.MatchFuncName("+", "-", "*", "/", "mod", ">", "<", "=");
            if(match is null)
                return false;

            Debug.Log($"[NUM_OP] match op={match[0]} param={match.Count-1}");
            
            // make new 
            string funcName = match[0].ToString();
            List<EXP> exp = new List<EXP>();
            for(int i = 1; i < match.Count; i++)
            {
                if( !(match[i] is EXP) )
                    throw new System.Exception($"Syntax error, expect EXP but get \"{match[i]}\"({match[i].GetType()}).");
                EXP e = (EXP)match[i];
                exp.Add(e);
            }
            stack.Push( new NUM_OP(funcName, exp.ToArray()) );
            
            return true;            
        }


        private EXP l => elements[0];
        private EXP r => elements[1];
        public object Evaluate()
        {
            switch(op)
            {
                case "+": return PlusAll();
                case "*": return MultiAll();
                case "=": return EqualAll();
                case "-":   return l.Evaluate<double>() - r.Evaluate<double>();
                case "/":   return l.Evaluate<double>() / r.Evaluate<double>();
                case "mod": return l.Evaluate<double>() % r.Evaluate<double>();
                case ">":   return l.Evaluate<double>() > r.Evaluate<double>();
                case "<":   return l.Evaluate<double>() < r.Evaluate<double>();
            }
            throw new System.Exception("Num op messed up");
        }    

        private double PlusAll()
        {
            double result = 0;
            foreach(var e in elements)
            {
                result += e.Evaluate<double>();
            }
            return result;
        }    
        private double MultiAll()
        {
            double result = 1;
            foreach(var e in elements)
            {
                result *= e.Evaluate<double>();
            }
            return result;
        }    
        private bool EqualAll()
        {
            double value = l.Evaluate<double>();
            bool result = true;
            foreach(var e in elements)
            {
                result &= value == e.Evaluate<double>();
            }
            return result;
        }    

    }
}