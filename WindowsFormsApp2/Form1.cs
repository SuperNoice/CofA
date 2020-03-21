using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{


    public partial class Form1 : Form
    {
        struct Edge
        {
            public
            int TransmitterVertex, ReceiverVertex;

            public Edge(int trans, int rec)
            {
                this.TransmitterVertex = trans;
                this.ReceiverVertex = rec;
            }

            public void SetTransmitter(int newValue)
            {
                this.TransmitterVertex = newValue;
            }

            public void SetReceiver(int newValue)
            {
                this.ReceiverVertex = newValue;
            }

            public int GetTransmitter()
            {
                return this.TransmitterVertex;
            }

            public int GetReceiver()
            {
                return this.ReceiverVertex;
            }

            public void EdgeSetup(int value1, int value2)
            {
                this.TransmitterVertex = value1;
                this.ReceiverVertex = value2;
            }
        };
        public Form1()
        {
            InitializeComponent();
        }

        string Normal(string str)
        {
            string res = "";
            bool f = false;
            for (int i = 0; i < str.Length; i++)
            {

                if (f)
                {
                    if (str[i] == '-') continue;
                    res += str[i];
                }
                if (str[i] == ':') f = true;
            }

            return res;
        }

        bool Provsm(List<Edge> list, int st, int str)
        {
            bool f = false;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].GetTransmitter() == st && list[i].GetReceiver() == str) f = true;
                if (list[i].GetReceiver() == st && list[i].GetTransmitter() == str) f = true;
            }

            return f;
        }
        void Fill_all(int[,] mas1)
        {
            richTextBox1.Clear();
            richTextBox2.Clear();
            richTextBox3.Clear();
            richTextBox4.Clear();
            textBox1.Clear();

            int size = Convert.ToInt32(Math.Sqrt(mas1.Length));


            for (int i = 0; i < size; i++)                          //RTB1
            {
                for (int j = 0; j < size; j++)
                {
                    richTextBox1.AppendText(Convert.ToString(mas1[i, j]) + " ");
                }
                richTextBox1.AppendText("\n");
            }
            richTextBox1.Undo();

            // RTB4

            for (int i = 0; i < size; i++)
            {
                richTextBox4.AppendText(i + ":");
                for (int j = 0; j < size; j++)
                {
                    if (mas1[i, j] == 1) { richTextBox4.AppendText(Convert.ToString(j)); richTextBox4.AppendText("->"); }
                }
                richTextBox4.Undo();
                richTextBox4.AppendText("\n");
            }
            richTextBox4.Undo();


            List<Edge> list = new List<Edge>();         // RTB2



            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (mas1[i, j] == 1 && Provsm(list, i, j)) { Edge tmp = new Edge(); tmp.EdgeSetup(i, j); list.Add(tmp); }

            int st = list.Count, str = size;

            int[,] rv = new int[st, str];

            for (int i = 0; i < st; i++)
            {
                rv[list[i].GetTransmitter(), i] = 1;
                rv[list[i].GetReceiver(), i] = 1;
            }

            for (int i = 0; i < str; i++)
            {
                for (int j = 0; j < st; j++)
                {
                    richTextBox2.AppendText(Convert.ToString(rv[i, j]));
                }
                richTextBox2.AppendText("\n");
            }
            richTextBox2.Undo();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            int[,] mas1, mas2, mas3, mas4;
            string[] str1, str2;
            bool f = true;

            if (richTextBox1.Text.Length != 0 && f)
            {
                string str = richTextBox1.Text;
                str1 = str.Split('\n');
                str2 = str1[0].Split(' ');
                mas1 = new int[str1.Length, str2.Length];

                for (int i = 0; i < str1.Length; i++)
                {
                    str2 = str1[i].Split(' ');
                    for (int j = 0; j < str2.Length; j++)
                    {
                        try
                        {
                            mas1[i, j] = Convert.ToInt32(str2[j]);
                            textBox1.Clear();
                        }
                        catch (Exception)
                        {
                            textBox1.Clear();
                            textBox1.AppendText("Error");

                        }
                    }
                }
                Fill_all(mas1);
                f = false;
            }
            if (richTextBox2.Text.Length != 0 && f)
            {
                string str = richTextBox1.Text;
                str1 = str.Split('\n');
                str2 = str1[0].Split(' ');
                mas1 = new int[str1.Length, str1.Length];
                List<int> pair = new List<int>();

                for (int j = 0; j < str2.Length; j++)
                {

                    for (int i = 0; i < str1.Length; i++)
                    {
                        str2 = str1[i].Split(' ');
                        if (str2[j] == "1") pair.Add(i);
                    }

                    mas1[pair[0], pair[1]] = 1;
                    pair.Clear();
                }
                Fill_all(mas1);
                f = false;
            }
            if (richTextBox3.Text.Length != 0 && f)
            {
                string str = richTextBox1.Text;
                str1 = str.Split('\n');
                str2 = str1[0].Split(' ');
                mas3 = new int[str1.Length, str2.Length];

                for (int i = 0; i < str1.Length; i++)
                {
                    str2 = str1[i].Split(' ');
                    for (int j = 0; j < str2.Length; j++)
                    {

                    }
                }
                f = false;
            }
            if (richTextBox4.Text.Length != 0 && f)
            {
                string str = richTextBox4.Text;
                str1 = str.Split('\n');

                mas1 = new int[str1.Length, str1.Length];

                for (int i = 0; i < str1.Length; i++)
                {
                    str1[i] = Normal(str1[i]);
                    str2 = str1[i].Split('>');
                    for (int j = 0; j < str2.Length; j++)
                    {
                        mas1[i, Convert.ToInt32(str2[j])] = 1;
                        mas1[Convert.ToInt32(str2[j]), i] = 1;
                    }
                }
                Fill_all(mas1);
                textBox1.AppendText("ok");
                f = false;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox2.Clear();
            richTextBox3.Clear();
            richTextBox4.Clear();
            textBox1.Clear();
        }
    }
}
