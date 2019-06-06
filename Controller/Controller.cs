using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using View;

namespace Controller
{
    class Controller
    {
        private Ex02_Othelo.Game game = new Ex02_Othelo.Game();
        private UI m_UI = new UI();
        public void GameLoop()
        {
            m_UI.m_SettingsForm.OnePlayerBtn.Click += new EventHandler(setGamePlayers);
            m_UI.m_SettingsForm.TwoPlayersBtn.Click += new EventHandler(setGamePlayers);

            m_UI.m_SettingsForm.ShowDialog();

            m_UI.m_Board.ShowDialog();

        }

        public void setGamePlayers(object sender, EventArgs e)
        {
            if((sender as Button).Name == "OnePlayerBtn")
            {
                game.IsComputerPlaying = true;
            }
            else
            {
                game.IsComputerPlaying = true;
            }
            startGame();
        }

        private void startGame()
        {
            game.Start(m_UI.m_SettingsForm.BoardSize);
            m_UI.m_Board.AddButtons(m_UI.m_SettingsForm.BoardSize);
        }

    }
}
