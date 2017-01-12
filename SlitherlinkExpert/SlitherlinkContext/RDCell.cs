using Interpreter.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlitherlinkExpert.SlitherlinkContext
{
    class RDCell:RDElement
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public RDCell(int x,int y)
        {
            X = x;
            Y = y;
        }
        public override string ToString()
        {
            return string.Format("({0},{1})", X, Y);
        }
    }
}
