using AStarAlgorithm;
using System;
using System.Linq;

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

            Node p1start = new Node(8, 4);
            Player player1 = new Player(p1start, 0, p1Name, '*');

            Node p2start = new Node(0, 4);
            Player player2 = new Player(p2start, 8, p2Name, '#');

            new Game(grid, board, player1, player2);

            Console.ReadLine();
        }
    }
}