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
    public partial class MyMessageBox : Form
    {
        public MyMessageBox()
        {
            InitializeComponent();
        }

        static MyMessageBox newMessageBox;
        static string Button_id;

        public static string ShowBox(string txtMessage, string txtTitle)
        {
            newMessageBox = new MyMessageBox();
            newMessageBox.labelMessage.Text = txtMessage;
            newMessageBox.Text = txtTitle;
            newMessageBox.ShowDialog();
            return Button_id;
        }

        Form3 thirdForm = new Form3();
        private void buttonRestart_Click(object sender, EventArgs e)
        {
            Button_id = "1";
            newMessageBox.Dispose();
            thirdForm.Show();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Button_id = "2";
            newMessageBox.Dispose();
            Application.Exit();
        }
    }
}
