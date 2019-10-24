using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        Form1 firstForm = new Form1();
        private void button1_Click(object sender, EventArgs e)
        {
            firstForm.Show();
        }

        Form2 secondForm = new Form2();
        private void button2_Click(object sender, EventArgs e)
        {
            secondForm.Show();
        }
    }
}
