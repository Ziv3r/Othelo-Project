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
        private Player m_Player1 = new Player();
        private Player m_Player2 = null;
        private ComputerPlayer m_compPlayer = null;
        private bool m_IsComputerPlaying = false;
        private string[] m_PlayersNames = { "First Player", "Second Player" };

        public void Start(int i_BoardSize)
        {
            alocatePlayers();
            m_Board.Size = i_BoardSize;
            m_Board.Init(m_Board.Size);
        }

        public List<Point> getOptionals()
        {
            if (m_CurrentPlayer == 0)
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

        public void TryUpdateLogicMatrix(Point i_ChoosenPoint)
        {
            bool checkIfValidate = m_Board.TryUpdateMatrix(i_ChoosenPoint, m_CurrentPlayer);
            m_CurrentPlayer = (m_CurrentPlayer * -1) + 1;
            if (m_IsComputerPlaying)
            {
                Point ComputerMove = getPointFromCureentPlayer(true);
                if (ComputerMove != Point.Empty)
                {
                    m_Board.TryUpdateMatrix(getPointFromCureentPlayer(true), m_CurrentPlayer);
                }
                m_CurrentPlayer = (m_CurrentPlayer * -1) + 1;
            }
            updateScore();
        }

        public int getFirstPlayerScore()
        {
            return m_Player1.Score;
        }
        public int getSecondPlayerScore()
        {
            if (m_IsComputerPlaying)
            {
                return m_compPlayer.Score;
            }
            else
            {
                return m_Player2.Score;
            }
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
            get { return m_PlayersNames[Math.Abs(m_CurrentPlayer)]; }
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
            return m_compPlayer.ComputerMove(m_Board.Clone());
        }

        private bool checkeOptionsForPlayer()
        {
            bool isPossibleOption = true;

            if (m_CurrentPlayer == 0)
            {
                if (m_Board.Optionals1.Count == 0)
                {
                    isPossibleOption = false;
                }
            }
            else
            {
                if (m_Board.Optionals2.Count == 0)
                {
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