using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlitherlinkExpert.SlitherlinkContext
{
    public enum CornerTagEnum
    {
        None=0,
        Different=1,
        Same=2
    }
    public class FactPool
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int[,] VLine, HLine;
        public int[,] Cell;
        public CornerTagEnum[,,,] CornerTag;
        public FactPool(SlitherlinkPuzzle puzzle)
        {
            X = puzzle.X;
            Y = puzzle.Y;
            VLine = new int[X + 2, Y + 3];
            HLine = new int[X + 3, Y + 2];
            CornerTag = new CornerTagEnum[X + 2, Y + 2, 2, 2];
            for (int i = 0; i <= X + 1; ++i)
                for (int j = 0; j <= Y + 2; ++j)
                    VLine[i, j] = 2;
            for (int i = 0; i <= X + 2; ++i)
                for (int j = 0; j <= Y + 1; ++j)
                    HLine[i, j] = 2;
            for (int i = 1; i <= X; ++i)
                for (int j = 1; j <= Y + 1; ++j)
                    VLine[i, j] = 0;
            for (int i = 1; i <= X + 1; ++i)
                for (int j = 1; j <= Y; ++j)
                    HLine[i, j] = 0;
            Cell = puzzle.Hint.Clone() as int[,];
        }
    }
}
