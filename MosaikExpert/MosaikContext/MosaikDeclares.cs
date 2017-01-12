using Interpreter;
using Interpreter.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MosaikExpert.MosaikContext
{
    class MosaikDeclares:BaseDeclares
    {
        public void NumberCell(string name, Dictionary<string, RDElement> dict, object factPool, Rule rule, Action<int> callBack, int callBackID)
        {
            FactPool pool = (FactPool)factPool;
            for (int x = 1; x <= pool.X; ++x)
            {
                for (int y = 1; y <= pool.Y; ++y)
                {
                    if (IsUnfinishedNumberCell(x, y, factPool))
                    {
                        dict[name] = new RDCell(x, y);
                        callBack.Invoke(callBackID);
                    }
                }
            }
            dict.Remove(name);
        }

        public void NeighborNumberPair(string name1, string name2, Dictionary<string, RDElement> dict, object factPool, Rule rule, Action<int> callBack, int callBackID)
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
                        callBack.Invoke(callBackID);
                        dict[name1] = new RDCell(x, y + 1);
                        dict[name2] = new RDCell(x, y);
                        callBack.Invoke(callBackID);
                    }
                    if (IsUnfinishedNumberCell(x, y, factPool) && IsUnfinishedNumberCell(x + 1, y, factPool))
                    {
                        dict[name1] = new RDCell(x, y);
                        dict[name2] = new RDCell(x + 1, y);
                        callBack.Invoke(callBackID);
                        dict[name1] = new RDCell(x + 1, y);
                        dict[name2] = new RDCell(x, y);
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
                        dict[name1] = new RDCell(x + 1, y + 1);
                        dict[name2] = new RDCell(x, y);
                        callBack.Invoke(callBackID);
                    }
                    if (IsUnfinishedNumberCell(x, y + 1, factPool) && IsUnfinishedNumberCell(x + 1, y, factPool))
                    {
                        dict[name1] = new RDCell(x, y + 1);
                        dict[name2] = new RDCell(x + 1, y);
                        callBack.Invoke(callBackID);
                        dict[name1] = new RDCell(x + 1, y);
                        dict[name2] = new RDCell(x, y + 1);
                        callBack.Invoke(callBackID);
                    }
                }
            }
            dict.Remove(name1);
            dict.Remove(name2);
        }
        public void SpacedNumberPair(string name1, string name2, string name3, Dictionary<string, RDElement> dict, object factPool, Rule rule, Action<int> callBack, int callBackID)
        {
            FactPool pool = (FactPool)factPool;
            for (int x = 1; x <= pool.X; ++x)
            {
                for (int y = 1; y <= pool.Y; ++y)
                {
                    if(x==15&&y==7)
                    {
                        ;
                    }
                    if (y + 2<=pool.Y&&IsUnfinishedNumberCell(x, y, factPool) && IsUnfinishedNumberCell(x, y + 2, factPool))
                    {
                        dict[name1] = new RDCell(x, y);
                        dict[name2] = new RDCell(x, y + 1);
                        dict[name3] = new RDCell(x, y + 2);
                        callBack.Invoke(callBackID);
                        dict[name3] = new RDCell(x, y);
                        dict[name2] = new RDCell(x, y + 1);
                        dict[name1] = new RDCell(x, y + 2);
                        callBack.Invoke(callBackID);
                    }
                    if (x + 2 <= pool.X && IsUnfinishedNumberCell(x, y, factPool) && IsUnfinishedNumberCell(x + 2, y, factPool))
                    {
                        dict[name1] = new RDCell(x, y);
                        dict[name2] = new RDCell(x + 1, y);
                        dict[name3] = new RDCell(x + 2, y);
                        callBack.Invoke(callBackID);
                        dict[name3] = new RDCell(x, y);
                        dict[name2] = new RDCell(x + 1, y);
                        dict[name1] = new RDCell(x + 2, y);
                        callBack.Invoke(callBackID);
                    }
                }
            }
            dict.Remove(name1);
            dict.Remove(name2);
            dict.Remove(name3);
        }
        public void NumberMatrix(string name1, string name2, string name3, string name4, Dictionary<string, RDElement> dict, object factPool, Rule rule, Action<int> callBack, int callBackID)
        {
            FactPool pool = (FactPool)factPool;
            for (int x = 1; x <= pool.X; ++x)
            {
                for (int y = 1; y <= pool.Y; ++y)
                {
                    if (IsUnfinishedNumberCell(x, y, factPool) && IsUnfinishedNumberCell(x + 1, y + 1, factPool) && IsUnfinishedNumberCell(x + 1, y, factPool) && IsUnfinishedNumberCell(x, y + 1, factPool))
                    {
                        dict[name1] = new RDCell(x, y);
                        dict[name2] = new RDCell(x, y + 1);
                        dict[name3] = new RDCell(x + 1, y);
                        dict[name4] = new RDCell(x + 1, y + 1);
                        callBack.Invoke(callBackID);
                        dict[name1] = new RDCell(x + 1, y);
                        dict[name2] = new RDCell(x + 1, y + 1);
                        dict[name3] = new RDCell(x, y);
                        dict[name4] = new RDCell(x, y + 1);
                        callBack.Invoke(callBackID);
                    }
                }
            }
            dict.Remove(name1);
            dict.Remove(name2);
            dict.Remove(name3);
            dict.Remove(name4);
        }
        private bool IsUnfinishedNumberCell(int x, int y, object factPool)
        {
            FactPool pool = (FactPool)factPool;
            if (pool.Cell[x, y] == -1) return false;
            for (int i = x - 1; i <= x + 1; ++i)
                for (int j = y - 1; j <= y + 1; ++j)
                    if (pool.Result[i, j] == 0) return true;
            return false;
        }
    }
}
