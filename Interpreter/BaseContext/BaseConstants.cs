using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.BaseContext
{
    public class BaseConstants
    {
        public virtual bool IsConstant(Formula formula,out RDElement result)
        {
            if(formula.Parameters==null)
            {
                int res = 0;
                if (formula.OperationName == "TRUE")
                {
                    result = new RDBool(true);
                    return true;
                }
                if (formula.OperationName == "FALSE")
                {
                    result = new RDBool(false);
                    return true;
                }
                if (int.TryParse(formula.OperationName,out res))
                {
                    result = new RDNumber(res);
                    return true;
                }
            }
            result = null;
            return false;
        }
    }
}
