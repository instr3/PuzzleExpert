using Interpreter.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public class Formula
    {
        public string OperationName { get; private set; }
        public Formula[] Parameters { get; private set; }
        public Formula(List<string> tokens)
        {
            if(tokens.Count==0)
            {
                throw new Exception("格式错误!");
            }
            while(true)
            {
                KeyValuePair<int,string> kv= StringProcessor.ScanOperators(tokens);
                int opAt = kv.Key;
                if (opAt == -1)
                {
                    if (tokens[0] == "(" && tokens[tokens.Count - 1] == ")")
                    {
                        tokens = tokens.GetRange(1, tokens.Count - 2);
                        continue;
                    }
                    else if (tokens[0] != "(")// 以函数或元素开头
                    {
                        if(tokens.Count==1)// 元素
                        {
                            Parameters = null;
                            OperationName = tokens[0];
                            return;
                        }
                        else // 函数
                        {
                            OperationName = tokens[0];
                            tokens = tokens.GetRange(1, tokens.Count - 1);
                            if (tokens[0] == "(" && tokens[tokens.Count - 1] == ")")// 仅去掉最多一层括号
                            {
                                tokens = tokens.GetRange(1, tokens.Count - 2);
                            }
                            // 按逗号分隔参数表
                            List<List<string>> paraRaw = StringProcessor.SplitInBrackets(tokens, ",");
                            Parameters = paraRaw.Select(r => new Formula(r)).ToArray();
                            return;
                        }
                    }
                    else throw new Exception();
                }
                else
                {
                    OperationName = kv.Value;
                    if (opAt != 0)
                    {
                        Parameters = new Formula[] {
                            new Formula(tokens.GetRange(0,opAt)),
                            new Formula(tokens.GetRange(opAt+1,tokens.Count-opAt-1))
                        };
                    }
                    else
                    {
                        Parameters = new Formula[] {
                            new Formula(tokens.GetRange(opAt+1,tokens.Count-opAt-1))
                        };
                    }
                    return;
                }
            }
        }
        public string ToString(int depth)
        {
            string res = new string(' ', depth);
            res += "[" + OperationName + "]" + Environment.NewLine;
            if(Parameters!=null)
            {
                foreach(Formula f in Parameters)
                {
                    res += f.ToString(depth + 1);
                }
            }
            return res;
        }
        public override string ToString()
        {
            return ToString(0);
        }
    }
}
