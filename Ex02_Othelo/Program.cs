using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    class Program
    {
        public static void Main()
        {
            Board board = new Board();
            board.Init(6);
            board.TryUpdateMatrix(new Cell(2,4),1);
            board.PrintMatrix();
            Console.ReadLine();
        }
    }

}
