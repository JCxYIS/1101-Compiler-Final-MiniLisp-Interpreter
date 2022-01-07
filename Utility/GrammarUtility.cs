using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JC.MiniLisp_Interpreter.Grammar;

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
                Debug.Log("[Util.Match (Str)] Added "+stackList[i]);
            }

            // Pop the (... ...) of stack
            stack.PopN(matches.Count()+2);

            return matches;
        }

        /// <summary>
        /// If (typeof(type) ...) match, pop them all and return the elements:  type(element of that type) and ...
        /// NOTICE: the type contains in EXP also counts!
        /// </summary>
        /// <returns></returns>
        public static List<object> MatchFuncType(this Stack<object> stack, Type type)
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

            // the result
            List<object> matches = new List<object>();

            // match funcName
            Type myFuncType = stackList[LParenthesisPos - 1].GetType();
            if(myFuncType == type) // EXP
            {
                matches.Add(stackList[LParenthesisPos - 1]);
            }
            else if(myFuncType == typeof(EXP) && ((EXP)stackList[LParenthesisPos - 1]).Type == type )
            {
                matches.Add( ((EXP)stackList[LParenthesisPos - 1]).Value );
            }
            else
            {
                return null;
            }

            // Match matchtype
            for(int i = LParenthesisPos-2; i > 0; i--)
            {
                matches.Add(stackList[i]);
                Debug.Log("[Util.Match (Type)] Added "+stackList[i]);
            }

            // Pop the (... ...) of stack
            stack.PopN(matches.Count + 2);

            return matches;
        }

        /// <summary>
        /// pop n objects
        /// </summary>
        /// <param name="stack"></param>
        /// <param name="n"></param>
        public static void PopN(this Stack<object> stack, int n)
        {
            for(int i = 0; i < n; i++)
            {
                stack.Pop();
            }
        }

        /// <summary>
        /// Print current Stack status in Debig.Log
        /// </summary>
        /// <param name="stack"></param>
        public static void DebugLog(this Stack<object> stack)
        {
            string log = $"[PARSER] Stack (LEN={stack.Count}) is ";
            foreach(var ele in stack)
            {
                log += "\n........"+ele;
            }
            Debug.Log(log);
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