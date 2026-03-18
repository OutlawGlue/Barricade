using AStarAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barricade
{
    internal class Board
    {
        private Grid grid;

        public Board(Grid grid)
        {
            this.grid = grid;

            DisplayBoard();
        }

        public void DisplayBoard()
        {
            for (int r = 0; r < grid.Rows * 2 + 1; r++)
            {
                for (int c = 0; c < grid.Cols * 2 + 1; c++)
                {
                    if (r % 2 == 0 && c % 2 == 0)
                        Console.Write('+');
                    else if (r % 2 == 0)
                        Console.Write(' '); //horizontal
                    else if (c % 2 == 0)
                        Console.Write(' '); //vertical
                    else
                        Console.Write(' '); //space
                }
                Console.WriteLine();
            }
        }
    }
}