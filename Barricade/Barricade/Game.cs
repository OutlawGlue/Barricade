using AStarAlgorithm;
using System;

namespace Barricade
{
    internal class Game
    {
        private Grid grid;
        private Board board;
        private Player player1;
        private Player player2;
        private bool gameWon = false;

        public Game(Grid grid, Board board, Player player1, Player player2)
        {
            this.grid = grid;
            this.board = board;
            this.player1 = player1;
            this.player2 = player2;

            RunGame();
        }

        private void RunGame()
        {
            Console.Clear();

            Player currentPlayer = player1;
            Player opponentPlayer = player2;

            while (!gameWon)
            {
                board.DisplayBoard(player1, player2);

                currentPlayer.Move(AskMove(currentPlayer, opponentPlayer));
                Console.Clear();
                if (CheckWin(currentPlayer))
                {
                    board.DisplayBoard(player1, player2);
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
                movement[0] = 0; movement[1] = 0;
                Console.WriteLine($"{current.Name} please make your move.");

                ConsoleKeyInfo keyInfo = Console.ReadKey(true); // true = don't echo

                switch (keyInfo.Key)
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

        private bool CheckWin(Player currentPlayer)
        {
            if (currentPlayer.Position.Row == currentPlayer.TargetRow)
                return true;
            else
                return false;
        }
    }
}