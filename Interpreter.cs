using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JC.MiniLisp_Interpreter.Grammar;

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
            // token is either IGrammar or char
            List<object> tokens = Scanner(code);            

            Parser(tokens);

            return "";
        }

        /// <summary>
        /// Scan the incomming code to tokens
        /// </summary>
        /// <returns>token is either IGrammar or string</returns>
        private List<object> Scanner(string code)
        {
            List<object> tokens = new List<object>();

            List<string> splitedCode = code
                .Replace("(", " ( ")
                .Replace(")", " ) ")
                .Split(' ', '\n', '\t', '\r')  // note: NTR is BADBADBAD!!
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList(); 

            // scan each string s into token (IGrammar form)
            foreach(string s in splitedCode)
            {
                // Console.WriteLine($"[SPLITED CODE]\"{s}\"");

                if(double.TryParse(s, out double value))
                {
                    // token is number
                    tokens.Add( new EXP(typeof(double), value) );
                }
                else if(s == "#t" || s == "#f")
                {
                    // token is bool
                    tokens.Add( new EXP(typeof(bool), s == "#t"));
                }
                else
                {
                    // fallback: add str
                    tokens.Add(s);                
                }
            }

            return tokens;
        }

        /// <summary>
        /// Top-down parser
        /// </summary>
        /// <param name="tokens"></param>
        public void Parser(List<object> tokens)
        {
            // stack
            Stack<object> stack = new Stack<object>();
            
            // one in one push the token into stack
            foreach(object token in tokens)
            {
                // Shift one token to stack
                stack.Push(token);    
                Console.WriteLine("[TOKEN]"+token);
                Stack<object> cachedStack = new Stack<object>();

                // Check if the stack is altered, 
                // if it IS altered, keep running.
                while(
                    PROGRAM.TryParse(stack) 
                ) 
                {                    
                }
            }
        }
    }
}