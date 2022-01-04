
using System.Collections.Generic;

namespace JC.MiniLisp_Interpreter.Grammar
{
    /// <summary>
    /// NUM-OP ::= PLUS | MINUS | MULTIPLY | DIVIDE | MODULUS | GREATER | SMALLER | EQUAL
    /// PLUS ::= (+ EXP EXP+)
    /// MINUS ::= (- EXP EXP)
    /// MULTIPLY ::= (* EXP EXP+)
    /// DIVIDE ::= (/ EXP EXP)
    /// MODULUS ::= (mod EXP EXP)
    /// GREATER ::= (> EXP EXP)
    /// SMALLER ::= (< EXP EXP)
    /// EQUAL ::= (= EXP EXP+)
    /// /// </summary>
    public class NUM_OP : IGrammar
    {
        public char op;
        public EXP l;
        public EXP r;

        public NUM_OP(char opChar, EXP lElement, EXP rElement)
        {
            op = opChar;
            l = lElement;
            r = rElement;
        }


        public Stack<object> TryParse(Stack<object> stack)
        {
            throw new System.NotImplementedException();
        }


        public object Evaluate()
        {
            switch(op)
            {
                case '+': return l.Evaluate<double>() + r.Evaluate<double>();
                case '-': return l.Evaluate<double>() - r.Evaluate<double>();
                case '*': return l.Evaluate<double>() * r.Evaluate<double>();
                case '/': return l.Evaluate<double>() / r.Evaluate<double>();
                case '%': return l.Evaluate<double>() % r.Evaluate<double>();
                case '>': return l.Evaluate<double>() > r.Evaluate<double>();
                case '<': return l.Evaluate<double>() < r.Evaluate<double>();
                case '=': return l.Evaluate<double>() == r.Evaluate<double>();
            }

            throw new System.Exception("Num op messed up");
        }        
    }
}