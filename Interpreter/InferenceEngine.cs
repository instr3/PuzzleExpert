using Interpreter.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Interpreter
{
    public class InferenceEngine
    {
        public RuleInterpreter RuleInterpreter { get; set; }
        public Context Context { get; set; }
        Dictionary<string, MethodInfo> declares;
        Dictionary<string, MethodInfo> operations;
        Dictionary<string, MethodInfo> setters;
        public object FactPool;
        public bool[] RuleEnabled;
        public InferenceEngine(RuleInterpreter ruleInterpreter,Context context,object factPool,bool[] ruleEnabled=null)
        {
            if(ruleEnabled==null)
            {
                RuleEnabled = new bool[ruleInterpreter.RuleSet.Length];
                for(int i=0;i< ruleInterpreter.RuleSet.Length;++i)
                {
                    RuleEnabled[i] = true;
                }
            }
            else RuleEnabled = ruleEnabled;
            RuleInterpreter = ruleInterpreter;
            Context = context;
            FactPool = factPool;

            MethodInfo[] methods;
            // Extract Declarations
            Type declareType = Context.Declares.GetType();
            methods = declareType.GetMethods();
            declares = new Dictionary<string, MethodInfo>();
            foreach (MethodInfo method in methods)
            {
                if (method.IsPublic)
                    declares.Add(method.Name, method);
            }
            
            // Extract Operations
            Type operationType = Context.Operations.GetType();
            methods = operationType.GetMethods();
            operations = new Dictionary<string, MethodInfo>();
            foreach (MethodInfo method in methods)
            {
                if(method.IsPublic)
                    operations.Add(method.Name, method);
            }
            // Extract Setters
            Type setterType = Context.Setters.GetType();
            methods = setterType.GetMethods();
            setters = new Dictionary<string, MethodInfo>();
            foreach (MethodInfo method in methods)
            {
                if (method.IsPublic)
                    setters.Add(method.Name, method);
            }

        }
        public bool SingleStep()
        {
            for (int i = 0; i < RuleInterpreter.RuleSet.Length; ++i)
            {
                if (!RuleEnabled[i]) continue;
                Rule rule = RuleInterpreter.RuleSet[i];
                if (TryFire(rule))
                {
                    LogSuccessFire(i);
                    return true;
                }
            }
            return false;
        }
        public void FindAllFacts()
        {
            LogText = "";
            while (true)
            {
                bool success = false;
                for(int i=0;i< RuleInterpreter.RuleSet.Length;++i)
                {
                    if (!RuleEnabled[i]) continue;
                    Rule rule = RuleInterpreter.RuleSet[i];
                    if (TryFire(rule))
                    {
                        LogSuccessFire(i);
                        success = true;
                    }
                }
                if (!success)
                    break;
            }
        }
        private void LogSuccessFire(int ruleNo)
        {
            string res = "[R" + RuleInterpreter.RuleIDs[ruleNo] + "] " + 
                 tempSetterLog + Environment.NewLine;
            LogText += res;
        }
        string tempSetterLog;
        private bool FireSetters(Rule rule, Dictionary<string, RDElement> assginedValue)
        {
            bool successSet = false;
            foreach(Result r in rule.Results)
            {
                RDElement[] parameterValues = r.Parameters.Select(p => Calculate(p, assginedValue)).ToArray();
                RDElement result = Calculate(r.AssignFormula, assginedValue);
                MethodInfo method = null;
                if (!setters.TryGetValue(r.SetterName, out method))
                {
                    throw new Exception("THEN部分左值出错：Setter未定义：" + r.SetterName);
                }
                List<object> input = (parameterValues as object[]).ToList();
                input.Add(result);
                input.Add(FactPool);
                if ((bool)method.Invoke(Context.Setters, input.ToArray()))
                {
                    tempSetterLog = string.Join(", ", tempAssginedValue.Select(kv => kv.Key + "=" + kv.Value.ToString()))
                        + " => " + Environment.NewLine + r.SetterName + "(" +
                        string.Join(",", (object[])parameterValues) + ") := " + result;
                    successSet = true;
                }
            }
            return successSet;
        }
        Rule tempRule;
        Dictionary<string, RDElement> tempAssginedValue;
        bool tempSuccessFire;
        public string LogText { get; private set; }
        private void DeclareCallBack(int declareID)
        {
            if (tempSuccessFire == true)
                return;
            if(declareID==tempRule.Declaration.Generator.Length) // 变量已经全部定义
            {
                // Will fail if condition doesn't return a RDBool
                RDBool result = Calculate(tempRule.Condition, tempAssginedValue) as RDBool;
                if(result.Data==true)// 如果IF条件满足
                {
                    if (FireSetters(tempRule, tempAssginedValue)) // 尝试触发THEN
                        tempSuccessFire = true; // 触发成功
                }
            }
            else
            {
                MethodInfo method = null;
                if (!declares.TryGetValue(tempRule.Declaration.Generator[declareID].Method, out method))
                {
                    throw new Exception("定义部分出错：函数未找到：" + tempRule.Declaration.Generator[declareID].Method);
                }
                List<object> input = (tempRule.Declaration.Generator[declareID].Variables as object[]).ToList();
                input.AddRange(new object[] {
                    tempAssginedValue,
                    FactPool,
                    tempRule,
                    new Action<int>(DeclareCallBack),
                    declareID+1
                    });
                method.Invoke(Context.Declares,input.ToArray());
            }
        }
        private bool TryFire(Rule rule)
        {
            tempSuccessFire = false;
            tempRule = rule;
            tempAssginedValue = new Dictionary<string, RDElement>();
            DeclareCallBack(0);
            return tempSuccessFire;
        }
        private RDElement Calculate(Formula formula, Dictionary<string, RDElement> assginedValue)
        {
            if(formula.Parameters==null)
            {
                RDElement value = null;
                if (Context.Constant.IsConstant(formula, out value))
                    return value;
                if(!assginedValue.TryGetValue(formula.OperationName,out value))
                {
                    throw new Exception("推理机错误：变量未定义：" + formula.OperationName);
                }
                return value;
            }
            else
            {
                if(formula.OperationName=="And"&& formula.Parameters.Length==2)
                {
                    RDBool left = Calculate(formula.Parameters[0], assginedValue) as RDBool;
                    if (left.Data == true)
                        return Calculate(formula.Parameters[1], assginedValue) as RDBool;
                    else
                        return left;
                }
                else if (formula.OperationName == "Or" && formula.Parameters.Length == 2)
                {
                    RDBool left = Calculate(formula.Parameters[0], assginedValue) as RDBool;
                    if (left.Data == false)
                        return Calculate(formula.Parameters[1], assginedValue) as RDBool;
                    else
                        return left;
                }
                List<object> input = formula.Parameters.Select(p => (object)Calculate(p, assginedValue)).ToList();
                input.Add(FactPool);
                MethodInfo method = null;
                if (!operations.TryGetValue(formula.OperationName, out method))
                {
                    throw new Exception("推理机错误：函数未定义：" + formula.OperationName);
                }
                RDElement result = method.Invoke(Context.Operations, input.ToArray()) as RDElement;
                return result;
            }
        }
    }
}
