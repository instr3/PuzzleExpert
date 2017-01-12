using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.BaseContext;
namespace Interpreter
{
    public class Context
    {
        public BaseDeclares Declares { get; set; }
        public BaseOperations Operations { get; set; }
        public BaseSetters Setters { get; set; }
        public BaseConstants Constant { get; set; }
        public Context(BaseDeclares declares, BaseOperations operations, BaseSetters setters,BaseConstants constant)
        {
            Declares = declares;
            Operations = operations;
            Setters = setters;
            Constant = constant;
        }
    }
}
