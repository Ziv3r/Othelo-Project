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
        private ComputerPlayer m_CompPlayer = null;
        private bool m_IsComputerPlaying = false;
        private string[] m_PlayersNames = { "Red", "Yellow" };

        public void Start(int i_BoardSize)
        {
            alocatePlayers();
            m_Board.Size = i_BoardSize;
            m_Board.Init(m_Board.Size);
        }

        public List<Point> getOptionals()
        {
            List<Point> toReturn;
            if (m_CurrentPlayer == 0)
            {
                toReturn = m_Board.Optionals1;
            }
            else
            {
                toReturn = m_Board.Optionals2;
            }

            return toReturn;
        }

        public bool HasOptionsToPlay()
        {
            return m_Board.HasOption();
        }

        public void TryUpdateLogicMatrix(Point i_ChoosenPoint)
        {
            bool checkIfValidate = m_Board.TryUpdateMatrix(i_ChoosenPoint, m_CurrentPlayer);
            caclCurrPlayer();

            if (m_IsComputerPlaying)
            {
                Point ComputerMove = getPointFromCurentPlayer(true);
                if (ComputerMove != Point.Empty)
                {
                    m_Board.TryUpdateMatrix(getPointFromCurentPlayer(true), m_CurrentPlayer);
                }

                caclCurrPlayer();
            }

            List<Point> op = m_CurrentPlayer == 0 ? m_Board.Optionals1 : m_Board.Optionals2;
            if (op.Count == 0)
            {
                caclCurrPlayer();
            }

            updateScore();
        }

        private void caclCurrPlayer()
        {
            m_CurrentPlayer = (m_CurrentPlayer * -1) + 1;
        }

        public int getFirstPlayerScore()
        {
            return m_Player1.Score;
        }

        public int getSecondPlayerScore()
        {
            if (m_IsComputerPlaying)
            {
                return m_CompPlayer.Score;
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
                m_CompPlayer = new ComputerPlayer(k_FirstPlayerSign);
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
            set { m_IsComputerPlaying = value; }
        }

        private Point getPointFromCurentPlayer(bool i_IsFirstChance)
        {
            return m_CompPlayer.ComputerMove(m_Board.Clone());
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
                m_CompPlayer.Score = score2;
            }
            else
            {
                m_Player2.Score = score2;
            }
        }
    }
}