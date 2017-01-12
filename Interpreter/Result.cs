using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public class Result
    {
        public string SetterName { get; private set; }
        public Formula[] Parameters { get; private set; }
        public Formula AssignFormula { get; private set; }
        public Result(List<string> tokens)
        {
            KeyValuePair<int,string> kv = StringProcessor.ScanOperators(tokens);
            string equalFunction = kv.Value;
            if (equalFunction != "Equal" && equalFunction != "AreAll")
                throw new FormatException("THEN语句不合法，不是赋值语句");
            int opAt = kv.Key;
            AssignFormula = new Formula(tokens.GetRange(opAt + 1, tokens.Count - opAt - 1));
            tokens = tokens.GetRange(0, opAt);
            kv = StringProcessor.ScanOperators(tokens);
            opAt = kv.Key;
            if (opAt!=-1)
            {
                throw new FormatException("THEN语句不合法，Equal左半部分不可赋值");
            }
            Formula formula = new Formula(tokens);// Temp
            SetterName = (equalFunction == "AreAll" ? "All" : "") + formula.OperationName;
            Parameters = formula.Parameters;
        }
        public override string ToString()
        {
            string res = "[SETTER:" + SetterName + "]" + Environment.NewLine +
                string.Join("," + Environment.NewLine, Parameters.Select(p => p.ToString(1)));
            res += ":=" + Environment.NewLine + AssignFormula.ToString(1);
            return res;
        }
    }
}
