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
        private UI m_UI;
        private Game m_Game;
        private bool m_GameRunning;

        public Controller()
        {
            m_UI = new UI();
            m_Game = new Game();
            m_GameRunning = true;
        }

        public void GameLoop()
        {
            int[] points = new int[] { 0, 0 };
            initalizeGame();
            m_UI.SettingForm.ShowDialog();

            while (m_GameRunning)
            {
                startGame();
                if (m_UI.BoardForm.ShowDialog() == DialogResult.Cancel)
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

            m_UI.BoardForm.AddButtons(m_UI.SettingForm.BoardSize);

            startGame();
        }

        private void startGame()
        {
            m_Game.Start(m_UI.SettingForm.BoardSize);
            m_UI.BoardForm.UpdateBoard(m_Game.getOptionals(), m_Game.GetLogicMatrix(), m_Game.CurrentPlayer);
        }

        private void initalizeGame()
        {
            m_UI.SettingForm.OnePlayerButton.Click += new EventHandler(SetGamePlayers);
            m_UI.SettingForm.TwoPlayersButton.Click += new EventHandler(SetGamePlayers);
            m_UI.BoardForm.OnButtonClick += HandelButtonClicked;
        }

        public void HandelButtonClicked(Point i_ClickedBtn)
        {
            m_Game.TryUpdateLogicMatrix(i_ClickedBtn);
            m_UI.BoardForm.UpdateBoard(m_Game.getOptionals(), m_Game.GetLogicMatrix(), m_Game.CurrentPlayer);
            if (!m_Game.HasOptionsToPlay())
            {
                m_UI.BoardForm.DialogResult = DialogResult.Abort;
            }
        }

        private void ExitOrContinueExecute(int[] i_Points)
        {
            int winnerScore;
            int loserScore;
            string winnerColor;

            getWinnerAndLoserPoints(out winnerScore, out loserScore, out winnerColor, i_Points);
            m_GameRunning = m_UI.ExitOrContinue(winnerScore, loserScore, winnerColor, i_Points);
        }

        private void getWinnerAndLoserPoints(out int i_WinnerScore, out int i_LoserScore, out string i_WinnerColor, int[] points)
        {
            bool isFirstWin = true;
            int firstPlayerScore = m_Game.getFirstPlayerScore();
            int SecondPlayerScore = m_Game.getSecondPlayerScore();
            isFirstWin = (firstPlayerScore > SecondPlayerScore) ? true : false;

            if (isFirstWin)
            {
                i_WinnerScore = firstPlayerScore;
                i_LoserScore = SecondPlayerScore;
                i_WinnerColor = "Red";
                points[0]++;
            }
            else
            {
                i_WinnerScore = SecondPlayerScore;
                i_LoserScore = firstPlayerScore;
                i_WinnerColor = "Yellow";
                points[1]++;
            }
        }
    }
}
