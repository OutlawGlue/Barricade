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
                    {
                        if (prevIsVert)
                        {
                            //Vertical wall (2 tall):
                            isPreview =
                                (r == prevRow * 2 + 1 && c == prevCol * 2 + 2) ||
                                (r == prevRow * 2 + 3 && c == prevCol * 2 + 2);
                        }
                        else
                        {
                            //Horizontal wall (2 wide):
                            isPreview =
                                (r == prevRow * 2 + 2 && c == prevCol * 2 + 1) ||
                                (r == prevRow * 2 + 2 && c == prevCol * 2 + 3);
                        }
                    }

                    //Nodes:
                    if (r % 2 == 1 && c % 2 == 1)
                    {
                        int gridRow = r / 2;
                        int gridCol = c / 2;

                        //Compare grid coordinates directly against player position:
                        if (gridRow == player1.Row && gridCol == player1.Col)
                        {
                            Console.ForegroundColor = player1.Colour;
                            Console.Write(player1.Symbol);
                            Console.ForegroundColor = grey;
                        }
                        else if (gridRow == player2.Row && gridCol == player2.Col)
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
                    {
                        if (isPreview)
                        {
                            Console.ForegroundColor = isValidPreview ? ConsoleColor.Yellow : ConsoleColor.Red;
                            Console.Write('-');
                            Console.ForegroundColor = grey;
                        }
                        else
                        {
                            int wallRow = (r - 2) / 2;
                            int wallCol = (c - 1) / 2;

                            if (wallRow >= 0 && wallRow < horizontal.GetLength(0) &&
                                wallCol >= 0 && wallCol < horizontal.GetLength(1) &&
                                horizontal[wallRow, wallCol])
                            {
                                Console.Write('-');
                            }
                            else
                            {
                                Console.Write(' ');
                            }
                        }
                    }
                    //Vertical walls:
                    else if (c % 2 == 0 && r % 2 == 1)
                    {
                        if (isPreview)
                        {
                            Console.ForegroundColor = isValidPreview ? ConsoleColor.Yellow : ConsoleColor.Red;
                            Console.Write('|');
                            Console.ForegroundColor = grey;
                        }
                        else
                        {
                            int wallRow = (r - 1) / 2;
                            int wallCol = (c - 2) / 2;

                            if (wallRow >= 0 && wallRow < vertical.GetLength(0) &&
                                wallCol >= 0 && wallCol < vertical.GetLength(1) &&
                                vertical[wallRow, wallCol])
                            {
                                Console.Write('|');
                            }
                            else
                            {
                                Console.Write(' ');
                            }
                        }
                    }
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = defaultColour;
        }
    }
}