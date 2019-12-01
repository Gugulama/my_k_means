using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kmeans
{
    public partial class Form2 : Form
    {
        public int clasters, pointCount;
        public Form2()
        {
            InitializeComponent();            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                try
                {
                    clasters = Int32.Parse(textBox1.Text);
                    pointCount = Int32.Parse(textBox2.Text);
                    this.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
