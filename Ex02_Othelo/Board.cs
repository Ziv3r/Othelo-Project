using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    class Board
    {
        private int m_Size;
        private char[,] m_Matrix;
        List<Cell> m_Optional1 = new List<Cell>();
        List<Cell> m_Optional2 = new List<Cell>();

        public void Init(int i_Size)
        {
            m_Size = i_Size;
            m_Matrix = new char[i_Size, i_Size];

            for (int row = 0; row < i_Size; row++)
            {
                for (int col = 0; col < i_Size; col++)
                {
                    m_Matrix[row, col] = ' ';
                }
            }

            if (i_Size == 6)
            {
                m_Matrix[2, 2] = m_Matrix[3, 3] = 'O';
                m_Matrix[2, 3] = m_Matrix[3, 2] = 'X';
            }
            else
            {
                m_Matrix[3, 3] = m_Matrix[4, 4] = 'O';
                m_Matrix[3, 4] = m_Matrix[4, 3] = 'X';
            }

            m_Optional2.Add(new Cell(3, 4));
            m_Optional2.Add(new Cell(4, 3));
            m_Optional2.Add(new Cell(1, 2));
            m_Optional2.Add(new Cell(2, 1));
            m_Optional1.Add(new Cell(4, 2));
            m_Optional1.Add(new Cell(1, 3));
            m_Optional1.Add(new Cell(3, 1));
            m_Optional1.Add(new Cell(2, 4));
        }

        public bool TryUpdateMatrix(Cell i_ToUpdate, int i_CurrentPlayer)
        {
            bool isUpdateSuccess = false;
            if (m_Matrix[i_ToUpdate.X, i_ToUpdate.Y] != ' ')
            {
                return false;
            }

            char userSign = i_CurrentPlayer == 1 ? 'O' : 'X';

            if (i_CurrentPlayer == 1)
            {
                foreach (Cell currentCell in m_Optional1)
                {
                    if (currentCell.X == i_ToUpdate.X && currentCell.Y == i_ToUpdate.Y)
                    {
                        m_Matrix[i_ToUpdate.X, i_ToUpdate.Y] = 'O';
                        isUpdateSuccess = true;
                    }
                }
            }
            else
            {
                foreach (Cell currentCell in m_Optional2)
                {
                    if (currentCell.X == i_ToUpdate.X && currentCell.Y == i_ToUpdate.Y)
                    {
                        m_Matrix[i_ToUpdate.X, i_ToUpdate.Y] = 'X';
                        isUpdateSuccess = true;
                    }
                    isUpdateSuccess = true;
                }

            }
            if (isUpdateSuccess)
            {
                update(i_ToUpdate, userSign);
            }
            return isUpdateSuccess;
        }

        private void update(Cell i_ToUpdate, char i_UserSign)
        {
            updateMatrix(i_ToUpdate, i_UserSign);
            updateOptionals(i_ToUpdate, i_UserSign);
        }

        private void updateMatrix(Cell i_ToUpdate, char i_UserSign)
        {
            m_Matrix[i_ToUpdate.X, i_ToUpdate.Y] = i_UserSign;
            updateMatrixRec(i_ToUpdate, i_UserSign, 0, 1);
            updateMatrixRec(i_ToUpdate, i_UserSign, 0, -1);
            updateMatrixRec(i_ToUpdate, i_UserSign, 1, 1);
            updateMatrixRec(i_ToUpdate, i_UserSign, 1, -1);
            updateMatrixRec(i_ToUpdate, i_UserSign, -1, 1);
            updateMatrixRec(i_ToUpdate, i_UserSign, -1, -1);
            updateMatrixRec(i_ToUpdate, i_UserSign, 1, 0);
            updateMatrixRec(i_ToUpdate, i_UserSign, -1, 0);

        }

        // update each cell after eacg player play
        private bool updateMatrixRec(Cell i_ToUpdate, char i_UserSign, int i_DirX, int i_DirY)
        {
            char currentCell = m_Matrix[i_ToUpdate.X + i_DirX, i_ToUpdate.Y + i_DirY];

            if (currentCell == i_UserSign)
            {
                return true;
            }
            else if (currentCell == ' ' || isOutOfBound(i_ToUpdate))
            {
                return false;
            }

            Cell nextCell = new Cell(i_ToUpdate.X + i_DirX, i_ToUpdate.Y + i_DirY);

            bool res = updateMatrixRec(nextCell, i_UserSign, i_DirX, i_DirY);

            if (res)
            {
                m_Matrix[i_ToUpdate.X + i_DirX, i_ToUpdate.Y + i_DirY] = i_UserSign;
            }

            return res;
        }

        private bool isOutOfBound(Cell i_toCheckIfOutOfBound)
        {
            bool outOfBound = true;

            if (i_toCheckIfOutOfBound.X >= m_Size || i_toCheckIfOutOfBound.X < 0)
                outOfBound = false;

            return (outOfBound && (i_toCheckIfOutOfBound.Y >= m_Size || i_toCheckIfOutOfBound.Y < 0));
        }

        private void updateOptionals(Cell i_ToUpdate, char i_UserSign)
        {
            m_Optional1.Clear(); 
            m_Optional2.Clear();

            for (int i = 0; i < m_Size; i++)
            {
                for (int j = 0; j < m_Size; j++)
                {
                    if (m_Matrix[i, j] != ' ')
                    {
                        updateOptionalsRec(new Cell(i + 0, j + 1), m_Matrix[i, j], 0, 1, 0);
                        updateOptionalsRec(new Cell(i + 0, j - 1), m_Matrix[i, j], 0, -1, 0);
                        updateOptionalsRec(new Cell(i + 1, j + 0), m_Matrix[i, j], 1, 0, 0);
                        updateOptionalsRec(new Cell(i + 1, j - 1), m_Matrix[i, j], 1, -1, 0);
                        updateOptionalsRec(new Cell(i + 1, j + 1), m_Matrix[i, j], 1, 1, 0);
                        updateOptionalsRec(new Cell(i + -1, j + 1), m_Matrix[i, j], -1, 1, 0);
                        updateOptionalsRec(new Cell(i - 1, j - 1), m_Matrix[i, j], -1, -1, 0);
                        updateOptionalsRec(new Cell(i - 1, j + 0), m_Matrix[i, j], -1, 0, 0);
                    }
                }
            }
        }

        private void updateOptionalsRec(Cell i_ToUpdate, char i_UserSign, int i_DirX, int i_DirY, int i_Counter)
        {
            char currentCell = m_Matrix[i_ToUpdate.X, i_ToUpdate.Y];

            if (currentCell == i_UserSign)
            {
                return;
            }
            else if (currentCell == ' ')
            {
                if (i_Counter != 0)
                {
                    // add this cell to the optional list 
                    if (i_UserSign == 'O')
                    {
                        m_Optional1.Add(i_ToUpdate);
                    }
                    else
                    {
                        m_Optional2.Add(i_ToUpdate);
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                Cell nextCell = new Cell(i_ToUpdate.X + i_DirX, i_ToUpdate.Y + i_DirY);
                updateOptionalsRec(nextCell, i_UserSign, i_DirX, i_DirY, i_Counter + 1);
            }
        }

        public void PrintMatrix()
        {
            for (int i = 0; i < m_Size; i++)
            {
                for (int j = 0; j < m_Size; j++)
                {
                    Console.Write("{0} ", m_Matrix[i, j]);
                }
                Console.WriteLine();
            }
        }

        public void PrintOptionals()
        {
            foreach (Cell c1 in m_Optional1)
            {
                Console.Write("({0} , {1}) ", c1.X, c1.Y);
            }
            Console.WriteLine();
            foreach (Cell c1 in m_Optional2)
            {
                Console.Write("({0} , {1}) ", c1.X, c1.Y);
            }
            Console.WriteLine();


        }
    }
}
