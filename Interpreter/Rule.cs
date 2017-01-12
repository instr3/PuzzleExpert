using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interpreter
{
    public class Rule
    {
        public Declaration Declaration { get; set; }
        public Formula Condition { get; set; }
        public Result[] Results { get; set; }

        public Rule(string plainText)
        {
            List<string> tokens = StringProcessor.Process(plainText);
            int for_id = tokens.IndexOf("FOR");
            int if_id = tokens.IndexOf("IF");
            int then_id = tokens.IndexOf("THEN");
            if(for_id==-1||if_id==-1||then_id==-1)
            {
                throw new FormatException("非结构化的规则");
            }
            Declaration = new Declaration(tokens.GetRange(for_id + 1, if_id - for_id - 1));
            //MessageBox.Show(Declaration.ToString());
            Condition =new Formula(tokens.GetRange(if_id + 1, then_id - if_id - 1));
            //MessageBox.Show(Condition.ToString());
            List<List<string>> tokenslist = StringProcessor.Split(tokens.GetRange(then_id + 1, tokens.Count - then_id - 1),
                "ALSO");
            Results = tokenslist.Select(s => new Result(s)).ToArray();
            //MessageBox.Show(string.Join(Environment.NewLine,Results.Select(s=>s.ToString())));
        }
        public override string ToString()
        {
            return string.Join(Environment.NewLine, new string[]
            {
                "Declarations (FOR Part):",
                Declaration.ToString(),
                "Conditions (IF Part):",
                Condition.ToString()+
                "Setters (THEN Part):",
                string.Join(Environment.NewLine,Results.Select(s=>s.ToString()))
            });
        }
    }
}
