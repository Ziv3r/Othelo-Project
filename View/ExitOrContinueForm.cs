using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace View
{
    public partial class ExitOrContinueForm : Form
    {
        public ExitOrContinueForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void ExitOrContinueForm_Load(object sender, EventArgs e)
        {
            string message = "some message";
            MessageBox.Show(message);
        }
    }
}
