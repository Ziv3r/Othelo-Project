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
            UI userInterface = new UI(6);

            userInterface.PrintMatrixP();

            Console.WriteLine();
        }
    }

}
