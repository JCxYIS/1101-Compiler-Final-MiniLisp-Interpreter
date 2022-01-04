using System.Collections.Generic;

namespace JC.MiniLisp_Interpreter
{
    public interface IGrammar
    {
        /// <summary>
        /// Try parse the parser stack, return the parsed stack
        /// (return the original stack if we can't find anything to parse)
        /// </summary>
        /// <param name="stack"></param>
        /// <returns></returns>
        Stack<object> TryParse(Stack<object> stack);

        /// <summary>
        /// Attempt to evaluate the value
        /// </summary>
        /// <returns></returns>
        object Evaluate();
    }
}