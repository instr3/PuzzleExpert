using Interpreter.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlitherlinkExpert.SlitherlinkContext
{
    class RDEdge:RDElement
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        /// <summary>
        /// False for horizontal(-), True for vertical(|)
        /// </summary>
        public bool Direction { get; private set; }
        /// <summary>
        /// Create a RDEdge
        /// </summary>
        /// <param name="x">x pos</param>
        /// <param name="y">y pos</param>
        /// <param name="direction">False for horizontal(-), True for vertical(|)</param>
        public RDEdge(int x, int y,bool direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }
        public override string ToString()
        {
            return string.Format(Direction? "LeftEdgeOf({0},{1})":"UpEdgeOf({0},{1})", X, Y);
        }

        public static bool operator ==(RDEdge lhs, RDEdge rhs)
        {
            return lhs.X == rhs.X && lhs.Y == rhs.Y && lhs.Direction == rhs.Direction;
        }
        public static bool operator !=(RDEdge lhs, RDEdge rhs)
        {
            return !(lhs == rhs);
        }
        public override bool Equals(object obj)
        {
            if (obj is RDEdge)
                return this == (RDEdge)obj;
            else return false;
        }
    }
}
