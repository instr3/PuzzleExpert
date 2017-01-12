using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public class Declaration
    {
        public class GeneratingStructure
        {
            public string Method;
            public string[] Variables;
            public GeneratingStructure(List<string> tokens)
            {
                tokens = StringProcessor.Remove(tokens, ",");
                Method = tokens[0];
                Variables = tokens.GetRange(1, tokens.Count - 1).ToArray();
            }
        }
        public GeneratingStructure[] Generator { get; set; }
        public Declaration(List<string> tokens)
        {
            List<List<string>> tokensList = StringProcessor.SplitNonEmpty(tokens, "ANY");
            Generator = new GeneratingStructure[tokensList.Count];
            for(int i=0;i<tokensList.Count;++i)
            {
                Generator[i]=new GeneratingStructure(tokensList[i]);
            }
        }
        public override string ToString()
        {
            string res = "[";
            foreach(GeneratingStructure g in Generator)
            {
                res += g.Method + ":" + string.Join(".", g.Variables) + ";";
            }
            return res + "]";
        }
    }
}
