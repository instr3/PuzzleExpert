using Interpreter.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlitherlinkExpert.SlitherlinkContext
{
    class SlitherlinkOperations:BaseOperations
    {
        public RDNumber NumberOf(RDCell cell,object factPool)
        {
            return new RDNumber(((FactPool)factPool).Cell[cell.X, cell.Y]);
        }
        public RDList AllEdgesOf(RDCell cell,object factPool)
        {
            return new RDList(new RDElement[] {
                new RDEdge(cell.X,cell.Y,false),
                new RDEdge(cell.X,cell.Y,true),
                new RDEdge(cell.X+1,cell.Y,false),
                new RDEdge(cell.X,cell.Y+1,true)
            });
        }
        public RDBool IsNear(RDEdge edge,RDCell cell,object factPool)
        {
            return new RDBool((edge.X == cell.X && edge.Y == cell.Y) ||
                (edge.Direction == true && edge.X == cell.X && edge.Y == cell.Y + 1) ||
                (edge.Direction == false && edge.X == cell.X + 1 && edge.Y == cell.Y));

        }
        public RDEdge CommonEdgeOf(RDCell lhs,RDCell rhs, object factPool)
        {
            RDCell a = (lhs.X + lhs.Y < rhs.X + rhs.Y) ? lhs : rhs;
            RDCell b = (lhs.X + lhs.Y < rhs.X + rhs.Y) ? rhs : lhs;
            if (a.X == b.X && a.Y + 1 == b.Y)
            {
                return new RDEdge(b.X, b.Y, true);
            }
            else if (a.X + 1 == b.X && a.Y == b.Y)
            {
                return new RDEdge(b.X, b.Y, false);
            }
            else throw new Exception("两个Cell没有公共边！");
        }
        public RDList OppositeEdgesOf(RDEdge edge,object factPool)
        {
            if (edge.Direction == false)
            {
              return new RDList(new RDElement[]
              {
                new RDEdge(edge.X+1,edge.Y,edge.Direction),
                new RDEdge(edge.X-1,edge.Y,edge.Direction)
              });
            }
            else
            {
                return new RDList(new RDElement[]
                {
                new RDEdge(edge.X,edge.Y+1,edge.Direction),
                new RDEdge(edge.X,edge.Y-1,edge.Direction)
                });
            }
        }
        public RDNumber ColorOf(RDEdge edge,object factPool)
        {
            FactPool pool = (FactPool)factPool;
            int[,] arr = edge.Direction ? pool.VLine : pool.HLine;
            return new RDNumber(arr[edge.X, edge.Y]);
        }
        public RDList ColorsOf(RDList edges, object factPool)
        {
            return new RDList(edges.Data.Select(
                e => ColorOf((RDEdge)e, factPool)).ToArray());
        }
        public RDList UncoloredSubsetOf(RDList edgelist,object factPool)
        {
            return new RDList(edgelist.Data.Where(
                e => ColorOf((RDEdge)e, factPool).Data == 0).ToArray());
        }
        public RDList BlackSubsetOf(RDList edgelist, object factPool)
        {
            return new RDList(edgelist.Data.Where(
                e => ColorOf((RDEdge)e, factPool).Data == 1).ToArray());
        }
        public RDList WhiteSubsetOf(RDList edgelist, object factPool)
        {
            return new RDList(edgelist.Data.Where(
                e => ColorOf((RDEdge)e, factPool).Data == 2).ToArray());
        }
        public RDList ExtendedEdgesOf(RDEdge edge, object factPool)
        {
            if (edge.Direction == false)
            {
                return new RDList(new RDElement[]
                {
                new RDEdge(edge.X,edge.Y+1,edge.Direction),
                new RDEdge(edge.X,edge.Y-1,edge.Direction)
                });
            }
            else
            {
                return new RDList(new RDElement[]
                {
                new RDEdge(edge.X+1,edge.Y,edge.Direction),
                new RDEdge(edge.X-1,edge.Y,edge.Direction)
                });
            }
        }
        private int DistanceOfEdgeAndCell(RDEdge edge,RDCell cell)
        {
            int x = edge.X * 2, y = edge.Y * 2;
            if (edge.Direction) y--; else x--;
            x -= cell.X * 2;
            y -= cell.Y * 2;
            return x * x + y * y;
        }
        public RDList MostAwayFrom(RDList edges, RDCell cell, object factPool)
        {
            int[] Dist = edges.Data.Select(e => DistanceOfEdgeAndCell((RDEdge)e, cell)).ToArray();
            int maxDist = Dist.Max();
            return new RDList(edges.Data.Where(e => DistanceOfEdgeAndCell((RDEdge)e, cell) == maxDist).ToArray());

        }
        public RDCorner OppositeCornerOf(RDCorner corner, object factPool)
        {
            return new RDCorner(corner.X + (corner.DX==1?1:-1), corner.Y + (corner.DY==1?1:-1), 1 - corner.DX, 1 - corner.DY);
        }
        public RDCorner DiagonalCornerOf(RDCorner corner, object factPool)
        {
            return new RDCorner(corner.X, corner.Y, 1 - corner.DX, 1 - corner.DY);
        }
        public RDList NearingCornerOf(RDCorner corner, object factPool)
        {
            return new RDList(new RDCorner[]
            {
                new RDCorner(corner.X,corner.Y,1-corner.DX,corner.DY),
                new RDCorner(corner.X,corner.Y,corner.DX,1-corner.DY)
            });
        }
        public RDList NearingEdgesOf(RDCorner corner,object factPool)
        {
            return new RDList(new RDElement[]
            {
                new RDEdge(corner.X,corner.Y+corner.DY,true),
                new RDEdge(corner.X+corner.DX,corner.Y,false)
            });
        }
        public RDCell CellOf(RDCorner corner,object factPool)
        {
            return new RDCell(corner.X, corner.Y);
        }
        public RDList SuccessiveEdgesOf(RDEdge lhs, object factPool)
        {
            int x = lhs.X, y = lhs.Y;
            if (lhs.Direction==false)//(-)
            {
                return new RDList(new RDElement[]
                {
                    new RDEdge(x,y,true),
                    new RDEdge(x-1,y,true),
                    new RDEdge(x,y+1,true),
                    new RDEdge(x-1,y+1,true),
                    new RDEdge(x,y-1,false),
                    new RDEdge(x,y+1,false)
                });
            }
            else//(|)
            {
                return new RDList(new RDElement[]
                {
                    new RDEdge(x,y,false),
                    new RDEdge(x,y-1,false),
                    new RDEdge(x+1,y,false),
                    new RDEdge(x+1,y-1,false),
                    new RDEdge(x-1,y,true),
                    new RDEdge(x+1,y,true)
                });
            }
        }
        public RDList CommonSuccessiveEdgesOf(RDEdge lhs, RDEdge rhs, object factPool)
        {
            RDList llist = SuccessiveEdgesOf(lhs, factPool);
            RDList rlist = SuccessiveEdgesOf(rhs, factPool);
            RDList result = new RDList();
            foreach (RDElement e1 in llist.Data)
            {
                foreach(RDElement e2 in rlist.Data)
                {
                    if ((e1 as RDEdge).X==(e2 as RDEdge).X&& (e1 as RDEdge).Y == (e2 as RDEdge).Y && (e1 as RDEdge).Direction == (e2 as RDEdge).Direction)
                    {
                        result.Data.Add(e1);
                        break;
                    }
                }
            }
            return result; 
        }
        public RDBool IsEdgeTouch(RDEdge lhs,RDEdge rhs,object factPool)
        {
            if (lhs.X == rhs.X && lhs.Y == rhs.Y) return new RDBool(true);
            if (lhs.X + (lhs.Direction ? 1 : 0) == rhs.X && lhs.Y + (lhs.Direction ? 0 : 1) == rhs.Y) return new RDBool(true);
            if (lhs.X + (lhs.Direction ? 1 : 0) == rhs.X + (rhs.Direction ? 1 : 0) && lhs.Y + (lhs.Direction ? 0 : 1) == rhs.Y + (rhs.Direction ? 0 : 1)) return new RDBool(true);
            if (lhs.X == rhs.X + (rhs.Direction ? 1 : 0) && lhs.Y == rhs.Y + (rhs.Direction ? 0 : 1)) return new RDBool(true);
            return new RDBool(false);
        }
    }
}
