using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.BaseContext
{
    public class RDNumber:RDElement
    {
        public int Data { get; private set; }
        public RDNumber(int input)
        {
            Data = input;
        }
        public override string ToString()
        {
            return Data.ToString();
        }
    }
}
