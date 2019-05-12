using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    class Board
    {
        private int m_Size = 6;
        private char[,] m_Matrix;
        private Cell[] optional1;
        private Cell[] optional2;


        public void Init(int i_Size)
        {
            m_Size = i_Size; 
            m_Matrix = new char[i_Size, i_Size];
        }

        public bool TryUpdateMatrix(Cell i_ToUpdate, int i_CurrentPlayer)
        {
            bool isUpdateSuccess = false;

            char userSign = i_CurrentPlayer == 1 ? 'O' : 'X';

            if (i_CurrentPlayer == 1)
            {
                foreach (Cell currentCell in optional1)
                {
                    if (currentCell == i_ToUpdate)
                    {
                        m_Matrix[i_ToUpdate.X, i_ToUpdate.Y] = 'O';
                        isUpdateSuccess = true;
                    }
                }
            }
            else
            {
                foreach (Cell currentCell in optional2)
                {
                    if (currentCell == i_ToUpdate)
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
            //updateOptionals();
        }

        private void updateMatrix(Cell i_ToUpdate, char i_UserSign)
        {
            m_Matrix[i_ToUpdate.X, i_ToUpdate.Y] = i_UserSign;
            updateMatrixRec(i_ToUpdate, i_UserSign, 0,1);
            updateMatrixRec(i_ToUpdate, i_UserSign, 0, -1);
            updateMatrixRec(i_ToUpdate, i_UserSign, 1, 1);
            updateMatrixRec(i_ToUpdate, i_UserSign, 1, -1);
            updateMatrixRec(i_ToUpdate, i_UserSign, -1, 1);
            updateMatrixRec(i_ToUpdate, i_UserSign, -1, -1);
            updateMatrixRec(i_ToUpdate, i_UserSign, 1, 0);
            updateMatrixRec(i_ToUpdate, i_UserSign, -1, 0);

        }

        private bool updateMatrixRec(Cell i_ToUpdate, char i_UserSign, int i_DirX, int i_DirY)
        {
            char currentCell = m_Matrix[i_ToUpdate.X, i_ToUpdate.Y];

            if (currentCell == i_UserSign)
            {
                return true;
            }
            else if (currentCell == ' ' || isOutOfBound(i_ToUpdate))
            {
                return false;
            }
            Cell nexxtCell = new Cell(i_ToUpdate.X + i_DirX, i_ToUpdate.Y + i_DirY);

            bool res = updateMatrixRec(nexxtCell, i_UserSign, i_DirX, i_DirY);

            if (res)
            {
                m_Matrix[i_ToUpdate.X, i_ToUpdate.Y] = i_UserSign;
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
    }
}
