using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.BaseContext
{
    public class RDBool:RDElement
    {
        public bool Data;
        public RDBool(bool data)
        {
            Data = data;
        }
        public RDBool Not()
        {
            return new RDBool(!Data);
        }
        public RDBool Or(RDBool rhs)
        {
            return new RDBool(Data || rhs.Data);
        }
        public RDBool And(RDBool rhs)
        {
            return new RDBool(Data && rhs.Data);
        }
        public override string ToString()
        {
            return Data ? "TRUE" : "FALSE";
        }
    }
}
