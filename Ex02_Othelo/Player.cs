using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    class Player
    {
        private string m_Name;
        private int m_Score=2 ;

        public  string Name
        {
            get { return m_Name; }
            set { m_Name = value;  }
        }

        public int Score
        {
            get { return m_Score; }
            set { m_Score = value; }
        }
    }
}
