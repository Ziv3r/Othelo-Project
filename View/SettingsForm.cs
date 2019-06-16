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
        private const string k_BoardSizeTxt = "Board Size: {0}x{0} (click to increse)";
        private int m_BoardSize = 6; // default size

        public SettingsForm()
        {
            Application.EnableVisualStyles();
            InitializeComponent();
        }

        public int BoardSize
        {
            get { return m_BoardSize; }
        }

        public Button OnePlayerButton
        {
            get { return OnePlayerBtn; }
        }

        public Button TwoPlayersButton
        {
            get { return TwoPlayersBtn; }
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

            BoardSizeBtn.Text = string.Format(k_BoardSizeTxt, m_BoardSize);
        }

        private void choosenNumOfPlayers_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
        }
    }
}
