using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NMEACheckSum
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox1.Text.IndexOf('*') - textBox1.Text.IndexOf('$') > 1)
            {
                textBox2.Text = textBox1.Text + Convert.ToString(NL_CheckValue(GetBytes(textBox1.Text), textBox1.Text.IndexOf('$') + 1, textBox1.Text.IndexOf('*') - textBox1.Text.IndexOf('$') - 1), 16).ToUpper().PadLeft(2, '0');
            }
            else
            {
                MessageBox.Show("输入的NMEI命令，必须包含符号$和*");
            }
        }
        static byte[] GetBytes(string str)
        {
            int i;
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            for (i = 0; i < bytes.Length / 2 - 1; i++)
            {
                bytes[i + 1] = bytes[2 * (i + 1)];
            }
            return bytes;
        }
        int NL_CheckValue(byte[] p, int s, int n)
        {
            int checkvalue = 0, i;

            checkvalue = p[s];
            for (i = 1; i < n; i++)
            {
                checkvalue ^= p[i + s];
            }
            return checkvalue;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(textBox2.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }


}
