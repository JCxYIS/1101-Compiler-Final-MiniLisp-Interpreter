
using System;
using System.Collections.Generic;
using JC.MiniLisp_Interpreter.Utility;

namespace JC.MiniLisp_Interpreter.Grammar
{
    public class PRINT_STMT : IGrammar
    {
        private string cmd;
        private EXP r;

        public PRINT_STMT(string cmd, EXP exp)
        {
            r = exp;
            this.cmd = cmd;
        }

        /// <summary>
        /// Try parse the parser stack
        /// </summary>
        /// <param name="stack"></param>
        /// <returns>The stack is substitued</returns>
        public static bool TryParse(Stack<object> stack)
        {
            var match = stack.MatchFuncName("print-num", "print-bool");
            if(match is null)
                return false;

            stack.Push( new PRINT_STMT(match[0].ToString(), (EXP)match[1]) );

            return false;
        }

        public object Evaluate()
        {
            object result = r.Evaluate();
            string output = "";

            if(cmd == "print-num")
            {
                output = result.ToString();
            }
            else
            {
                output = (bool)result ? "#t" : "#f";
            }
            
            Debug.Print(output);
            return output + "\r\n";
        }
    }
}