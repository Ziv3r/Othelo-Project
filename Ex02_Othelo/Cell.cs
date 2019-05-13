using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    public class Cell
    {
        private int m_X;
        private int m_Y;

        public Cell(int i_X , int i_Y)
        {
            m_X = i_X;
            m_Y = i_Y; 
        }
        public int X
        {
            get { return m_X; }
            set { m_X = value; }
        }

        public int Y
        {
            get { return m_Y; }
            set { m_Y = value; }
        }
    }
}
