using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    class Program
    {
        public static void Main()
        {
            Game gamer = new Game();
            gamer.Start();
        }
    }

}

/*
 To-Do:
 1. correct message after mistake - not valid cell .                (done) . 
 2. message for no options for current player.                      (done) . 
 3. game finished - who wins and option to start a new game . 
 4. computer Player .                                                (done); .
 5. exit game with pressing Q .
 6.check validation for each input .                                  (done) . 
 7.separte to dlls .                                                  
 8. Ai .
 9.

    */