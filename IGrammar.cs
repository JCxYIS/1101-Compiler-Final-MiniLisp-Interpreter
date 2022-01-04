using System.Collections.Generic;

namespace JC.MiniLisp_Interpreter
{
    public interface IGrammar
    {
        /// <summary>
        /// Attempt to evaluate the value
        /// </summary>
        /// <returns></returns>
        object Evaluate();
    }
}