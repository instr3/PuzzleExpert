using Interpreter.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlitherlinkExpert.SlitherlinkContext
{
    class SlitherlinkSetters:BaseSetters
    {
        public bool ColorOf(RDEdge edge, RDNumber result, object factPool)
        {
            //Console.WriteLine(edge.X + "::" + edge.Y);
            FactPool pool = (FactPool)factPool;
            int[,] arr = edge.Direction ? pool.VLine : pool.HLine;
            if(arr[edge.X,edge.Y]!=0)
            {
                if(arr[edge.X,edge.Y]!=result.Data)
                    throw new Exception("结果冲突！");
                return false;
            }
            arr[edge.X, edge.Y] = result.Data;
            return true;
        }
        public bool AllColorOf(RDList edges, RDNumber result, object factPool)
        {
            var resultGroup = edges.Data.Select(edge => ColorOf(edge as RDEdge, result, factPool)).ToArray();
            return resultGroup.Contains(true);
        }
    }
}
