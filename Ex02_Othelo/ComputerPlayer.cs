using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Ex02_Othelo
{
    public class ComputerPlayer
    {
        private readonly char m_Sign;
        private int m_Score = 2;
        private AI m_AiMoves = new AI();

        public ComputerPlayer(char i_FirstPlayerSign)
        {
            m_Sign = i_FirstPlayerSign;
        }

        public int Score
        {
            get { return m_Score; }
            set { m_Score = value; }
        }
       
        public Point ComputerMove(Board i_Board)
        {
           return m_AiMoves.AIMove(i_Board, m_Sign);
        }
    }
}
