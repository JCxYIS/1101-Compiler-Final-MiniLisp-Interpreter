using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JC.MiniLisp_Interpreter.Grammar;
using JC.MiniLisp_Interpreter.Utility;

namespace JC.MiniLisp_Interpreter
{

    /// <summary>
    /// MiniLisp Interpreter
    /// </summary>
    public class Interpreter
    {
        public static Interpreter Instance;     

        /// <summary>
        /// Code
        /// </summary>   
        private string code = "";

        /// <summary>
        /// Holder of variables defined in this STMT / PROGRAM
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <typeparam name="EXP"></typeparam>
        /// <returns></returns>
        private Dictionary<string, EXP> variables = new Dictionary<string, EXP>();

        /// <summary>
        /// This value becomes non-null when a func declaration is parsed, and currently parsing its scope.
        /// Use Stack because there might be nested fun. NASTY!
        /// </summary>
        private List<FUN_EXP> funConstructorStack = new List<FUN_EXP>();

        /// <summary>
        /// Current fun constructor. 
        /// null = at global scope
        /// </summary>
        /// <returns></returns>
        private FUN_EXP funConstructor => funConstructorStack.Count == 0 ? null : funConstructorStack[funConstructorStack.Count()-1];

        /* -------------------------------------------------------------------------- */

        public Interpreter() { }
        public Interpreter(string lispCode)
        {
            this.code = lispCode;
            Instance = this;
        }

        /// <summary>
        /// Evaluate the code, output result
        /// </summary>
        /// <returns></returns>
        public string Evaluate()
        {
            // token is either IGrammar or string
            List<object> tokens = Scanner(code);            

            PROGRAM program = Parser(tokens);

            return program.Evaluate().ToString();
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
        public PROGRAM Parser(List<object> tokens)
        {
            // parser stack
            Stack<object> stack = new Stack<object>();
            
            // one in one push the token into stack
            foreach(object token in tokens)
            {
                // Shift one token to stack
                // Console.WriteLine("[TOKEN]"+token);
                stack.Push(token); 

                // [Debug] print Stack
                stack.DebugLog();

                // Fun mode
                var funMode = FUN_EXP.CheckFunMode(stack);
                if(funMode is FUN_EXP)
                {
                    // start fun mode!
                    funConstructorStack.Add(funMode);
                    continue;
                }

                // fun has its own parser
                if(funConstructor?.FeedingFUN_IDs(stack) ?? false)
                {
                    // still fedding the FUN_ID
                    continue;
                }

                // flag of end of fun constructor
                bool endOfFunConstrutor = funConstructor?.ScanEndOfConstructor(stack) ?? false;

                // Check if the stack is altered, 
                // if it IS altered, keep running.
                while( NUM_OP.TryParse(stack)
                    || LOGICAL_OP.TryParse(stack)
                    || PRINT_STMT.TryParse(stack) 
                    || DEF_STMT.TryParse(stack)
                    || IF_EXP.TryParse(stack) 
                    || FUN_CALL.TryParse(stack)
                    || EXP.TryParse(stack)
                ) 
                {                    
                    if(stack.Count == 0)
                        throw new Exception("OMG! Stack is empty!? That's impossipa!!");
                }

                // end of fun
                if(endOfFunConstrutor)
                {
                    funConstructor.EndOfConstructor(stack);
                    funConstructorStack.RemoveAt(funConstructorStack.Count()-1);
                    EXP.TryParse(stack); // convert the FUN_CALL directly to EXP
                    Debug.Log($"[Fun Constructor] Ended {funConstructorStack.Count()+1}, Thank you.");
                }                
            }

            // finish parse, now try sum up to PROGRAM
            STMT.ParseAll(stack);
            return PROGRAM.ParseAll(stack);
        }


        /// <summary>
        /// Check if a variable exist
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ContainsVariable(string id)
        {
            // find in local var
            for(int i = funConstructorStack.Count()-1; i >= 0; i--)
            {
                var localVar = funConstructorStack[i].HasLocalVar(id);
                if(localVar != null)
                    return true;
            }
            
            // get global var
            return variables.ContainsKey(id);
        }

        /// <summary>
        /// Get a variable
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EXP GetVariable(string id)
        {            
            if(!ContainsVariable(id))
                throw new Exception($"Undefined variable \"{id}\".");

            // local var
            for(int i = funConstructorStack.Count()-1; i >= 0; i--)
            {
                var localVar = funConstructorStack[i].HasLocalVar(id);
                if(localVar != null)
                    return localVar;
            }
            return variables[id];
        }

        /// <summary>
        /// Set a variable
        /// throw error when the variable exist OR id is invalid
        /// </summary>
        /// <param name="id"></param>
        /// <param name="exp"></param>
        public void SetVariable(string id, EXP exp)
        {
            // setting local var is not here, should be at FUN constructor.
            id.CheckLegalId();            
            if(variables.ContainsKey(id))
                throw new Exception($"Variable \"{id}\" is already exist.");
            variables.Add(id, exp);
        }

    }
}