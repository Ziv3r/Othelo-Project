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
        Player m_Player1 = new Player();
        Player m_Player2 = null;
        ComputerPlayer m_compPlayer = null;


        public void Start()
        {
            int matrixSize;
            m_Players = m_UserInterface.GetGameData(out matrixSize);
            alocatePlayers();
            m_Board.Size = matrixSize;
            m_UserInterface.InitUI(m_Board.Size);
            m_Board.Init(m_Board.Size);
            //m_Board.PrintOptionals();         just for check - delete . 
            run();

        }
        private void run()
        {
            bool firstChanse = true;
            Cell choosenCell;
            while (m_Board.HasOption())
            {
                firstChanse = true;
                m_UserInterface.FillUpMatrixP(m_Board.Matrix);
                m_CurrentPlayer = m_CurrentPlayer * -1 + 1;
                {
                    do
                    {
                        if (!checkeNoOptions())
                        {
                            break;
                        }

                        if (m_Players[m_CurrentPlayer] == string.Empty)
                        {
                            choosenCell = m_compPlayer.ChooseCell(m_Board.Optionals2);
                        }
                        else
                        {
                            choosenCell = m_UserInterface.GetCellFromPlayer(m_Players[m_CurrentPlayer], firstChanse);
                        }
                        firstChanse = false;
                    } while (!m_Board.TryUpdateMatrix(choosenCell, m_CurrentPlayer));
                }
            }
            //m_UserInterface.checkWinner();
        }
        private void alocatePlayers()
        {
            m_Player1.Name = m_Players[0];

            if (m_Players[1] == string.Empty)
            {
                m_compPlayer = new ComputerPlayer();
            }
            else
            {
                m_Player2 = new Player();
                m_Player2.Name = m_Players[2];
            }
        }

        private bool checkeNoOptions()
        {
            bool isPossibleOption = true;

            if (m_CurrentPlayer == 0)      //to split to function .
            {
                //checkNoOptions(m_CurrentPlayer);
                if (m_Board.Optionals1.Count == 0)
                {
                    m_UserInterface.NoOptionsMessage(m_Players[m_CurrentPlayer]);
                    isPossibleOption = false;
                }
            }
            else
            {
                if (m_Board.Optionals2.Count == 0)
                {
                    m_UserInterface.NoOptionsMessage(m_Players[m_CurrentPlayer]);
                    isPossibleOption = false;
                }
            }
            return isPossibleOption;
        }
    }
}

//board.TryUpdateMatrix(new Cell(2, 3), 1);
