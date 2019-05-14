using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    class Game
    {
        int m_CurrentPlayer = 1;
        Board m_Board = new Board();
        UI m_UserInterface = new UI();
        String[] m_Players = new String[2];

        public void Start()
        {
            int matrixSize;
            m_Players = m_UserInterface.GetGameData(out matrixSize);
            m_Board.Size = matrixSize;
            m_UserInterface.InitUI(m_Board.Size);
            m_Board.Init(m_Board.Size);
            m_Board.PrintOptionals();
            run();

        }
        private void run()
        {
            Cell choosenCell;
            while (m_Board.HasOption())
            {
                m_UserInterface.FillUpMatrixP(m_Board.Matrix);
                m_CurrentPlayer = m_CurrentPlayer * -1 + 1;
                {
                    do
                    {
                        choosenCell = m_UserInterface.GetCellFromPlayer(m_Players[m_CurrentPlayer]);
                    } while (!m_Board.TryUpdateMatrix(choosenCell, m_CurrentPlayer));
                }
            }
        }
    }
}
//board.TryUpdateMatrix(new Cell(2, 3), 1);
