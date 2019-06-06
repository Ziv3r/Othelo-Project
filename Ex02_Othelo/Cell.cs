using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    public class Cell
    {
        private int m_X;
        private int m_Y;

        public Cell(int i_X, int i_Y)
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

        public static bool operator ==(Cell c1, Cell c2)
        {
            return c1.X == c2.X && c1.Y == c2.Y;
        }

        public override bool Equals(object i_ToCheck)
        {
            Cell cellToCheck = i_ToCheck as Cell;
            return this == cellToCheck;
        }

        public static bool operator !=(Cell c1, Cell c2)
        {
            return !(c1.X == c2.X && c1.Y == c2.Y);
        }
    }
}