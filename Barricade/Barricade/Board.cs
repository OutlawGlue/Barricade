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

        public void DisplayBoard(Player player1, Player player2, bool[,] horizontal, bool[,] vertical,
            bool wallMode = false, int prevRow = -1, int prevCol = -1, bool prevIsVert = false,
            bool isValidPreview = true)
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
                    bool isPreview = false;

                    if (wallMode)
                        if (prevIsVert)
                        {
                            //Vertical wall = 2 stacked walls:
                            isPreview =
                                (r == prevRow * 2 + 1 && c == prevCol * 2) ||
                                (r == prevRow * 2 + 3 && c == prevCol * 2);
                        }
                        else
                        {
                            //Horizontal wall = 2 side-by-side walls:
                            isPreview =
                                (r == prevRow * 2 && c == prevCol * 2 + 1) ||
                                (r == prevRow * 2 && c == prevCol * 2 + 3);
                        }
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
                        Console.Write('+');
                    //Horizontal walls:
                    else if (r % 2 == 0 && c % 2 == 1)
                        if (isPreview)
                        {
                            Console.ForegroundColor = isValidPreview ? ConsoleColor.Yellow : ConsoleColor.Red;
                            Console.Write('-');
                            Console.ForegroundColor = grey;
                        }
                        else if (r > 0 && r < boardRows - 1 && (c - 1) / 2 >= 0 && (c - 1) / 2 < horizontal.GetLength(1)
                            && r / 2 >= 0 && r / 2 < horizontal.GetLength(0) && horizontal[r / 2, (c - 1) / 2])
                        {
                            Console.Write('-');
                        }
                        else
                        {
                            Console.Write(' ');
                        }
                    //Vertical walls:
                    else if (c % 2 == 0 && r % 2 == 1)
                        if (isPreview)
                        {
                            Console.ForegroundColor = isValidPreview ? ConsoleColor.Yellow : ConsoleColor.Red;
                            Console.Write('|');
                            Console.ForegroundColor = grey;
                        }
                        else if (c > 0 && c < boardCols - 1 && (r - 1) / 2 >= 0 && (r - 1) / 2 < vertical.GetLength(0)
                            && c / 2 >= 0 && c / 2 < vertical.GetLength(1) && vertical[(r - 1) / 2, c / 2])
                        {
                            Console.Write('|');
                        }
                        else
                        {
                            Console.Write(' ');
                        }
                    Console.WriteLine();
                }

                Console.ForegroundColor = defaultColour;
            }
        }
    }
}