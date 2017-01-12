using Interpreter.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MosaikExpert.MosaikContext
{
    class MosaikOperations:BaseOperations
    {
        public RDNumber NumberOf(RDCell cell, object factPool)
        {
            return new RDNumber(((FactPool)factPool).Cell[cell.X, cell.Y]);
        }
        public RDNumber ColorOf(RDCell cell, object factPool)
        {
            FactPool pool = (FactPool)factPool;
            return new RDNumber(pool.Result[cell.X,cell.Y]);
        }
        public RDList SurroundingOf(RDCell cell, object factPool)
        {
            return new RDList(new RDElement[] {
                new RDCell(cell.X-1,cell.Y-1),
                new RDCell(cell.X-1,cell.Y),
                new RDCell(cell.X-1,cell.Y+1),
                new RDCell(cell.X,cell.Y-1),
                new RDCell(cell.X,cell.Y),
                new RDCell(cell.X,cell.Y+1),
                new RDCell(cell.X+1,cell.Y-1),
                new RDCell(cell.X+1,cell.Y),
                new RDCell(cell.X+1,cell.Y+1)
            });
        }
        public RDList ColorsOf(RDList cells, object factPool)
        {
            return new RDList(cells.Data.Select(
                e => ColorOf((RDCell)e, factPool)).ToArray());
        }
        public RDList UncoloredSubsetOf(RDList celllist, object factPool)
        {
            return new RDList(celllist.Data.Where(
                e => ColorOf((RDCell)e, factPool).Data == 0).ToArray());
        }
        public RDList BlackSubsetOf(RDList celllist, object factPool)
        {
            return new RDList(celllist.Data.Where(
                e => ColorOf((RDCell)e, factPool).Data == 1).ToArray());
        }
        public RDList WhiteSubsetOf(RDList celllist, object factPool)
        {
            return new RDList(celllist.Data.Where(
                e => ColorOf((RDCell)e, factPool).Data == 2).ToArray());
        }

        /*public RDList IntersectionOf(RDList lhs, RDList rhs, object factPool)
        {
            RDCell[] lc = (RDCell[])lhs.Data.ToArray();
            RDCell[] rc = (RDCell[])rhs.Data.ToArray();
            return new RDList(lc.Intersect(rc).ToArray());
        }
        public RDList UnoinOf(RDList lhs, RDList rhs, object factPool)
        {
            RDCell[] lc = (RDCell[])lhs.Data.ToArray();
            RDCell[] rc = (RDCell[])rhs.Data.ToArray();
            return new RDList(lc.Union(rc).ToArray());
        }
        public RDList Except(RDList lhs, RDList rhs, object factPool)
        {
            RDCell[] lc = (RDCell[])lhs.Data.ToArray();
            RDCell[] rc = (RDCell[])rhs.Data.ToArray();
            return new RDList(lc.Except(rc).ToArray());
        }*/
    }
}
