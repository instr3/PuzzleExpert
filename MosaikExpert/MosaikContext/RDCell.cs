using Interpreter.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MosaikExpert.MosaikContext
{
    class RDCell:RDElement
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public RDCell(int x, int y)
        {
            X = x;
            Y = y;
        }
        public override string ToString()
        {
            return string.Format("({0},{1})", X, Y);
        }
        public static bool operator ==(RDCell lhs, RDCell rhs)
        {
            return lhs.X == rhs.X && lhs.Y == rhs.Y;
        }
        public static bool operator !=(RDCell lhs, RDCell rhs)
        {
            return !(lhs == rhs);
        }
        public override bool Equals(object obj)
        {
            if (obj is RDCell)
                return this == (RDCell)obj;
            return false;
        }
        public override int GetHashCode()
        {
            return X * 10000 + Y;
        }
    }
}
