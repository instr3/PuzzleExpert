using CommonData;
using SlitherlinkExpert.SlitherlinkContext;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SlitherlinkExpert
{
    public class SlitherlinkPuzzle
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public FactPool factPool;
        private SlitherlinkPuzzle(int x,int y)
        {
            X = x;
            Y = y;
        }
        public int[,] Hint { get; private set; }
        public static SlitherlinkPuzzle FromXML(StreamReader inputStream)
        {
            throw new NotSupportedException();
        }

        public static SlitherlinkPuzzle FromTXT(StreamReader inputStream)
        {
            SlitherlinkPuzzle puzzle;
            
            {
                string[] dim = inputStream.ReadLine().Split(' ');

                puzzle = new SlitherlinkPuzzle(int.Parse(dim[0]), int.Parse(dim[1]));
                
                puzzle.Hint = new int[puzzle.X + 2, puzzle.Y + 2];
                for (int i = 0; i <= puzzle.X + 1; ++i)
                    for (int j = 0; j <= puzzle.Y + 1; ++j)
                        puzzle.Hint[i, j] = -1;
                for (int i = 1; i <= puzzle.X; ++i)
                {
                    string[] source = inputStream.ReadLine().Split(' ');
                    for (int j = 1; j <= puzzle.Y; ++j)
                    {
                        if (source[j - 1][0] != '-')
                            puzzle.Hint[i, j] = int.Parse(source[j - 1]);
                    }
                }
                puzzle.factPool = new FactPool(puzzle);
                //xr.ReadToFollowing("solution");
                //MessageBox.Show(xr.ReadElementString());
            }
            return puzzle;
        }
        /// <summary>
        /// Draw a line
        /// </summary>
        /// <param name="g"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="align"></param>
        /// <param name="pen"></param>
        /// <param name="direction">H is false(-),V is true(|)</param>
        /// <param name="result"></param>
        void drawLine(Graphics g, int x, int y, int align, Pen pen, bool direction, int result)
        {
            if (result == 0) return;
            int delta = align / 2;
            if (result == 1)
            {
                Point start = new Point(x - delta, y - delta);
                Point end = direction ? new Point(x - delta, y + delta) : new Point(x + delta, y - delta);
                g.DrawLine(pen, start, end);
            }
            else if (result == 2)
            {
                Point center = direction ? new Point(x - delta, y) : new Point(x, y - delta);
                g.DrawString("×", Common.XFONT, Brushes.Black, center, Common.STRFMT_CENTER);
            }
            else throw new Exception("状态有误");
        }
        public void DrawArc(Graphics g,int x,int y, int align, int dx,int dy,CornerTagEnum result)
        {
            if (result == CornerTagEnum.None) return;
            float startAngle = 0;
            if (dx == 0 && dy == 0) startAngle = 0;
            if (dx == 0 && dy == 1) startAngle = 90;
            if (dx == 1 && dy == 1) startAngle = 180;
            if (dx == 1 && dy == 0) startAngle = 270;
            g.DrawArc(new Pen(Color.Red), 
                new RectangleF(x - align / 2 - align / 3f, y - align / 2 - align / 3f, 2 * align / 3f, 2 * align / 3f), startAngle, 90);
            if(result==CornerTagEnum.Same)
                g.DrawArc(new Pen(Color.Red),
                new RectangleF(x - align / 2 - align / 4f, y - align / 2 - align / 4f, 2 * align / 4f, 2 * align / 4f), startAngle, 90);
        }
        public void Draw(Graphics g)
        {
            int DrawAlign = 30;
            int DrawBaseWidth = 0;
            int DrawBaseHeight = 0;
            int FontHeightAdjust = 2;
            int LineWidth = 4;
            int PointWidth = 2;
            g.Clear(Color.White);
            for (int i = 1; i <= X+1; ++i)
            {
                for (int j = 1; j <= Y+1; ++j)
                {
                    int posW = j * DrawAlign + DrawBaseWidth;
                    int posH = i * DrawAlign + DrawBaseHeight;
                    if (i <= X && j <= Y)
                    {
                        if(Hint[i,j]>=0)
                            g.DrawString(Hint[i, j].ToString(), Common.CELLFONT, Brushes.Black, new Point(posW, posH+ FontHeightAdjust), Common.STRFMT_CENTER);
                        for(int dx=0;dx<=1;++dx)
                        {
                            for(int dy=0;dy<=1;++dy)
                            {
                                DrawArc(g, posW + dy * DrawAlign, posH + dx * DrawAlign, Common.DEFAULT_ALIGN, dx, dy, factPool.CornerTag[i, j, dx, dy]);
                            }
                        }
                    }
                    if (i <= X + 1 && j <= Y)
                    {
                        drawLine(g, posW, posH, Common.DEFAULT_ALIGN, new Pen(Color.Black, LineWidth), false,factPool.HLine[i,j]);
                    }
                    if (i <= X && j <= Y + 1)
                    {
                        drawLine(g, posW, posH, Common.DEFAULT_ALIGN, new Pen(Color.Black, LineWidth), true, factPool.VLine[i, j]);
                    }
                    int delta = DrawAlign / 2;
                    g.FillRectangle(Brushes.Black, new Rectangle(posW - delta - PointWidth, posH - delta - PointWidth, PointWidth * 2, PointWidth * 2));
                }
            }
        }
    }
}
