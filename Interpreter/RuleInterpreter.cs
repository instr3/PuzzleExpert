using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public class RuleInterpreter
    {
        public Rule[] RuleSet { get; private set; }
        public int[] RuleIDs { get; private set; }
        public string[] RuleDescriptions { get; private set; }
        public RuleInterpreter(string plainText)
        {
            string[] rulesRaw = plainText.Split(new string[] { "[RULE " }, StringSplitOptions.RemoveEmptyEntries);
            RuleSet = new Rule[rulesRaw.Length];
            RuleIDs = new int[rulesRaw.Length];
            RuleDescriptions = new string[rulesRaw.Length];
            for (int i=0;i<rulesRaw.Length;++i)
            {
                int descSp = rulesRaw[i].IndexOf(':');
                int headEnd= rulesRaw[i].IndexOf(']');
                if (descSp != -1)
                {
                    RuleIDs[i] = int.Parse(rulesRaw[i].Substring(0, descSp));
                    RuleDescriptions[i] = rulesRaw[i].Substring(descSp + 1, headEnd - descSp - 1);
                }
                else
                {
                    RuleIDs[i] = int.Parse(rulesRaw[i].Substring(0, headEnd));
                    RuleDescriptions[i] = "";

                }
                RuleSet[i] = new Rule(rulesRaw[i].Substring(headEnd + 1));
            }
        }
        public override string ToString()
        {
            string res = "";
            for(int i=0;i<RuleSet.Length;++i)
            {
                res += "[STRUCTURED RULE #" + RuleIDs[i] + "]" + Environment.NewLine;
                res += RuleSet[i].ToString() + Environment.NewLine;
            }
            return res;
        }
    }
}
