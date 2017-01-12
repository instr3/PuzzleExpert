using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MosaikExpert.MosaikContext
{
    public class FactPool
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int[,] Result;
        public int[,] Cell;
        public FactPool(MosaikPuzzle puzzle)
        {
            X = puzzle.X;
            Y = puzzle.Y;
            Result = new int[X + 2, Y + 2];
            for (int i = 0; i <= X + 1; ++i)
                for (int j = 0; j <= Y + 1; ++j)
                    Result[i, j] = 2;
            for (int i = 1; i <= X; ++i)
                for (int j = 1; j <= Y; ++j)
                    Result[i, j] = 0;
            Cell = puzzle.Hint.Clone() as int[,];
        }
    }
}
