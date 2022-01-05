using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace JC.MiniLisp_Interpreter.Utility
{
    public static class GrammarUtility
    {
        /// <summary>
        /// is last element of stack String AND end with ")" ?
        /// </summary>
        /// <param name="stack"></param>
        /// <returns></returns>
        public static bool IsStringAndEndWith(this Stack<object> stack, string IsStackEndWithThisString)
        {
            return stack.Peek() is string && (string)stack.Peek() == IsStackEndWithThisString;
        }

        /// <summary>
        /// If (funcName ...) match, pop them all and return the elements:  funcName and ...
        /// </summary>
        /// <returns></returns>
        public static List<object> MatchFuncName(this Stack<object> stack, params string[] funcNames)
        {
            if(!stack.IsStringAndEndWith(")"))
                return null;

            // cached stack
            // NOTE THIS IS REVERSED LIST!!! [0] = ")"
            List<object> stackList = new List<object>(stack); 

            // find "("
            int LParenthesisPos = stackList.FindIndex(s => s.ToString() == "(");
            if(LParenthesisPos < 0)
                throw new IndexOutOfRangeException("Cannot find \"(\".");

            // match funcName
            string myFuncName = stackList[LParenthesisPos - 1].ToString();
            if(!funcNames.Any(funcName => funcName == myFuncName))
            {
                // Debug.Log($"[GrammarUtil] Function {stackList[LParenthesisPos - 1].ToString()} doesn't match expected funcName {funcName}");
                return null;
            }

            // Match matchtype
            List<object> matches = new List<object>();
            for(int i = LParenthesisPos-1; i > 0; i--)
            {
                matches.Add(stackList[i]);
                Debug.Log("Added "+stackList[i]);
            }

            // Pop the (...) of stack
            while(true)
            {
                var s = stack.Pop();
                if(s.ToString() == "(")
                {
                    break;
                }
            }

            return matches;
        }

        /// <summary>
        /// Check if an id is valid 
        /// ID ::= letter (letter | digit | ‘-’)*
        /// </summary>
        /// <param name="id"></param>
        public static void CheckLegalId(this string id)
        {
            Regex regex = new Regex("[a-z]([a-z|0-9|-])*");
            if(!regex.IsMatch(id))
            {
                throw new Exception($"id \"{id}\" is invalid!");
            }        
        }
    }
}