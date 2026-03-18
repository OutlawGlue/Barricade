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

            while (!gameWon)
            {
                board.DisplayBoard();

                AskMove(currentPlayer);
                //currentPlayer.Move();

                if (CheckWin(currentPlayer))
                {
                    gameWon = true;
                    Console.WriteLine($"{currentPlayer} wins!");
                }
                else
                {
                    currentPlayer = currentPlayer == player1 ? player2 : player1;
                    Console.Clear();
                }
            }
        }

        private void AskMove(Player current)
        {
            int[] movement = { 0, 0 };
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
        }

        private bool CheckWin(Player currentPlayer)
        {
            return false;
        }
    }
}