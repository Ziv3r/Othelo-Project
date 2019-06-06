using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace View
{
    public partial class SettingsForm : Form
    {
        private int m_BoardSize = 6; //default size
        private string m_BoardSizeTxt = "Board Size: {0}x{0} (click to increse)";
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void BoardSizeBtn_Click(object sender, EventArgs e)
        {
            if (m_BoardSize >= 12)
            {
                m_BoardSize = 6;
            }
            else
            {
                m_BoardSize += 2;
            }
            BoardSizeBtn.Text = string.Format(m_BoardSizeTxt, m_BoardSize);
        }

        private void onePlayerBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void twoPlayersBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        public int BoardSize
        {
            get { return m_BoardSize; }
        }
    }
}
