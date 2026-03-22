using AStarAlgorithm;
using System;

namespace Barricade
{
    internal class Program
    {
        private static void Main()
        {
            int[] gridSize = { 9, 9 };

            Console.WriteLine("Welcome to Barricade!");
            Console.Write("Player 1 name: "); string p1Name = Console.ReadLine();
            Console.Write("Player 2 name: "); string p2Name = Console.ReadLine();

            //Create required objects:
            Grid grid = new Grid(gridSize[0], gridSize[1]);
            Board board = new Board(grid);

            //Players use row/col directly:
            Player player1 = new Player(8, 4, 0, p1Name, '*', ConsoleColor.Red);
            Player player2 = new Player(0, 4, 8, p2Name, '#', ConsoleColor.Blue);

            new Game(grid, board, player1, player2);

            Console.ReadLine();
        }
    }
}