using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using View;

namespace Controller
{
    class Controller
    {
        private Ex02_Othelo.Game game = new Ex02_Othelo.Game();
        private UI m_UI = new UI();
        private bool m_GameRunning = true;

        public void GameLoop()
        {
            initalizeButtonsEvents();

            int count = 0;
            while (m_GameRunning)
            {
                m_UI.m_SettingsForm.ShowDialog();
                while (game.HasOptionsToPlay())
                {
                    m_UI.m_Board.UpdateBoard(game.getOptionals(),game.GetLogicMatrix(), game.CurrentPlayer);
                    m_UI.m_Board.ShowDialog();
                    if (count == 3)
                    {
                        break;
                    }
                    else
                        count++;
                }
                m_GameRunning = false;
                m_UI.m_ExitOrContinue.ShowDialog();
            }
        }

        private void initalizeButtonsEvents()
        {
            m_UI.m_SettingsForm.OnePlayerBtn.Click += new EventHandler(SetGamePlayers);
            m_UI.m_SettingsForm.TwoPlayersBtn.Click += new EventHandler(SetGamePlayers);
            m_UI.m_Board.onClick += HandelButtonClicked;
        }

        public void HandelButtonClicked(Point p)
        {
            game.TryUpdateLogicMatrix(p);
            //if computer is playing so show dialog choose cell ? 
        }

        public void SetGamePlayers(object sender, EventArgs e)
        {
            if ((sender as Button).Name == "OnePlayerBtn")
            {
                game.IsComputerPlaying = true;
            }
            else
            {
                game.IsComputerPlaying = false;
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
