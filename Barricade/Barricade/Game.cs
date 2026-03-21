using AStarAlgorithm;
using System;
using System.ComponentModel;
using System.Data;

namespace Barricade
{
    internal class Game
    {
        private Grid grid;
        private Board board;
        private bool[,] horizontalWalls;
        private bool[,] verticalWalls;
        private Player player1;
        private Player player2;
        private bool gameWon = false;

        public Game(Grid grid, Board board, Player player1, Player player2)
        {
            this.grid = grid;
            this.board = board;
            this.player1 = player1;
            this.player2 = player2;

            int rows = grid.Rows;
            int cols = grid.Cols;
            horizontalWalls = new bool[rows - 1, cols];
            verticalWalls = new bool[rows, cols - 1];

            RunGame();
        }

        private void RunGame()
        {
            Console.Clear();

            Player currentPlayer = player1;
            Player opponentPlayer = player2;

            while (!gameWon)
            {
                board.DisplayBoard(player1, player2, horizontalWalls, verticalWalls);

                currentPlayer.Move(AskMove(currentPlayer, opponentPlayer));
                Console.Clear();
                if (CheckWin(currentPlayer))
                {
                    board.DisplayBoard(player1, player2, horizontalWalls, verticalWalls);
                    gameWon = true;
                    Console.WriteLine($"{currentPlayer.Name} wins!");
                }
                else
                {
                    opponentPlayer = currentPlayer;
                    currentPlayer = currentPlayer == player1 ? player2 : player1;
                }
            }
        }

        private int[] AskMove(Player current, Player opponent)
        {
            bool valid = false;
            int[] movement = new int[2];

            while (!valid)
            {
                Console.WriteLine($"{current.Name} please make your move.");
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Spacebar)
                {
                    //Wall mode:
                }

                movement[0] = 0; movement[1] = 0;
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        movement[0] = -1;
                        break;

                    case ConsoleKey.DownArrow:
                        movement[0] = 1;
                        break;

                    case ConsoleKey.LeftArrow:
                        movement[1] = -1;
                        break;

                    case ConsoleKey.RightArrow:
                        movement[1] = 1;
                        break;

                    case ConsoleKey.Spacebar:
                        WallMode(current);
                        break;

                    default: continue;
                }

                if (ValidateMove(current, ref movement, opponent))
                {
                    valid = true;
                }
                else
                {
                    Console.WriteLine("Invalid move.");
                }
            }

            return movement;
        }

        private bool ValidateMove(Player current, ref int[] movement, Player opponent)
        {
            int newRow = current.Position.Row + movement[0];
            int newCol = current.Position.Col + movement[1];

            //Check bounds:
            if (newRow < 0 || newRow >= grid.Rows || newCol < 0 || newCol >= grid.Cols)
                return false;

            //Check if into opponent:
            if (newRow == opponent.Position.Row && newCol == opponent.Position.Col)
            {
                int jumpRow = current.Position.Row + movement[0] * 2;
                int jumpCol = current.Position.Col + movement[1] * 2;

                //Check jump within bounds:
                if (jumpRow < 0 || jumpRow >= grid.Rows || jumpCol < 0 || jumpCol >= grid.Cols)
                    return false;

                //Apply jump:
                movement[0] *= 2;
                movement[1] *= 2;
            }

            return true;
        }

        private bool CheckWin(Player current)
        {
            if (current.Position.Row == current.TargetRow)
                return true;
            else
                return false;
        }

        private void WallMode(Player current)
        {
            int prevRow = 4;
            int prevCol = 4;
            bool prevIsVert = false;

            do
            {
                Console.Clear();

                board.DisplayBoard(player1, player2, horizontalWalls, verticalWalls, true, prevRow, prevCol, prevIsVert);
            } while (!AskWall(ref prevRow, ref prevCol, ref prevIsVert));
        }

        private bool AskWall(ref int prevRow, ref int prevCol, ref bool prevIsVert)
        {
            Console.WriteLine("Wall controls: move - arrow keys, rotate - r, place - enter.");

            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    prevRow -= 1;
                    break;

                case ConsoleKey.DownArrow:
                    prevRow += 1;
                    break;

                case ConsoleKey.LeftArrow:
                    prevCol -= 1;
                    break;

                case ConsoleKey.RightArrow:
                    prevCol += 1;
                    break;

                case ConsoleKey.R:
                    prevIsVert = !prevIsVert;
                    break;

                case ConsoleKey.Enter:
                    return true;

                default: break;
            }

            //VALIDATE LATER
            //if (ValidateMove())
            //{
            //    valid = true;
            //}
            //else
            //{
            //    Console.WriteLine("Invalid placement.");
            //}

            return false;
        }
    }
}