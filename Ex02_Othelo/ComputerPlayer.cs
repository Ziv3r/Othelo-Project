using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    class ComputerPlayer
    {
        int m_Score =2 ; 

        public Cell ChooseCell(List<Cell> Optionals)
        {
            int numberOfOptionals = Optionals.Count;
            Random rnd = new Random();
            int indexChoosenCell = rnd.Next(0, numberOfOptionals-1);

            return Optionals[indexChoosenCell];

        }
        public int Score
        {
            get { return m_Score; }
            set { m_Score = value; }
        }
    }
}
