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

                if (CheckWin(currentPlayer))
                {
                    gameWon = true;
                    Console.WriteLine($"{currentPlayer.Name} wins!");
                }
                else
                {
                    opponentPlayer = currentPlayer;
                    currentPlayer = currentPlayer == player1 ? player2 : player1;
                    Console.Clear();
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

                if (ValidateMove(current, movement, opponent))
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

        private bool ValidateMove(Player current, int[] movement, Player opponent)
        {
            int[] newPos = { current.Position.Row + movement[0], current.Position.Col + movement[1] };

            if (newPos[0] < 0 || newPos[0] >= grid.Rows || newPos[1] < 0 || newPos[1] >= grid.Rows)
                return false;
            else if (newPos[0] == opponent.Position.Row && newPos[1] == opponent.Position.Row)
                return false;
            else return true;
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