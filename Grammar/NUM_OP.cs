
namespace JC.MiniLisp_Interpreter.Grammar
{
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