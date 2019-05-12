using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{

    class UI
    {

        int m_Width;
        int m_Height;

        private char[,] m_MatrixPrint;


        public UI(int i_Size)
        {
            if (i_Size == 6)
            {
                m_Width = 27;
                m_Height = 14;
            }
            m_MatrixPrint = new char[m_Height, m_Width];
        }
        public void PrintMatrixP()
        {
            int counter = 0;
            int countNumber = 0;

            for (int i = 0; i < m_Height; i++)
            {
                for (int j = 0; j < m_Width; j++)
                {
                    if (i %2 ==1 && j>1)
                    {
                        m_MatrixPrint[i, j] = '=';

                    }
                   else if (i == 0 && j % 4 == 0 && j!=0)
                    {
                        m_MatrixPrint[i,j] = (char)(65 + counter);
                        counter++;
                    }
                    else if(i!= 0 && i%2 ==0 && j==0)
                    {
                        m_MatrixPrint[i, j] = (char)('1'+countNumber);
                        countNumber++; 
                    }
                    else if (i%2 == 0 && j % 4 ==2 && i!= 0)
                    {
                        m_MatrixPrint[i, j] = '|';
                    }
                    else
                    {
                        m_MatrixPrint[i, j] = ' ';
                    }
                }

            }
            for (int i = 0; i < m_Height; i++)
            {
                for (int j = 0; j < m_Width; j++)
                {
                    Console.Write(m_MatrixPrint[i, j]);
                }
                Console.WriteLine();
            }
        }

        //public void ConvertMatrixLogicToMatrixPrint(char[,] i_MatrixLogic)
        //{
        //    for(int row= 0; row <i_MatrixLogic; row++)
        //    {

        //    }
        //}
    }
}
