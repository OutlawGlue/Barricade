using AStarAlgorithm;
using System;

namespace Barricade
{
    internal class Game
    {
        private readonly Grid grid;
        private readonly Board board;
        private readonly bool[,] horizontalWalls;
        private readonly bool[,] verticalWalls;
        private readonly Player player1;
        private readonly Player player2;
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
                        WallMode();
                        break;

                    default: continue;
                }

                if (ValidateMove(current, ref movement, opponent))
                    valid = true;
                else
                    Console.WriteLine("Invalid move.");
            }

            return movement;
        }

        private bool ValidateMove(Player current, ref int[] move, Player opponent)
        {
            int newRow = current.Position.Row + move[0];
            int newCol = current.Position.Col + move[1];

            //Check bounds:
            if (newRow < 0 || newRow >= grid.Rows || newCol < 0 || newCol >= grid.Cols)
                return false;

            //Check for walls:

            //Vertical movement:
            if (move[0] == -1) //UP
            {
                if (newRow >= 0 && horizontalWalls[newRow, newCol])
                    return false;
            }
            else if (move[0] == 1) //DOWN
            {
                if (newRow - 1 >= 0 && horizontalWalls[newRow - 1, newCol])
                    return false;
            }

            //Horizontal movement:
            else if (move[1] == -1) //LEFT
            {
                if (newCol >= 0 && verticalWalls[newRow, newCol])
                    return false;
            }
            else if (move[1] == 1) //RIGHT
            {
                if (newCol - 1 >= 0 && verticalWalls[newRow, newCol - 1])
                    return false;
            }

            //Check if moving into opponent:
            if (newRow == opponent.Position.Row && newCol == opponent.Position.Col)
            {
                int row = current.Position.Row;
                int col = current.Position.Col;

                int jumpRow = row + move[0] * 2;
                int jumpCol = col + move[1] * 2;

                //Check jump within bounds:
                if (jumpRow < 0 || jumpRow >= grid.Rows || jumpCol < 0 || jumpCol >= grid.Cols)
                    return false;

                //Check wall beside opponent (to fix wall jumping bug):

                //Moving up:
                if (move[0] == -1)
                {
                    if (horizontalWalls[row - 2, col])
                        return false;
                }
                //Moving down:
                else if (move[0] == 1)
                {
                    if (horizontalWalls[row + 1, col])
                        return false;
                }
                //Moving left:
                else if (move[1] == -1)
                {
                    if (verticalWalls[row, col - 2])
                        return false;
                }
                //Moving right:
                else if (move[1] == 1)
                {
                    if (verticalWalls[row, col + 1])
                        return false;
                }

                //Apply jump
                move[0] *= 2;
                move[1] *= 2;
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

        private void WallMode()
        {
            int prevRow = 4;
            int prevCol = 4;
            bool prevIsVert = false;

            while (true)
            {
                bool isValid = ValidateWall(prevRow, prevCol, prevIsVert);

                Console.Clear();
                board.DisplayBoard(
                    player1, player2,
                    horizontalWalls, verticalWalls,
                    true, prevRow, prevCol, prevIsVert,
                    isValid);

                bool wallMode = AskWall(ref prevRow, ref prevCol, ref prevIsVert);

                //Clamp value after preview moved:
                ClampPreview(ref prevRow, ref prevCol, prevIsVert);

                if (wallMode)
                {
                    if (!isValid)
                        continue;

                    //Wall placement:
                    if (prevIsVert)
                    {
                        verticalWalls[prevRow, prevCol] = true;
                        verticalWalls[prevRow + 1, prevCol] = true;
                    }
                    else
                    {
                        horizontalWalls[prevRow, prevCol] = true;
                        horizontalWalls[prevRow, prevCol + 1] = true;
                    }

                    break;
                }
            }
        }

        private bool AskWall(ref int prevRow, ref int prevCol, ref bool prevIsVert)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    prevRow--;
                    break;

                case ConsoleKey.DownArrow:
                    prevRow++;
                    break;

                case ConsoleKey.LeftArrow:
                    prevCol--;
                    break;

                case ConsoleKey.RightArrow:
                    prevCol++;
                    break;

                case ConsoleKey.R:
                    prevIsVert = !prevIsVert;
                    break;

                case ConsoleKey.Enter:
                    return true; //Attempt placement

                default:
                    break;
            }

            return false;
        }

        private void ClampPreview(ref int prevRow, ref int prevCol, bool isVert)
        {
            if (isVert)
            {
                //verticalWalls: [rows, cols - 1]
                if (prevRow < 0)
                    prevRow = 0;
                if (prevRow > verticalWalls.GetLength(0) - 2)
                    prevRow = verticalWalls.GetLength(0) - 2;

                if (prevCol < 0)
                    prevCol = 0;
                if (prevCol > verticalWalls.GetLength(1) - 1)
                    prevCol = verticalWalls.GetLength(1) - 1;
            }
            else
            {
                //horizontalWalls: [rows - 1, cols]
                if (prevRow < 0)
                    prevRow = 0;
                if (prevRow > horizontalWalls.GetLength(0) - 1)
                    prevRow = horizontalWalls.GetLength(0) - 1;

                if (prevCol < 0)
                    prevCol = 0;
                if (prevCol > horizontalWalls.GetLength(1) - 2)
                    prevCol = horizontalWalls.GetLength(1) - 2;
            }
        }

        private bool ValidateWall(int prevRow, int prevCol, bool prevIsVert)
        {
            if (prevIsVert)
            {
                int maxRow = verticalWalls.GetLength(0);
                int maxCol = verticalWalls.GetLength(1);

                if (prevRow < 0 || prevRow >= maxRow - 1 || prevCol < 0 || prevCol >= maxCol)
                    return false;

                if (verticalWalls[prevRow, prevCol] || verticalWalls[prevRow + 1, prevCol])
                    return false;
            }
            else
            {
                int maxRow = horizontalWalls.GetLength(0);
                int maxCol = horizontalWalls.GetLength(1);

                if (prevRow < 0 || prevRow >= maxRow || prevCol < 0 || prevCol >= maxCol - 1)
                    return false;

                if (horizontalWalls[prevRow, prevCol] || horizontalWalls[prevRow, prevCol + 1])
                    return false;
            }
            return true;
        }
    }
}