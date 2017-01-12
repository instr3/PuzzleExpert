using Interpreter;
using Interpreter.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterTest.TestContext
{
    class TestDeclares:BaseDeclares
    {
        public void Number(string name, Dictionary<string, RDElement> dict, object factPool, Rule rule, Action<int> callBack, int callBackID)
        {
            for(int i=0;i<=1000;++i)
            {
                dict[name] = new RDNumber(i);
                callBack(callBackID);
            }
            dict.Remove(name);
        }
        public void SquareNumber(string name, Dictionary<string, RDElement> dict, object factPool, Rule rule, Action<int> callBack, int callBackID)
        {
            FactPool pool = (FactPool)factPool; 
            for (int i = 0; i <= 1000; ++i)
            {
                if(pool.isSquare[i])
                {
                    dict[name] = new RDNumber(i);
                    callBack(callBackID);
                }
            }
            dict.Remove(name);
        }
    }
}
