using Interpreter.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlitherlinkExpert.SlitherlinkContext
{
    class RDCorner:RDElement
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        /// <summary>
        /// 0:UP 1:DOWN
        /// </summary>
        public int DX { get; private set; }
        /// <summary>
        /// 0:LEFT 1:RIGHT
        /// </summary>
        public int DY { get; private set; }
        public RDCorner(int x,int y,int dx, int dy)
        {
            X = x;
            Y = y;
            DX = dx;
            DY = dy;
        }
        public override string ToString()
        {
            return (DY==0?"Left":"Right")+(DX==0?"Top":"Bottom")+"CornerOf" + string.Format("({0},{1})", X, Y);
        }
    }
}
