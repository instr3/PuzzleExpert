using Interpreter.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter;

namespace SlitherlinkExpert.SlitherlinkContext
{
    class SlitherlinkConstants:BaseConstants
    {
        public override bool IsConstant(Formula formula, out RDElement result)
        {
            if (formula.Parameters == null)
            {
                if (formula.OperationName == "BLACK")
                {
                    result = new RDNumber(1);
                    return true;
                }
                if (formula.OperationName == "WHITE")
                {
                    result = new RDNumber(2);
                    return true;
                }
                if (formula.OperationName == "DIFFERENT")
                {
                    result = new RDNumber(1);
                    return true;
                }
                if (formula.OperationName == "SAME")
                {
                    result = new RDNumber(2);
                    return true;
                }
                if (formula.OperationName == "UNDECIDED")
                {
                    result = new RDNumber(0);
                    return true;
                }
            }
            return base.IsConstant(formula, out result);
        }
    }
}
