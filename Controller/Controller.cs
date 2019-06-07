using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using View;
using Ex02_Othelo;

namespace Controller
{
    internal class Controller
    {
        private Game m_Game = new Game();
        private UI m_UI = new UI();
        private bool m_GameRunning = true;

        public void GameLoop()
        {
            initalizeGame();
            int[] points = new int[] { 0, 0 };

            while (m_GameRunning)
            {
                m_UI.SettingForm.ShowDialog();
                if(m_UI.BoardForm.ShowDialog() == DialogResult.Cancel)
                {
                    break;
                }
                ExitOrContinueExecute(points);
            }
        }

        public void SetGamePlayers(object sender, EventArgs e)
        {
            if ((sender as Button).Name == "OnePlayerBtn")
            {
                m_Game.IsComputerPlaying = true;
            }
            else
            {
                m_Game.IsComputerPlaying = false;
            }

            startGame();
        }

        private void startGame()
        {
            m_Game.Start(m_UI.SettingForm.BoardSize);
            m_UI.BoardForm.AddButtons(m_UI.SettingForm.BoardSize);
            m_UI.BoardForm.UpdateBoard(m_Game.getOptionals(), m_Game.GetLogicMatrix(), m_Game.CurrentPlayer);
        }
        private void initalizeGame()
        {
            m_UI.SettingForm.OnePlayerButton.Click += new EventHandler(SetGamePlayers);
            m_UI.SettingForm.TwoPlayersButton.Click += new EventHandler(SetGamePlayers);
            m_UI.BoardForm.onClick += HandelButtonClicked;
        }

        public void HandelButtonClicked(Point p)
        {
            m_Game.TryUpdateLogicMatrix(p);
            m_UI.BoardForm.UpdateBoard(m_Game.getOptionals(), m_Game.GetLogicMatrix(), m_Game.CurrentPlayer);
            if (!m_Game.HasOptionsToPlay())
            {
                m_UI.BoardForm.Close();
            }
        }

        private void ExitOrContinueExecute(int[] points)
        {
            int winnerScore;
            int loserScore;
            string winnerColor;
            getWinnerAndLoserPoints(out winnerScore, out loserScore, out winnerColor, points);

            m_GameRunning = m_UI.ExitOrContinue(winnerScore, loserScore, winnerColor, points);
        }
        private void getWinnerAndLoserPoints(out int winnerScore, out int loserScore, out string winnerColor, int[] points)
        {
            bool isFirstWin = true;

            int firstPlayerScore = m_Game.getFirstPlayerScore();
            int SecondPlayerScore = m_Game.getSecondPlayerScore();
            isFirstWin = (firstPlayerScore > SecondPlayerScore) ? true : false;

            if (isFirstWin)
            {
                winnerScore = firstPlayerScore;
                loserScore = SecondPlayerScore;
                winnerColor = "Red";
                points[0]++;
            }
            else
            {
                winnerScore = SecondPlayerScore;
                loserScore = firstPlayerScore;
                winnerColor = "Yello";
                points[1]++;
            }
        }

    }
}
