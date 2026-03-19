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
        }

        public void DisplayBoard(Player player1, Player player2)
        {
            for (int r = 0; r < grid.Rows * 2 + 1; r++)
            {
                for (int c = 0; c < grid.Cols * 2 + 1; c++)
                {
                    //Nodes:
                    if (r % 2 == 1 && c % 2 == 1)
                    {
                        int gridRow = r / 2;
                        int gridCol = c / 2;

                        Node node = grid.GetNode(gridRow, gridCol);

                        if (node.Row == player1.Position.Row &&
                            node.Col == player1.Position.Col)
                        {
                            Console.Write(player1.Symbol);
                        }
                        else if (node.Row == player2.Position.Row &&
                                 node.Col == player2.Position.Col)
                        {
                            Console.Write(player2.Symbol);
                        }
                        else
                        {
                            Console.Write(' ');
                        }
                    }
                    //Corners:
                    else if (r % 2 == 0 && c % 2 == 0)
                    {
                        Console.Write('+');
                    }
                    //Horizontal walls:
                    else if (r % 2 == 0)
                    {
                        Console.Write(' ');
                    }
                    //Vertical walls:
                    else if (c % 2 == 0)
                    {
                        Console.Write(' ');
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine($"Player at: {player1.Position.Row}, {player1.Position.Col}");
        }
    }
}