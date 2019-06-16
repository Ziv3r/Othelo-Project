using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Ex02_Othelo
{
    public class Board
    {
        private const char k_FirstPlayerSign = 'X';
        private const char k_SecPlayerSign = 'O';
        private const char k_EmptyPointSign = ' ';
        private int m_Size;
        private char[,] m_Matrix;
        private List<Point> m_Optional1 = new List<Point>();
        private List<Point> m_Optional2 = new List<Point>();

        public Board Clone()
        {
            Board newBoard = new Board();
            newBoard.m_Size = m_Size;
            newBoard.m_Matrix = new char[m_Size, m_Size];

            for (int row = 0; row < m_Size; row++)
            {
                for (int col = 0; col < m_Size; col++)
                {
                    char currChar = m_Matrix[row, col];
                    newBoard.m_Matrix[row, col] = currChar;
                }
            }

            foreach (Point option in m_Optional1)
            {
                newBoard.m_Optional1.Add(new Point(option.X, option.Y));
            }

            foreach (Point option in m_Optional2)
            {
                newBoard.m_Optional2.Add(new Point(option.X, option.Y));
            }

            return newBoard;
        }

        public void Init(int i_Size)
        {
            m_Size = i_Size;
            m_Matrix = new char[m_Size, m_Size];

            for (int row = 0; row < m_Size; row++)
            {
                for (int col = 0; col < m_Size; col++)
                {
                    m_Matrix[row, col] = k_EmptyPointSign;
                }
            }

            addDefaults();
        }

        public char[,] Matrix
        {
            get { return m_Matrix; }
        }

        public int Size
        {
            get { return m_Size; }
            set { m_Size = value; }
        }

        public List<Point> Optionals1
        {
            get { return m_Optional1; }
        }

        public List<Point> Optionals2
        {
            get { return m_Optional2; }
        }

        private void addDefaults()
        {
            int[] defaultIdxForTopBlack = { 2, 2 };
            int[] defaultIdxForBottomBlack = { 3, 3 };
            int[] defaultIdxForTopWhite = { 2, 3 };
            int[] defaultIdxForBottomWhite = { 3, 2 };

            int toAddToDefault = (m_Size - 6) / 2; // formula for position first stones

            m_Matrix[defaultIdxForBottomBlack[0] + toAddToDefault, defaultIdxForBottomBlack[1] + toAddToDefault] =
                m_Matrix[defaultIdxForTopBlack[0] + toAddToDefault, defaultIdxForTopBlack[1] + toAddToDefault] = k_FirstPlayerSign;
            m_Matrix[defaultIdxForBottomWhite[0] + toAddToDefault, defaultIdxForBottomWhite[1] + toAddToDefault] =
                m_Matrix[defaultIdxForTopWhite[0] + toAddToDefault, defaultIdxForTopWhite[1] + toAddToDefault] = k_SecPlayerSign;

            updateOptionals();
        }

        public bool TryUpdateMatrix(Point i_ToUpdate, int i_CurrentPlayer)
        {
            bool isUpdateSuccess = true;
            char userSign = i_CurrentPlayer == 0 ? k_FirstPlayerSign : k_SecPlayerSign;

            if (isUpdateSuccess)
            {
                update(i_ToUpdate, userSign);
            }

            return isUpdateSuccess;
        }

        private bool validatePoint(List<Point> i_OptionsArr, Point i_ToCheck)
        {
            bool isValid = false;
            foreach (Point currentPoint in i_OptionsArr)
            {
                if (currentPoint == i_ToCheck)
                {
                    isValid = true;
                    break;
                }
            }

            return isValid;
        }

        public void GetScores(out int o_Score1, out int o_Score2)
        {
            o_Score1 = 0;
            o_Score2 = 0;

            for (int i = 0; i < m_Size; i++)
            {
                for (int j = 0; j < m_Size; j++)
                {
                    if (m_Matrix[i, j] == k_SecPlayerSign)
                    {
                        o_Score2++;
                    }
                    else if (m_Matrix[i, j] == k_FirstPlayerSign)
                    {
                        o_Score1++;
                    }
                }
            }
        }

        private void update(Point i_ToUpdate, char i_UserSign)
        {
            updateMatrix(i_ToUpdate, i_UserSign);
            updateOptionals();
        }

        private void updateMatrix(Point i_ToUpdate, char i_UserSign)
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

        private bool updateMatrixRec(Point i_ToUpdate, char i_UserSign, int i_DirX, int i_DirY)
        {
            Point nextPoint = new Point(i_ToUpdate.X + i_DirX, i_ToUpdate.Y + i_DirY);

            if (isOutOfBound(nextPoint))
            {
                return false;
            }

            char currentPoint = m_Matrix[i_ToUpdate.X + i_DirX, i_ToUpdate.Y + i_DirY];

            if (currentPoint == i_UserSign)
            {
                return true;
            }
            else if (currentPoint == k_EmptyPointSign)
            {
                return false;
            }

            bool res = updateMatrixRec(nextPoint, i_UserSign, i_DirX, i_DirY);

            if (res)
            {
                m_Matrix[i_ToUpdate.X + i_DirX, i_ToUpdate.Y + i_DirY] = i_UserSign;
            }

            return res;
        }

        private bool isOutOfBound(Point i_ToCheckIfOutOfBound)
        {
            bool outOfBound = false;

            if (i_ToCheckIfOutOfBound.X >= m_Size || i_ToCheckIfOutOfBound.X < 0)
            {
                outOfBound = true;
            }

            return outOfBound || (i_ToCheckIfOutOfBound.Y >= m_Size || i_ToCheckIfOutOfBound.Y < 0);
        }

        private void updateOptionals()
        {
            m_Optional1.Clear();
            m_Optional2.Clear();
            int counter = 0;

            for (int i = 0; i < m_Size; i++)
            {
                for (int j = 0; j < m_Size; j++)
                {
                    if (m_Matrix[i, j] != k_EmptyPointSign)
                    {
                        updateOptionalsRec(new Point(i + 0, j + 1), m_Matrix[i, j], 0, 1, counter);
                        updateOptionalsRec(new Point(i + 0, j - 1), m_Matrix[i, j], 0, -1, counter);
                        updateOptionalsRec(new Point(i + 1, j + 0), m_Matrix[i, j], 1, 0, counter);
                        updateOptionalsRec(new Point(i + 1, j - 1), m_Matrix[i, j], 1, -1, counter);
                        updateOptionalsRec(new Point(i + 1, j + 1), m_Matrix[i, j], 1, 1, counter);
                        updateOptionalsRec(new Point(i + -1, j + 1), m_Matrix[i, j], -1, 1, counter);
                        updateOptionalsRec(new Point(i - 1, j - 1), m_Matrix[i, j], -1, -1, counter);
                        updateOptionalsRec(new Point(i - 1, j + 0), m_Matrix[i, j], -1, 0, counter);
                    }
                }
            }
        }

        private void updateOptionalsRec(Point i_ToUpdate, char i_UserSign, int i_DirX, int i_DirY, int i_Counter)
        {
            if (isOutOfBound(i_ToUpdate))
            {
                return;
            }

            char currentPoint = m_Matrix[i_ToUpdate.X, i_ToUpdate.Y];
            if (currentPoint == i_UserSign)
            {
                return;
            }
            else if (currentPoint == k_EmptyPointSign)
            {
                if (i_Counter != 0)
                {
                    if (i_UserSign == k_FirstPlayerSign)
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
                Point nextPoint = new Point(i_ToUpdate.X + i_DirX, i_ToUpdate.Y + i_DirY);
                updateOptionalsRec(nextPoint, i_UserSign, i_DirX, i_DirY, i_Counter + 1);
            }
        }

        public bool HasOption()
        {
            return m_Optional1.Count != 0 || m_Optional2.Count != 0;
        }
    }
}
