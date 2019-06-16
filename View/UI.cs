using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace View
{
    public class UI
    {
        private SettingsForm m_SettingsForm = new SettingsForm();
        private BoardForm m_Board = new BoardForm();

        public SettingsForm SettingForm
        {
            get { return m_SettingsForm; }
        }

        public BoardForm BoardForm
        {
            get { return m_Board; }
        }

        public bool ExitOrContinue(int i_WinnerScore, int i_LoserScore, string i_WinnersColor, int[] i_GamesPoint)
        {
            bool result = false;
            string message = string.Format(
                @"{0} Won !! ({1}/{2}) ({3}/{4})
Would you like another round?",
                i_WinnersColor,
                i_WinnerScore,
                i_LoserScore,
                i_GamesPoint[0],
                i_GamesPoint[1]);
            if (MessageBox.Show(
                message,
                "Othello",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.ServiceNotification)
                == DialogResult.Yes)
            {
                result = true;
            }

            return result;
        }
    }
}
