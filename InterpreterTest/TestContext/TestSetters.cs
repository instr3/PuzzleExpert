using Interpreter.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterTest.TestContext
{
    class TestSetters:BaseSetters
    {
        public bool IsSquareNumber(RDNumber n,RDBool result,object factPool)
        {
            FactPool pool = (FactPool)factPool;
            if (pool.isSquare[n.Data] == result.Data) return false;
            pool.isSquare[n.Data] = result.Data;
            return true;
        }
        public bool IsDiagonalNumber(RDNumber n, RDBool result, object factPool)
        {
            FactPool pool = (FactPool)factPool;
            if (pool.isDiagnal[n.Data] == result.Data) return false;
            pool.isDiagnal[n.Data] = result.Data;
            return true;
        }
    }
}
