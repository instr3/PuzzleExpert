using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.BaseContext
{
    public class BaseOperations
    {
        public class Operator
        {
            public string name;
            public int priority;
            public string function;
            public Operator(string inputName,int inputPriority,string inputFunction)
            {
                name = inputName;
                priority = inputPriority;
                function = inputFunction;
            }
        };
        public static readonly Operator[] OperatorList = new Operator[]
        {
            // Binary
            new Operator("OR",0,"Or"),
            new Operator("AND",1,"And"),
            new Operator("ISANY",2,"IsAny"),
            new Operator("AREALL",2,"AreAll"),
            new Operator("<",2,"LessThan"),
            new Operator(">",2,"GreaterThan"),
            new Operator("<=",2,"LessOrEqual"),
            new Operator(">=",2,"GreaterOrEqual"),
            new Operator("!=",2,"NotEqual"),
            new Operator("IS",2,"Equal"),
            new Operator("=",2,"Equal"),
            new Operator(";",3,"Pair"),
            new Operator("+",4,"Add"),
            new Operator("-",4,"Subtract"),
            new Operator("*",5,"Multiply"),
            new Operator("/",5,"Divide"),
            new Operator("%",5,"Mod"),
            // Unary
            new Operator("NOT",6,"Not"),
        };
        public virtual RDNumber Add(RDElement lhs, RDElement rhs, object factPool)
        {
            return new RDNumber(((RDNumber)lhs).Data + ((RDNumber)rhs).Data);
        }
        public virtual RDNumber Subtract(RDElement lhs, RDElement rhs, object factPool)
        {
            return new RDNumber(((RDNumber)lhs).Data - ((RDNumber)rhs).Data);
        }
        public virtual RDNumber Multiply(RDElement lhs, RDElement rhs, object factPool)
        {
            return new RDNumber(((RDNumber)lhs).Data * ((RDNumber)rhs).Data);
        }
        public virtual RDNumber Divide(RDElement lhs, RDElement rhs, object factPool)
        {
            return new RDNumber(((RDNumber)lhs).Data / ((RDNumber)rhs).Data);
        }
        public virtual RDNumber Mod(RDElement lhs, RDElement rhs, object factPool)
        {
            return new RDNumber(((RDNumber)lhs).Data % ((RDNumber)rhs).Data);
        }
        public virtual RDBool Equal(RDElement lhs, RDElement rhs, object factPool)
        {
            if(lhs is RDNumber)
                return new RDBool(((RDNumber)lhs).Data == ((RDNumber)rhs).Data);
            if(lhs is RDBool)
                return new RDBool(((RDBool)lhs).Data == ((RDBool)rhs).Data);
            throw new NotImplementedException();
        }
        public RDBool NotEqual(RDElement lhs, RDElement rhs, object factPool)
        {
            return Equal(lhs, rhs, factPool).Not();
        }
        public virtual RDBool LessThan(RDElement lhs, RDElement rhs, object factPool)
        {
            return new RDBool(((RDNumber)lhs).Data < ((RDNumber)rhs).Data);
        }
        public virtual RDBool GreaterThan(RDElement lhs, RDElement rhs, object factPool)
        {
            return new RDBool(((RDNumber)lhs).Data > ((RDNumber)rhs).Data);
        }
        public RDBool LessOrEqual(RDElement lhs, RDElement rhs, object factPool)
        {
            return LessThan(lhs, rhs, factPool).Or(Equal(lhs, rhs, factPool));
        }
        public RDBool GreaterOrEqual(RDElement lhs, RDElement rhs, object factPool)
        {
            return GreaterThan(lhs, rhs, factPool).Or(Equal(lhs, rhs, factPool));
        }
        public RDBool And(RDBool lhs, RDBool rhs, object factPool)
        {
            if (lhs.Data)
                return rhs;
            else
                return lhs;
        }
        public RDBool Or(RDBool lhs, RDBool rhs, object factPool)
        {
            if (lhs.Data)
                return lhs;
            else
                return rhs;
        }
        public RDBool Not(RDBool rhs, object factPool)
        {
            return rhs.Not();
        }
        public RDBool AreAll(RDList lhs,RDElement rhs, object factPool)
        {
            foreach(RDElement x in lhs.Data)
            {
                if(!Equal(x,rhs,factPool).Data)
                {
                    return new RDBool(false);
                }
            }
            return new RDBool(true);
            //return new RDBool(lhs.Data.All(x => Equal(x, rhs, factPool).Data));
        }
        public RDBool IsAny(RDList lhs, RDElement rhs, object factPool)
        {
            foreach (RDElement x in lhs.Data)
            {
                if (Equal(x, rhs, factPool).Data)
                {
                    return new RDBool(true);
                }
            }
            return new RDBool(false);
            // return new RDBool(lhs.Data.Any(x => Equal(x, rhs, factPool).Data));
        }
        public RDNumber CountOf(RDList lhs,object factPool)
        {
            return new RDNumber(lhs.Data.Count);
        }
        public RDElement FirstOf(RDList lhs,object factPool)
        {
            return lhs.Data[0];
        }
        public RDElement LastOf(RDList lhs,object factPool)
        {
            return lhs.Data[lhs.Data.Count - 1];
        }
        public RDList IntersectionOf(RDList lhs, RDList rhs, object factPool)
        {
            return new RDList(lhs.Data.Intersect(rhs.Data).ToArray());
        }
        public RDList UnionOf(RDList lhs, RDList rhs, object factPool)
        {
            return new RDList(lhs.Data.Union(rhs.Data).ToArray());
        }
        public RDList Except(RDList lhs, RDList rhs, object factPool)
        {
            return new RDList(lhs.Data.Except(rhs.Data).ToArray());
        }
    }
}
