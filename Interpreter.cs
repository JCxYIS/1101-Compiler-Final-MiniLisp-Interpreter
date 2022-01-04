using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace JC.MiniLisp_Interpreter
{

    /// <summary>
    /// MiniLisp Interpreter
    /// </summary>
    public class Interpreter
    {
        string code = "";

        public Interpreter() { }
        public Interpreter(string lispCode)
        {
            LoadCode(lispCode);
        }

        /// <summary>
        /// Load code
        /// </summary>
        /// <param name="lispCode"></param>
        public void LoadCode(string lispCode)
        {
            this.code = lispCode;
        }

        /// <summary>
        /// Evaluate the code, output result
        /// </summary>
        /// <returns></returns>
        public string Evaluate()
        {
            // TODO evaluation
            // AAA
            List<IGrammar> tokens = Scanner();

            // Scanner
            


            return "";
        }

        private List<IGrammar> Scanner()
        {
            List<IGrammar> tokens = new List<IGrammar>();

            List<string> splitedCode = code
                .Replace("(", " ( ")
                .Replace(")", " ) ")
                .Split(' ', '\n', '\t', '\r')  // note: NTR is BADBADBAD!!
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList(); 

            foreach(string s in splitedCode)
            {
                System.Console.WriteLine($"[SPLITED CODE]\"{s}\"");
            }

            return tokens;
        }
    }
}