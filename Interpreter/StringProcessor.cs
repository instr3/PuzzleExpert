using Interpreter.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interpreter
{
    class StringProcessor
    {
        public static List<string> Process(string inputString)
        {
            List<string> tokens = new List<string>();
            bool isLetter = false;
            StringBuilder sb = new StringBuilder();
            for(int i=0;i<inputString.Length;++i)
            {
                char c = inputString[i];
                if(!char.IsLetterOrDigit(c)|| char.IsLetterOrDigit(c)!=isLetter|| char.IsWhiteSpace(c))
                {
                    if(sb.ToString()!="")
                    {
                        tokens.Add(sb.ToString());
                        sb.Clear();
                    }
                    isLetter = char.IsLetterOrDigit(c);
                }
                if(!char.IsWhiteSpace(c))
                {
                    sb.Append(c);
                }
            }
            if (sb.ToString() != "")
            {
                tokens.Add(sb.ToString());
                sb.Clear();
            }
            for (int i = 0; i < tokens.Count - 1; ++i)
            {
                if ((tokens[i] == "IS" && tokens[i + 1] == "ANY") ||
                    (tokens[i] == "ARE" && tokens[i + 1] == "ALL")||
                    (tokens[i] == "<" && tokens[i + 1] == "=") ||
                    (tokens[i] == ">" && tokens[i + 1] == "=") ||
                    (tokens[i] == "!" && tokens[i + 1] == "="))
                {
                    tokens[i] = tokens[i] + tokens[i + 1];
                    tokens.RemoveAt(i + 1);
                }
            }
            return tokens;
        }
        public static List<List<string>> Split(List<string> data,string delimiter)
        {
            List<List<string>> result = new List<List<string>>();
            List<string> current = new List<string>();
            foreach(string str in data)
            {
                if (str == delimiter)
                {
                    result.Add(current);
                    current = new List<string>();
                }
                else current.Add(str);
            }
            result.Add(current);
            return result;
        }
        public static List<string> Remove(List<string> data,string word)
        {
            List<string> result = new List<string>();
            foreach(string str in data)
            {
                if (str != word) result.Add(str);
            }
            return result;
        }
        public static List<List<string>> SplitNonEmpty(List<string> data, string delimiter)
        {
            List<List<string>> result = new List<List<string>>();
            List<string> current = new List<string>();
            foreach (string str in data)
            {
                if (str == delimiter)
                {
                    if(current.Count>0)
                        result.Add(current);
                    current = new List<string>();
                }
                else current.Add(str);
            }
            if (current.Count > 0)
                result.Add(current);
            return result;
        }
        public static List<List<string>> SplitInBrackets(List<string> data, string delimiter)
        {
            int depth = 0;
            List<List<string>> result = new List<List<string>>();
            List<string> current = new List<string>();
            foreach (string str in data)
            {
                if (str == "(") ++depth;
                if (depth < 0)
                    throw new Exception("括号不匹配");
                if(depth==0&& str == delimiter)
                {
                    result.Add(current);
                    current = new List<string>();
                }
                else current.Add(str);
                if (str == ")") --depth;
                if (depth < 0)
                    throw new Exception("括号不匹配");
            }
            result.Add(current);
            if (depth != 0)
                throw new Exception("括号不匹配");
            return result;
        }
        public static void Print(List<string> tokens)
        {
            string res = "";
            for(int i=0;i<tokens.Count;++i)
            {
                res += i.ToString() + " : " + tokens[i] + Environment.NewLine;
            }
            MessageBox.Show(res);
        }
        public static KeyValuePair<int, string> ScanOperators(List<string> tokens)
        {
            int depth = 0;
            BaseOperations.Operator MinOperator = null;
            int minAt = -1;
            for(int i=0;i<tokens.Count;++i)
            {
                if (tokens[i] == "(") ++depth;
                // 是否在最外层括号外
                if (depth==0)
                {
                    foreach(BaseOperations.Operator op in BaseOperations.OperatorList)
                    {
                        // 这个token是否是运算符
                        if(op.name==tokens[i])
                        {
                            // 是否存在优先级更小的运算符
                            if(MinOperator==null||MinOperator.priority>op.priority)
                            {
                                MinOperator = op;
                                minAt = i;
                            }
                        }
                    }
                }
                if (tokens[i] == ")") --depth;
                if (depth < 0)
                    throw new Exception("括号不匹配");
            }
            if(depth!=0)
                throw new Exception("括号不匹配");
            return new KeyValuePair<int, string>(minAt, MinOperator?.function);
        }
    }
}
