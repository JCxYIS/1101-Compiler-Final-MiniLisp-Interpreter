
namespace JC.MiniLisp_Interpreter.Grammar
{
    public class NUM_OP : IGrammar
    {
        public enum OP 
        { 
            PLUS, 
            MINUS, 
            MULTIPLY, 
            DIVIDE, 
            MODULUS, 
            GREATER, 
            SMALLER, 
            EQUAL, 
        }

        public OP op;
        public EXP l;
        public EXP r;

        public NUM_OP(OP operation, EXP lElement, EXP rElement)
        {
            // TODO
        }


        public object Evaluate()
        {
            switch(op)
            {
                case OP.PLUS: 
                    return l + r;
                case OP.MINUS: 
                    return l - r;
                case OP.MULTIPLY: 
                    return l * r;
                case OP.DIVIDE: 
                    return l / r;
                case OP.MODULUS: 
                    return l % r;
                case OP.GREATER: 
                    return l > r;
                case OP.SMALLER: 
                    return l < r;
                case OP.EQUAL: 
                    return l == r;
            }

            throw new System.Exception("Num op messed up");
        }
    }
}