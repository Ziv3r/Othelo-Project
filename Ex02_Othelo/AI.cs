namespace Ex02_Othelo
{
    using System;

    public class AI
    {
        private readonly char m_Sign;
        private int m_Score = 2;

        public AI(char i_Sign)
        {
            m_Sign = i_Sign;
        }

        public int Score
        {
            get { return m_Score; }
            set { m_Score = value; }
        }

        public Cell ComputerMove(Board i_Board)
        {
            Cell bestMove = null;
            int maxVal = int.MinValue;
            int score1 = 0, score2 = 0;
            foreach (Cell option in i_Board.Optionals2)
            {
                Board child = i_Board.Clone();
                child.TryUpdateMatrix(option, 1);
                child.GetScores(out score1, out score2);
                int valOfMove = minMax(false, 5, child, score1, score2);

                if (valOfMove > maxVal)
                {
                    maxVal = valOfMove;
                    bestMove = option;
                }
            }
      
            return bestMove;
        }

        private int getCornersHeuristic(Board i_Board)
        {
            int res = 0;
            int edge = i_Board.Size - 1;
            char[] corners =
                {
                i_Board.Matrix[0, edge],
                i_Board.Matrix[0, 0],
                i_Board.Matrix[edge, edge],
                i_Board.Matrix[edge, 0]
            };

            foreach(char corner in corners)
            {
                if(corner == m_Sign)
                {
                    res += 20;
                }
            }

            return res;
        }

        private int heuristic(int i_Score1, int i_Score2, Board i_Board)
        {
            int res = 0;
            res += getCornersHeuristic(i_Board);
            return res + i_Score2 - i_Score1;
        }

        private int minMax(bool i_isComputer, int i_depth, Board i_Board, int i_Score1, int i_Score2)
        {
            if (i_depth == 0 || i_Board.Optionals2.Count.Equals(0))
            {
                return heuristic(i_Score1, i_Score2, i_Board);
            }

            int bestVal = int.MaxValue;
            if (!i_isComputer)
            {
                foreach (Cell option in i_Board.Optionals1)
                {
                    Board child = i_Board.Clone();
                    child.TryUpdateMatrix(option, 0);
                    child.GetScores(out i_Score1, out i_Score2);
                    bestVal = Math.Min(bestVal, minMax(!i_isComputer, i_depth - 1, child, i_Score1, i_Score2));
                }
            }
            else
            {
                bestVal = int.MinValue;

                foreach (Cell option in i_Board.Optionals2)
                {
                    Board child = i_Board.Clone();
                    child.TryUpdateMatrix(option, 1);

                    child.GetScores(out i_Score1, out i_Score2);
                    bestVal = Math.Max(bestVal, minMax(!i_isComputer, i_depth - 1, child, i_Score1, i_Score2));
                }
            }

            return bestVal;
        }
    }
}
