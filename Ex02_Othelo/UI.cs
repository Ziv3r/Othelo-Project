using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    class UI
    {
        private const char k_FirstPlayerSign = 'X';
        private const char k_SecPlayerSign = 'O';

        private int m_Width;
        private int m_Height;
        private int m_SizeOfLogicMatrix;
        private char[,] m_MatrixPrint;

        public void InitUI(int i_Size = 6)
        {
            if (i_Size == 6)
            {
                m_SizeOfLogicMatrix = 6;
                m_Width = 27;
                m_Height = 14;
            }
            else
            {
                m_SizeOfLogicMatrix = 8;
                m_Width = 35;
                m_Height = 18;
            }
            m_MatrixPrint = new char[m_Height, m_Width];
        }

        public Cell GetCellFromPlayer(string i_PlayerName, bool i_FirstTry)
        {
            string input;
            int row = -1;
            int col = -1;
            do
            {
                Console.WriteLine("{0} choose {1}cell:", i_PlayerName, !i_FirstTry ? "valid " : "");
                input = Console.ReadLine();
                if (input.Length < 2)
                {
                    col = -1;
                    if (char.ToUpper(input[0]) == 'Q')
                    {
                        row = -1;
                        break;
                    }
                }
                else
                {
                    col = char.ToUpper(input[1]) - 'A';
                }
                i_FirstTry = false;
            } while (!int.TryParse(input[0].ToString(), out row) || !isInBoard(--row) || !isInBoard(col));

            return new Cell(row, col);
        }

        private bool isInBoard(int i_Num)
        {
            return i_Num >= 0 && i_Num < m_SizeOfLogicMatrix;
        }

        public string[] GetGameData(out int o_Size)
        {
            string[] names = new string[2];

            Console.WriteLine("Enter number of players: ");
            string numAsString = Console.ReadLine();
            int numOfPlayers = 0;
            while (!int.TryParse(numAsString, out numOfPlayers) || !isNumOfPlayersValid(numOfPlayers))
            {
                Console.WriteLine("Please Enter 1 or 2:");
                numAsString = Console.ReadLine();
            }

            names[0] = getValidName("first");
            if (numOfPlayers == 2)
            {
                names[1] = getValidName("second");
            }
            else
            {
                names[1] = "";
            }

            o_Size = getBoardSize();

            return names;
        }

        private string getValidName(string i_Player)
        {
            Console.WriteLine("Enter {0} player name:", i_Player);
            string name = Console.ReadLine();
            while (name.Length < 2)
            {
                Console.WriteLine("Name must contain 2 or more letters");
                name = Console.ReadLine();
            }

            return name;
        }

        public bool GameFinished(string[] i_Names, int i_FirstPlayerScore, int i_SecScore)
        {
            string secondPlayer = String.IsNullOrEmpty(i_Names[1]) ? "Computer" : i_Names[1];

            Console.WriteLine("{0} covered {1} cells and {2} covered {3} cells!", i_Names[0], i_FirstPlayerScore, secondPlayer, i_SecScore);

            if (i_FirstPlayerScore > i_SecScore)
            {
                Console.WriteLine("{0} won!", i_Names[0]);
            }
            else if (i_SecScore > i_FirstPlayerScore)
            {
                Console.WriteLine("{0} won", secondPlayer);
            }
            else
            {
                Console.WriteLine("No one won!");
            }

            Console.WriteLine("Would you like to play again?\ny for yes q to exit");
            string toContinue = Console.ReadLine();
            while (toContinue.ToLower() != "y" && toContinue.ToLower() != "q")
            {
                Console.WriteLine("y for yes q to exit");
                toContinue = Console.ReadLine();
            }

            return toContinue == "y";
        }

        public void NoOptionsMessage(string i_Name)
        {
            if(i_Name != "")
            {
                Console.WriteLine("{0} have no move to make", i_Name);
                System.Threading.Thread.Sleep(3000);
            }
        }

        private int getBoardSize()
        {
            int boardSize = 0;
            Console.WriteLine("Choose Board size (type 8 for 8x8 and 6 for 6x6):");
            string boardSizeString = Console.ReadLine();

            while (!int.TryParse(boardSizeString, out boardSize) || !isBoardSizeValid(boardSize))
            {
                Console.WriteLine("Please choose 6 or 8:");
                boardSizeString = Console.ReadLine();
            }

            return boardSize;
        }

        private bool isBoardSizeValid(int i_Size)
        {
            return i_Size == 6 || i_Size == 8;
        }
        private bool isNumOfPlayersValid(int i_NumOfPlayers)
        {
            return i_NumOfPlayers == 1 || i_NumOfPlayers == 2;
        }

        public void FillUpMatrixP(string[] PlayersNames, char[,] i_MatrixLogic, int i_FirstScore, int i_SecondScore)
        {

            int counter = 0;
            int countNumber = 0;

            Ex02.ConsoleUtils.Screen.Clear();
            for (int i = 0; i < m_Height; i++)
            {
                for (int j = 0; j < m_Width; j++)
                {
                    if (i % 2 == 1 && j > 1)
                    {
                        m_MatrixPrint[i, j] = '=';

                    }
                    else if (i == 0 && j % 4 == 0 && j != 0)
                    {
                        m_MatrixPrint[i, j] = (char)(65 + counter);
                        counter++;
                    }
                    else if (i != 0 && i % 2 == 0 && j == 0)
                    {
                        m_MatrixPrint[i, j] = (char)('1' + countNumber);
                        countNumber++;
                    }
                    else if (i % 2 == 0 && j % 4 == 2 && i != 0)
                    {
                        m_MatrixPrint[i, j] = '|';
                    }
                    else
                    {
                        m_MatrixPrint[i, j] = ' ';
                    }
                }
            }
            convertMatrixLogicToMatrixPrint(i_MatrixLogic);
            printMatirxP(PlayersNames, i_FirstScore, i_SecondScore);
        }
        private void printMatirxP(string[] PlayersNames, int i_FirstScore, int i_SecondScore)
        {
            string secondPyaer = string.IsNullOrEmpty(PlayersNames[1]) ? "computer" : PlayersNames[1];

            Console.WriteLine("{0} {1} \t\t\t {2} {3}", PlayersNames[0], i_FirstScore, secondPyaer, i_SecondScore);

            for (int i = 0; i < m_Height; i++)
            {
                for (int j = 0; j < m_Width; j++)
                {
                    Console.Write(m_MatrixPrint[i, j]);
                }
                Console.WriteLine();
            }
        }
        private void convertMatrixLogicToMatrixPrint(char[,] i_MatrixLogic)
        {
            for (int row = 0; row < m_SizeOfLogicMatrix; row++)
            {
                for (int col = 0; col < m_SizeOfLogicMatrix; col++)
                {
                    if (i_MatrixLogic[row, col] == k_SecPlayerSign)
                    {
                        m_MatrixPrint[row * 2 + 2, col * 4 + 4] = k_SecPlayerSign;
                    }
                    else if (i_MatrixLogic[row, col] == k_FirstPlayerSign)
                    {
                        m_MatrixPrint[row * 2 + 2, col * 4 + 4] = k_FirstPlayerSign;
                    }
                }
            }
        }
    }
}
