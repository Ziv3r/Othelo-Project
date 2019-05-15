using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Ex02_Othelo
{
    class Game
    {
        int m_CurrentPlayer = 1;
        Board m_Board = new Board();
        UI m_UserInterface = new UI();
        String[] m_PlayersNames = new String[2];
        Player m_Player1 = new Player();
        Player m_Player2 = null;
        ComputerPlayer m_compPlayer = null;


        public void Start()
        {
            int matrixSize;
            m_PlayersNames = m_UserInterface.GetGameData(out matrixSize);
            alocatePlayers();
            m_Board.Size = matrixSize;
            m_UserInterface.InitUI(m_Board.Size);
            m_Board.Init(m_Board.Size);
            run();            ///change run to bool . 
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
                    updateScore();
                    m_UserInterface.FillUpMatrixP(m_PlayersNames, m_Board.Matrix,m_Player1.Score ,m_Player2.Score); // if computer!!!!!!!
                    m_CurrentPlayer = m_CurrentPlayer * -1 + 1;         ////switch players 0=>1 ,1=>0
                    {
                        do
                        {
                            if (!checkeNoOptionsForPlayer())
                            {
                                m_UserInterface.NoOptionsMessage(m_PlayersNames[m_CurrentPlayer]);
                                System.Threading.Thread.Sleep(3000);
                                break;
                            }

                            if (m_PlayersNames[m_CurrentPlayer] == string.Empty)
                            {
                                m_UserInterface.NoOptionsMessage(m_PlayersNames[m_CurrentPlayer]);
                                choosenCell = m_compPlayer.ChooseCell(m_Board.Optionals2);
                            }

                            else
                            {
                                choosenCell = m_UserInterface.GetCellFromPlayer(m_PlayersNames[m_CurrentPlayer], firstChance);
                            }

                            firstChance = false;
                            if (choosenCell == new Cell(-1, -1))
                            {
                                return;
                            }
                        } while (!m_Board.TryUpdateMatrix(choosenCell, m_CurrentPlayer));
                    }
                }

                updateScore();
                if (m_PlayersNames[1] == string.Empty)
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

            if (m_PlayersNames[1] == string.Empty)
            {
                m_compPlayer = new ComputerPlayer();
            }
            else
            {
                m_Player2 = new Player();
                m_Player2.Name = m_PlayersNames[1];
            }
        }

        private bool checkeNoOptionsForPlayer()
        {
            bool isPossibleOption = true;

            if (m_CurrentPlayer == 0)      //to split to function .
            {
                //checkNoOptions(m_CurrentPlayer);
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

            if (m_PlayersNames[1] == string.Empty)
            {
                m_Player1.Score = score1;
                m_compPlayer.Score = score2;
            }
            else
            {
                m_Player1.Score = score1;
                m_Player2.Score = score2;
            }
        }
    }
}

//board.TryUpdateMatrix(new Cell(2, 3), 1);
