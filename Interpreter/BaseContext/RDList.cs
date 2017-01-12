using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.BaseContext
{
    public class RDList:RDElement
    {
        public List<RDElement> Data { get; private set; }
        public RDList()
        {
            Data = new List<RDElement>();
        }
        public RDList(RDElement[] input)
        {
            Data = input.ToList();
        }
        public static explicit operator RDNumber(RDList obj)
        {
            if (obj.Data.Count != 1)
            {
                throw new ArgumentException("不能进行转换");
            }
            else
                return obj.Data[0] as RDNumber;
        }
        public override string ToString()
        {
            return "[" + string.Join(",", Data) + "]";
        }
    }
}
