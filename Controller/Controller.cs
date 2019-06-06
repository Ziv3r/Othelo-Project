using System;
using System.Collections.Generic;
using System.Text;

using View;

namespace Controller
{
    class Controller
    {
        private Ex02_Othelo.Game game = new Ex02_Othelo.Game();
        private UI m_UI = new UI();
        public void GameLoop()
        {

            m_UI.m_SettingsForm.OnePlayerBtn.Click += new EventHandler(SetGameWithOnePlayer);
            m_UI.m_SettingsForm.TwoPlayersBtn.Click += new EventHandler(SetGameWithTwoPlayers);

            m_UI.m_SettingsForm.ShowDialog();

            m_UI.m_Board.ShowDialog();

        }

        public void SetGameWithOnePlayer(object sender, EventArgs e)
        {
            game.IsComputerPlaying = true;
            startGame();
        }

        public void SetGameWithTwoPlayers(object sender, EventArgs e)
        {
            game.IsComputerPlaying = false;
            startGame();
        }

        private void startGame()
        {
            game.Start(m_UI.m_SettingsForm.BoardSize);
            m_UI.m_Board.AddButtons(m_UI.m_SettingsForm.BoardSize);
        }
    }
}
