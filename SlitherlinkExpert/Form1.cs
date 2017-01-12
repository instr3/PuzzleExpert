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
using Interpreter;
using SlitherlinkExpert.SlitherlinkContext;
using System.Collections;

namespace SlitherlinkExpert
{
    public partial class Form1 : Form
    {
        BufferedGraphics bg;
        Graphics target;
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
                    slp = SlitherlinkPuzzle.FromTXT(sr);
                }
                button1_Click(null, null);
                engine = null;
            }
            //catch
            {

            }
        }
        bool[] GetRuleSelected()
        {
            return Enumerable.Range(0, RI.RuleSet.Length).Select(i => checkedListBox1.GetItemCheckState(i)==CheckState.Checked).ToArray();
        }
        void CreateEngine()
        {
            Context context = new Context(new SlitherlinkDeclares(), new SlitherlinkOperations(), new SlitherlinkSetters(), new SlitherlinkConstants());
            engine = new InferenceEngine(RI, context, slp.factPool,
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
                for(int i=0;i<RI.RuleSet.Length;++i)
                {
                    checkedListBox1.Items.Add("["+RI.RuleIDs[i]+"]"+RI.RuleDescriptions[i], true);
                }
            }

        }
        SlitherlinkPuzzle slp;
        private void button1_Click(object sender, EventArgs e)
        {
            slp.Draw(bg.Graphics);
            bg.Render(target);
            ShowPercentage();
        }
        private double GetPercentage()
        {
            int total=0, solved = 0;
            for (int i = 1; i <= slp.factPool.X; ++i)
                for (int j = 1; j <= slp.factPool.Y + 1; ++j)
                {
                    if (slp.factPool.VLine[i, j] > 0) ++solved;
                    ++total;
                }
            for (int i = 1; i <= slp.factPool.X + 1; ++i)
                for (int j = 1; j <= slp.factPool.Y; ++j)
                {
                    if (slp.factPool.HLine[i, j] > 0) ++solved;
                    ++total;
                }
            return solved / (double)total;
        }
        private void ShowPercentage()
        {
            labelPercentage.Text = string.Format("{0:0.00}%", GetPercentage() * 100);
        }
        InferenceEngine engine;
        RuleInterpreter RI;
        private void button2_Click(object sender, EventArgs e)
        {
            if (engine == null) CreateEngine();
            engine.FindAllFacts();
            textBox1.Text = engine.LogText;
            button1_Click(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (engine == null) CreateEngine();
            if (!engine.SingleStep())
                MessageBox.Show("推理已经结束！");
            textBox1.Text = engine.LogText;
            button1_Click(sender, e);

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            LoadByID(textBox2.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox2.Text = (long.Parse(textBox2.Text) - 1).ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox2.Text = (long.Parse(textBox2.Text) + 1).ToString();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            LoadByID(null);
        }

        private void checkedListBox1_SelectedValueChanged(object sender, EventArgs e)
        {
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (engine != null)
                engine.RuleEnabled = GetRuleSelected();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            engine = null;
            slp.factPool = new FactPool(slp);
            button2_Click(sender, e);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                    {
                        slp = null;
                        slp = SlitherlinkPuzzle.FromXML(sr);
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
    }
}
