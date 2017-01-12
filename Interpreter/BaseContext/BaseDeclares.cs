using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.BaseContext
{
    public class BaseDeclares
    {
        public void Bool(string name, Dictionary<string, RDElement> dict, object factPool, Rule rule, Action<int> callBack, int callBackID)
        {
            dict[name] = new RDBool(false);
            callBack.Invoke(callBackID);
            dict[name] = new RDBool(true);
            callBack.Invoke(callBackID);
            dict.Remove(name);
        }
    }
}
