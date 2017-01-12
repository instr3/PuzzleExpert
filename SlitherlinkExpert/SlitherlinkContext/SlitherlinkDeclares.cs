using Interpreter;
using Interpreter.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlitherlinkExpert.SlitherlinkContext
{
    class SlitherlinkDeclares:BaseDeclares
    {
        private static readonly int[] dirX = new int[] { 0, 1, 0, -1 };
        private static readonly int[] dirY = new int[] { 1, 0, -1, 0 };
        private bool IsUnfinishedNumberCell(int x,int y, object factPool)
        {
            FactPool pool = (FactPool)factPool;
            if (y > pool.Y || x > pool.X) return false;
            return pool.Cell[x, y] != -1 && (pool.HLine[x, y] == 0 || pool.VLine[x, y] == 0 ||
                            pool.HLine[x + 1, y] == 0 || pool.VLine[x, y + 1] == 0);
        }
        private bool IsUnfinishedCorner(int x,int y,int dx,int dy,object factPool)
        {
            FactPool pool = (FactPool)factPool;
            return pool.HLine[x + dx, y] == 0 || pool.VLine[x, y + dy] == 0;
        }
        public void NumberCell(string name, Dictionary<string, RDElement> dict, object factPool, Rule rule, Action<int> callBack, int callBackID)
        {
            FactPool pool = (FactPool)factPool;
            for(int x=1;x<=pool.X;++x)
            {
                for(int y=1;y<=pool.Y;++y)
                {
                    if(IsUnfinishedNumberCell(x,y,factPool))
                    {
                        dict[name] = new RDCell(x, y);
                        callBack.Invoke(callBackID);
                    }
                }
            }
            dict.Remove(name);
        }
        public void NeighborNumberPair(string name1,string name2, Dictionary<string, RDElement> dict, object factPool, Rule rule, Action<int> callBack, int callBackID)
        {
            FactPool pool = (FactPool)factPool;
            for (int x = 1; x <= pool.X; ++x)
            {
                for (int y = 1; y <= pool.Y; ++y)
                {
                    if (IsUnfinishedNumberCell(x, y, factPool) && IsUnfinishedNumberCell(x, y + 1, factPool))
                    {
                        dict[name1] = new RDCell(x, y);
                        dict[name2] = new RDCell(x, y + 1);
                        //Console.WriteLine(x + "," + y + ",y," + pool.Cell[x, y] + pool.Cell[x, y + 1]);
                        callBack.Invoke(callBackID);
                        dict[name1] = new RDCell(x, y + 1);
                        dict[name2] = new RDCell(x, y);
                        //Console.WriteLine(x + "," + y + ",y," + pool.Cell[x, y] + pool.Cell[x, y + 1]);
                        callBack.Invoke(callBackID);
                    }
                    if (IsUnfinishedNumberCell(x, y, factPool) && IsUnfinishedNumberCell(x + 1, y, factPool))
                    {
                        dict[name1] = new RDCell(x, y);
                        dict[name2] = new RDCell(x + 1, y);
                        //Console.WriteLine(x + "," + y + ",x," + pool.Cell[x, y] + pool.Cell[x + 1, y]);
                        callBack.Invoke(callBackID);
                        dict[name1] = new RDCell(x + 1, y);
                        dict[name2] = new RDCell(x, y);
                        //Console.WriteLine(x + "," + y + ",x," + pool.Cell[x, y] + pool.Cell[x + 1, y]);
                        callBack.Invoke(callBackID);
                    }
                }
            }
            dict.Remove(name1);
            dict.Remove(name2);
        }
        public void DiagonalNumberPair(string name1, string name2, Dictionary<string, RDElement> dict, object factPool, Rule rule, Action<int> callBack, int callBackID)
        {
            FactPool pool = (FactPool)factPool;
            for (int x = 1; x <= pool.X; ++x)
            {
                for (int y = 1; y <= pool.Y; ++y)
                {
                    if (IsUnfinishedNumberCell(x, y, factPool) && IsUnfinishedNumberCell(x + 1, y + 1, factPool))
                    {
                        dict[name1] = new RDCell(x, y);
                        dict[name2] = new RDCell(x + 1, y + 1);
                        callBack.Invoke(callBackID);
                    }
                    if (IsUnfinishedNumberCell(x, y + 1, factPool) && IsUnfinishedNumberCell(x + 1, y, factPool))
                    {
                        dict[name1] = new RDCell(x, y + 1);
                        dict[name2] = new RDCell(x + 1, y);
                        callBack.Invoke(callBackID);
                    }
                }
            }
            dict.Remove(name1);
            dict.Remove(name2);
        }
        public void NumberCorner(string name, Dictionary<string, RDElement> dict, object factPool, Rule rule, Action<int> callBack, int callBackID)
        {
            FactPool pool = (FactPool)factPool;
            for (int x = 1; x <= pool.X; ++x)
            {
                for (int y = 1; y <= pool.Y; ++y)
                {
                    if (IsUnfinishedNumberCell(x, y, factPool))
                    {
                        for(int dx=0;dx<=1;++dx)
                        {
                            for(int dy=0;dy<=1;++dy)
                            {
                                if(IsUnfinishedCorner(x,y,dx,dy,factPool))
                                {
                                    dict[name] = new RDCorner(x, y, dx, dy);
                                    callBack.Invoke(callBackID);
                                }
                            }
                        }
                    }
                }
            }
            dict.Remove(name);
        }
        public void OppositeCornerPair(string name1,string name2, Dictionary<string, RDElement> dict, object factPool, Rule rule, Action<int> callBack, int callBackID)
        {
            FactPool pool = (FactPool)factPool;
            for (int x = 0; x <= pool.X; ++x)
            {
                for (int y = 0; y <= pool.Y; ++y)
                {
                    if (IsUnfinishedCorner(x+1,y+1,0,0,factPool)||IsUnfinishedCorner(x,y,1,1, factPool))
                    {
                        dict[name1] = new RDCorner(x+1, y+1, 0, 0);
                        dict[name2] = new RDCorner(x, y, 1, 1);
                        callBack.Invoke(callBackID);
                        dict[name2] = new RDCorner(x + 1, y + 1, 0, 0);
                        dict[name1] = new RDCorner(x, y, 1, 1);
                        callBack.Invoke(callBackID);
                    }
                    if (IsUnfinishedCorner(x+1, y, 0, 1, factPool) || IsUnfinishedCorner(x, y+1, 1, 0, factPool))
                    {
                        dict[name1] = new RDCorner(x+1, y, 0, 1);
                        dict[name2] = new RDCorner(x, y+1, 1, 0);
                        callBack.Invoke(callBackID);
                        dict[name2] = new RDCorner(x + 1, y, 0, 1);
                        dict[name1] = new RDCorner(x, y + 1, 1, 0);
                        callBack.Invoke(callBackID);
                    }
                }
            }
            dict.Remove(name1);
            dict.Remove(name2);
        }
        private RDEdge GetEdgeBetweenVertex(int x1,int y1,int x2,int y2)
        {
            if (x1 == x2)//H
            {
                return new RDEdge(x1, Math.Min(y1, y2), false);
            }
            else if (y1 == y2)//V
            {
                return new RDEdge(Math.Min(x1, x2), y1, true);
            }
            else throw new Exception();
        }
        private RDEdge GetEdgeBetweenCell(int x1, int y1, int x2, int y2)
        {
            if (x1 == x2)//V
            {
                return new RDEdge(x1, Math.Max(y1, y2), true);
            }
            else if (y1 == y2)//H
            {
                return new RDEdge(Math.Max(x1, x2), y1, false);
            }
            else throw new Exception();
        }
        private int GetEdgeState(RDEdge edge, object factPool)
        {
            FactPool pool = (FactPool)factPool;
            if (edge.Direction == false)
                return pool.HLine[edge.X, edge.Y];
            else
                return pool.VLine[edge.X, edge.Y];
        }
        public void Rope(string name, Dictionary<string, RDElement> dict, object factPool, Rule rule, Action<int> callBack, int callBackID)
        {
            FactPool pool = (FactPool)factPool;
            bool[,] visit = new bool[pool.X + 2, pool.Y + 2];
            int total1Count = 0;
            for (int x = 1; x <= pool.X + 1; ++x)
            {
                for (int y = 1; y <= pool.Y + 1; ++y)
                {
                    if (!visit[x, y])
                    {
                        int totalEdges = 0;
                        for (int d = 0; d < 4; ++d)
                        {
                            int tx = x + dirX[d], ty = y + dirY[d];
                            if (GetEdgeState(GetEdgeBetweenVertex(x, y, tx, ty), factPool) == 1)
                                ++totalEdges;
                        }
                        if (totalEdges == 1)
                            total1Count++;
                    }
                }
            }
            if (total1Count <= 2) return;// Maybe a single roop
            for (int x = 1; x <= pool.X + 1; ++x)
            {
                for (int y = 1; y <= pool.Y + 1; ++y)
                {
                    if(!visit[x,y])
                    {
                        int totalEdges = 0;
                        for (int d = 0; d < 4; ++d)
                        {
                            int tx = x + dirX[d], ty = y + dirY[d];
                            if (GetEdgeState(GetEdgeBetweenVertex(x, y, tx, ty),factPool) == 1)
                                ++totalEdges;
                        }
                        if(totalEdges==1)
                        {
                            visit[x, y] = true;
                            RDList list = new RDList();
                            int lastX = -2, lastY = -2, tx = -2, ty = -2, cx = x, cy = y;
                            bool end;
                            while(true)
                            {
                                end = true;
                                for (int d = 0; d < 4; ++d)
                                {
                                    tx = cx + dirX[d];
                                    ty = cy + dirY[d];
                                    if (GetEdgeState(GetEdgeBetweenVertex(cx, cy, tx, ty), factPool) == 1
                                        &&(tx!=lastX||ty!=lastY))
                                    {
                                        list.Data.Add(GetEdgeBetweenVertex(cx, cy, tx, ty));
                                        end = false;
                                        break;
                                    }
                                }
                                if (end) break;
                                lastX = cx;
                                lastY = cy;
                                cx = tx;
                                cy = ty;
                                if(visit[cx,cy])// Error!
                                {
                                    return ;
                                }
                                visit[cx, cy] = true;
                            }
                            //Console.WriteLine(list.Data.First() + "-" + list.Data.Last());
                            dict[name] = list;
                            callBack.Invoke(callBackID);
                        }
                    }
                }
            }
            dict.Remove(name);
            //Console.WriteLine("End");
        }
        public void GraphCut(string name, Dictionary<string, RDElement> dict, object factPool, Rule rule, Action<int> callBack, int callBackID)
        {
            FactPool pool = (FactPool)factPool;
            Queue<KeyValuePair<int,int>> myq = new Queue<KeyValuePair<int, int>>();

            bool[,] inq = new bool[pool.X + 2, pool.Y + 2];
            KeyValuePair<int,int>[,] parents=new KeyValuePair<int, int>[pool.X + 2, pool.Y + 2];

            inq[0, 0] = true;
            parents[0, 0] = new KeyValuePair<int,int>(-1,-1);
            myq.Enqueue(new KeyValuePair<int, int>(0, 0));
            while(myq.Count>0)
            {
                KeyValuePair<int, int> kv = myq.Dequeue();
                int x = kv.Key, y = kv.Value;
                for(int d=0;d<4;++d)
                {
                    int tx = x + dirX[d], ty = y + dirY[d];
                    if(tx>=0&&ty>=0&&tx<=pool.X+1&&ty<=pool.Y+1&&!inq[tx,ty])
                    {
                        if(GetEdgeState(GetEdgeBetweenCell(x,y,tx,ty),factPool)>0)
                        {
                            inq[tx, ty] = true;
                            parents[tx, ty] = kv;
                            myq.Enqueue(new KeyValuePair<int, int>(tx, ty));
                        }
                    }
                }
            }
            for(int x=0;x<=pool.X;++x)
            {
                for(int y=0;y<=pool.Y;++y)
                {
                    if (!inq[x, y]) continue;
                    for (int d = 0; d < 2; ++d)
                    {
                        int tx = x + dirX[d], ty = y + dirY[d];
                        if (!inq[tx, ty]) continue;
                        if (GetEdgeState(GetEdgeBetweenCell(x, y, tx, ty), factPool) == 0)
                        {
                            RDList list = new RDList();
                            list.Data.Add(GetEdgeBetweenCell(x, y, tx, ty));
                            while(parents[x, y].Key!=-1)
                            {
                                list.Data.Add(GetEdgeBetweenCell(x, y, parents[x,y].Key, parents[x, y].Value));
                                KeyValuePair<int, int> kv = parents[x, y];
                                x = kv.Key;
                                y = kv.Value;
                            }
                            while (parents[tx, ty].Key != -1)
                            {
                                list.Data.Add(GetEdgeBetweenCell(tx, ty, parents[tx, ty].Key, parents[tx, ty].Value));
                                KeyValuePair<int, int> kv = parents[tx, ty];
                                tx = kv.Key;
                                ty = kv.Value;
                            }
                            dict[name] = list;
                            //Console.WriteLine("Let's See:");
                            //Console.WriteLine(list);
                            callBack.Invoke(callBackID);
                            // Data has been changed, return directly
                            dict.Remove(name);
                            return;
                        }
                    }
                        
                }
            }
        }
    }
}
