
namespace JC.MiniLisp_Interpreter.Grammar
{
    public class PROGRAM : IGrammar
    {
        private STMT stmt;

        public PROGRAM(STMT stmt)
        {
            this.stmt = stmt;
        }

        public object Evaluate()
        {
            return stmt.Evaluate();
        }
    }
}