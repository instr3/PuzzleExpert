using Interpreter.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MosaikExpert.MosaikContext
{
    class MosaikSetters:BaseSetters
    {
        public bool ColorOf(RDCell cell, RDNumber result, object factPool)
        {
            //Console.WriteLine(edge.X + "::" + edge.Y);
            FactPool pool = (FactPool)factPool;
            if (pool.Result[cell.X, cell.Y] != 0)
            {
                if (pool.Result[cell.X, cell.Y] != result.Data)
                    throw new Exception("结果冲突！");
                return false;
            }
            pool.Result[cell.X, cell.Y] = result.Data;
            return true;
        }
        public bool AllColorOf(RDList cells, RDNumber result, object factPool)
        {
            var resultGroup = cells.Data.Select(cell => ColorOf(cell as RDCell, result, factPool)).ToArray();
            return resultGroup.Contains(true);
        }
    }
}
