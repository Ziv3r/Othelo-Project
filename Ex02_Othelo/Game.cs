using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Drawing;

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
        private ComputerPlayer m_compPlayer = null;
        bool m_IsComputerPlaying = false;
        private string[] m_PlayersNames = {"First Player", "Second Player" };

        public void Start(int i_BoardSize)
        {
            alocatePlayers();
            m_Board.Size = i_BoardSize;
            m_Board.Init(m_Board.Size);
        }

        private void run()
        {
            bool startNewGame = true;
            bool firstChance = true;
            Point choosenPoint;
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

                        choosenPoint = getPointFromCureentPlayer(firstChance);
                        firstChance = false;

                        if (choosenPoint == new Point(-1, -1))
                        {
                            return;
                        }
                    }
                    while (!m_Board.TryUpdateMatrix(choosenPoint, m_CurrentPlayer));
                }

                if (m_IsComputerPlaying)
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

        public List<Point> getOptionals()
        {
            if(m_CurrentPlayer == 0)
            {
                return m_Board.Optionals1;
            }
            else
            {
                return m_Board.Optionals2;
            }
        }

        public bool HasOptionsToPlay()
        {
            return m_Board.HasOption();
        }

        public void  TryUpdateLogicMatrix(Point i_ChoosenPoint)
        {
            bool checkIfValidate = (m_Board.TryUpdateMatrix(i_ChoosenPoint, m_CurrentPlayer));
            m_CurrentPlayer = (m_CurrentPlayer * -1) + 1;
            if (m_IsComputerPlaying)
            {
                m_Board.TryUpdateMatrix(getPointFromCureentPlayer(true) , m_CurrentPlayer);
            }
            m_CurrentPlayer = (m_CurrentPlayer * -1) + 1;
        }

        private void alocatePlayers()
        {
            m_Player1.Name = m_PlayersNames[0];

            if (m_IsComputerPlaying)
            {
                m_compPlayer = new ComputerPlayer(k_FirstPlayerSign);
            }
            else
            {
                m_Player2 = new Player();
                m_Player2.Name = m_PlayersNames[1];
            }
        }

       public char[,] GetLogicMatrix()
        {
            return m_Board.Matrix;
        }

        public string CurrentPlayer
        {
            get { return m_PlayersNames[Math.Abs(m_CurrentPlayer-1)];}
        }

        public bool IsComputerPlaying
        {
            get { return m_IsComputerPlaying; }
            set
            {
                m_IsComputerPlaying = value;
            }
        }

        private Point getPointFromCureentPlayer(bool i_IsFirstChance)
        {
            return  m_compPlayer.ComputerMove(m_Board.Clone());
        }

        private void FillUpAndPrintMatrix()
        {
            updateScore();
            if (m_IsComputerPlaying)
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

            if (m_IsComputerPlaying)
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