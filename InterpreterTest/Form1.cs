using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Interpreter;
using System.IO;
using InterpreterTest.TestContext;

namespace InterpreterTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal)).ToArray();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            foreach(var i in GetTypesInNamespace(Assembly.GetExecutingAssembly(),"InterpreterTest"))
            {
                MessageBox.Show(i.ToString());
            }
            return;
            FatherClass child = new ChildClass("baby");
            FatherClass father = new FatherClass("father");
            //Type fatherType = Type.GetType("FatherClass");
            MessageBox.Show(GetType().Namespace);

            string ns = GetType().Namespace;// + ".SecretSpace";
            Type fatherType = Type.GetType(ns + ".FatherClass");
            MethodInfo fatherMethod = fatherType.GetMethod("Yell");
            fatherMethod.Invoke(child, new object[] { "abcdefg"});
            fatherMethod.Invoke(father, new object[] { "abcdefg" });
            Type childType = child.GetType();
            childType.GetMethod("IsCute").Invoke(null,new object[] { });
            MethodInfo childMethod = childType.GetMethod("Cry");
            childMethod.Invoke(child, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(int.Parse("1]\n").ToString());
        }
        RuleInterpreter RI;
        private void button3_Click(object sender, EventArgs e)
        {
            using (StreamReader sr = new StreamReader("rules.txt"))
            {
                RI = new RuleInterpreter(sr.ReadToEnd());
                textBox1.Text = RI.ToString();
            }
        }
        FactPool factPool = new FactPool();
        private void button4_Click(object sender, EventArgs e)
        {
            using (StreamReader sr = new StreamReader("rules.txt"))
            {
                RI = new RuleInterpreter(sr.ReadToEnd());
                textBox1.Text = RI.ToString();
                Context context = new Context(new TestDeclares(), new TestOperations(), new TestSetters(),new Interpreter.BaseContext.BaseConstants());
                InferenceEngine engine = new InferenceEngine(RI, context, factPool);
                engine.FindAllFacts();
                string res = "";
                for(int i=0;i<=200;++i)
                {
                    if (factPool.isDiagnal[i])
                        res += i.ToString() + " ";
                }
                textBox1.Text += engine.LogText;
                MessageBox.Show(res);
            }

        }
    }
    public class FatherClass
    {
        public FatherClass(string inputName)
        {
            name = inputName;
        }
        public string name;
        public virtual void Yell(string message)
        {
            MessageBox.Show(name + ":WWWWWWWWWWWWWWWooooo " + message);
        }
    }
    public class ChildClass : FatherClass
    {
        public ChildClass(string inputName) : base(inputName)
        {

        }
        public override void Yell(string message)
        {
            MessageBox.Show(name + ":Moew, " + message);
        }
        public void Cry()
        {
            MessageBox.Show(name + ":TAT");
        }
        public static void IsCute()
        {
            MessageBox.Show("Certainly!");
        }
    }
    namespace SecretSpace
    {

        public class FatherClass
        {
            public FatherClass(string inputName)
            {
                name = inputName;
            }
            public string name;
            public virtual void Yell(string message)
            {
                MessageBox.Show(name + ":HAHAHAHAHAH " + message);
            }
        }
    }

}

namespace WhateverNameSpace
{
    public class YouCantSeeMe
    {
        public static void Aloha()
        {
            MessageBox.Show("Wow");
        }
    }
}