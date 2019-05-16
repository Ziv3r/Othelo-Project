using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Ex02_Othelo
{
    public class Game
    {
        private const char k_FirstPlayerSign = 'X';
        private const char k_SecPlayerSign = 'O';
        private int m_CurrentPlayer = 0;
        private Board m_Board = new Board();
        private UI m_UserInterface = new UI();
        private Player m_Player1 = new Player();
        private Player m_Player2 = null;
        private AI m_compPlayer = null;
        private string[] m_PlayersNames;

        public void Start()
        {
            int matrixSize;
            m_PlayersNames = m_UserInterface.GetGameData(out matrixSize);
            alocatePlayers();
            m_Board.Size = matrixSize;
            m_UserInterface.InitUI(m_Board.Size);
            m_Board.Init(m_Board.Size);
            run();
        }

        private void run()
        {
            bool startNewGame = true;
            bool firstChance = true;
            Cell choosenCell;
            while (startNewGame)
            {
                while (m_Board.HasOption())
                {
                    firstChance = true;
                    FillUpAndPrintMatrix();
                    m_CurrentPlayer = (m_CurrentPlayer * -1) + 1;         ////switch players 0=>1 ,1=>0
                    do
                    {
                        if (!checkeOptionsForPlayer())
                        {
                            m_UserInterface.NoOptionsMessage(m_PlayersNames[m_CurrentPlayer]);
                            break;
                        }

                        choosenCell = getCellFromCureentPlayer(firstChance);
                        firstChance = false;

                        if (choosenCell == new Cell(-1, -1))
                        {
                            return;
                        }
                    }
                    while (!m_Board.TryUpdateMatrix(choosenCell, m_CurrentPlayer));
                }

                if (IsComputerPlaying())
                {
                    startNewGame = m_UserInterface.GameFinished(m_PlayersNames, m_Player1.Score, m_compPlayer.Score);
                }
                else
                {
                    startNewGame = m_UserInterface.GameFinished(m_PlayersNames, m_Player1.Score, m_Player2.Score);
                }

                if (startNewGame)
                {
                    m_UserInterface.InitUI(m_Board.Size);
                    m_Board.Init(m_Board.Size);
                }
            }
        }

        private void alocatePlayers()
        {
            m_Player1.Name = m_PlayersNames[0];

            if (IsComputerPlaying())
            {
                m_compPlayer = new AI(k_FirstPlayerSign);
            }
            else
            {
                m_Player2 = new Player();
                m_Player2.Name = m_PlayersNames[1];
            }
        }

        private bool IsComputerPlaying()
        {
            return m_PlayersNames[1] == string.Empty;
        }

        private Cell getCellFromCureentPlayer(bool i_IsFirstChance)
        {
            Cell choosenCell = null;
            if (m_PlayersNames[m_CurrentPlayer] == string.Empty)
            {
                choosenCell = m_compPlayer.ComputerMove(m_Board.Clone());
            }
            else
            {
                choosenCell = m_UserInterface.GetCellFromPlayer(m_PlayersNames[m_CurrentPlayer], i_IsFirstChance);
            }

            return choosenCell;
        }

        private void FillUpAndPrintMatrix()
        {
            updateScore();
            if (IsComputerPlaying())
            {
                m_UserInterface.FillUpMatrixP(m_PlayersNames, m_Board.Matrix, m_Player1.Score, m_compPlayer.Score);
            }
            else
            {
                m_UserInterface.FillUpMatrixP(m_PlayersNames, m_Board.Matrix, m_Player1.Score, m_Player2.Score);
            }
        }

        private bool checkeOptionsForPlayer()
        {
            bool isPossibleOption = true;

            if (m_CurrentPlayer == 0)
            {
                if (m_Board.Optionals1.Count == 0)
                {
                    m_UserInterface.NoOptionsMessage(m_PlayersNames[m_CurrentPlayer]);
                    isPossibleOption = false;
                }
            }
            else
            {
                if (m_Board.Optionals2.Count == 0)
                {
                    m_UserInterface.NoOptionsMessage(m_PlayersNames[m_CurrentPlayer]);
                    isPossibleOption = false;
                }
            }

            return isPossibleOption;
        }

        private void updateScore()
        {
            int score1, score2;

            m_Board.GetScores(out score1, out score2);
            m_Player1.Score = score1;

            if (IsComputerPlaying())
            {
                m_compPlayer.Score = score2;
            }
            else
            {
                m_Player2.Score = score2;
            }
        }
    }
}