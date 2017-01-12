using Interpreter.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterTest.TestContext
{
    class TestOperations:BaseOperations
    {
        public RDBool IsSquareNumber(RDNumber number,object factPool)
        {
            return new RDBool(((FactPool)factPool).isSquare[number.Data]);
        }
    }
}
