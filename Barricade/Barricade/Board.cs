using AStarAlgorithm;
using System;

namespace Barricade
{
    internal class Board
    {
        private readonly Grid grid;

        public Board(Grid grid)
        {
            this.grid = grid;
        }

        public void DisplayBoard(Player player1, Player player2, bool[,] horizontal, bool[,] vertical)
        {
            ConsoleColor defaultColour = Console.ForegroundColor;
            ConsoleColor grey = ConsoleColor.DarkGray;
            Console.ForegroundColor = grey;
            int boardRows = grid.Rows * 2 + 1;
            int boardCols = grid.Cols * 2 + 1;

            for (int r = 0; r < boardRows; r++)
            {
                for (int c = 0; c < boardCols; c++)
                {
                    //Nodes:
                    if (r % 2 == 1 && c % 2 == 1)
                    {
                        int gridRow = r / 2;
                        int gridCol = c / 2;

                        Node node = grid.GetNode(gridRow, gridCol);

                        if (node.Row == player1.Position.Row && node.Col == player1.Position.Col)
                        {
                            Console.ForegroundColor = player1.Colour;
                            Console.Write(player1.Symbol);
                            Console.ForegroundColor = grey;
                        }
                        else if (node.Row == player2.Position.Row && node.Col == player2.Position.Col)
                        {
                            Console.ForegroundColor = player2.Colour;
                            Console.Write(player2.Symbol);
                            Console.ForegroundColor = grey;
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
                        if (r > 0 && r < boardRows - 1)
                        {
                            if (horizontal[(r - 2) / 2, (c - 1) / 2])
                            {
                                Console.Write('|');
                            }
                            else
                            {
                                Console.Write(' ');
                            }
                        }
                        else
                        {
                            Console.Write(' ');
                        }
                    }
                    //Vertical walls:
                    else if (c % 2 == 0)
                    {
                        if (c > 0 && c < boardCols - 1)
                        {
                            if (vertical[(r - 1) / 2, (c - 2) / 2])
                            {
                                Console.Write('|');
                            }
                            else
                            {
                                Console.Write(' ');
                            }
                        }
                        else
                        {
                            Console.Write(' ');
                        }
                    }
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = defaultColour;
        }
    }
}