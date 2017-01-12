using Interpreter;
using MosaikExpert.MosaikContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MosaikExpert
{
    public partial class Form1 : Form
    {
        BufferedGraphics bg;
        Graphics target;
        MosaikPuzzle msp;
        InferenceEngine engine;
        RuleInterpreter RI;
        public Form1()
        {
            InitializeComponent();
        }
        void LoadByID(string id)
        {
            if (id == null)
                id = "test.txt";
            else
                throw new NotSupportedException();
            //try
            {
                using (StreamReader sr = new StreamReader(id))
                {
                    msp = MosaikPuzzle.FromTXT(sr);
                }
                button1_Click(null, null);
                engine = null;
            }
            //catch
            {

            }
        }
        private double GetPercentage()
        {
            int total = 0, solved = 0;
            for (int i = 1; i <= msp.factPool.X; ++i)
                for (int j = 1; j <= msp.factPool.Y; ++j)
                {
                    if (msp.factPool.Result[i, j] > 0) ++solved;
                    ++total;
                }
            return solved / (double)total;
        }
        private void ShowPercentage()
        {
            labelPercentage.Text = string.Format("{0:0.00}%", GetPercentage() * 100);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            msp.Draw(bg.Graphics);
            bg.Render(target);
            ShowPercentage();
        }
        bool[] GetRuleSelected()
        {
            return Enumerable.Range(0, RI.RuleSet.Length).Select(i => checkedListBox1.GetItemCheckState(i) == CheckState.Checked).ToArray();
        }
        void CreateEngine()
        {
            Context context = new Context(new MosaikDeclares(), new MosaikOperations(), new MosaikSetters(), new MosaikConstants());
            engine = new InferenceEngine(RI, context, msp.factPool,
                GetRuleSelected());
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            target = pictureBox1.CreateGraphics();
            bg = BufferedGraphicsManager.Current.Allocate(target, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            bg.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            bg.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            using (StreamReader sr = new StreamReader("rules.txt"))
            {
                RI = new RuleInterpreter(sr.ReadToEnd());
                textBox1.Text = RI.ToString();
                for (int i = 0; i < RI.RuleSet.Length; ++i)
                {
                    checkedListBox1.Items.Add("[" + RI.RuleIDs[i] + "]" + RI.RuleDescriptions[i], true);
                }
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            LoadByID(null);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            LoadByID(textBox2.Text);
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (engine != null)
                engine.RuleEnabled = GetRuleSelected();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            engine = null;
            msp.factPool = new FactPool(msp);
            button2_Click(sender, e);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (engine == null) CreateEngine();
            if (!engine.SingleStep())
                MessageBox.Show("推理已经结束！");
            textBox1.Text = engine.LogText;
            button1_Click(sender, e);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (engine == null) CreateEngine();
            engine.FindAllFacts();
            textBox1.Text = engine.LogText;
            button1_Click(sender, e);

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                    {
                        msp = null;
                        msp = MosaikPuzzle.FromXML(sr);
                    }
                    button1_Click(null, null);
                    engine = null;
                }
                catch
                {
                    MessageBox.Show("文件不存在或文件格式不正确！");
                }
            }

        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }
    }
}
