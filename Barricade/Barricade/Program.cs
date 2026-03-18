using AStarAlgorithm;
using System;
using System.Linq;

namespace Barricade
{
    internal class Program
    {
        private static void Main()
        {
            int[] gridSize = new int[2];
            int[] start = new int[2];
            int[] target = new int[2];

            bool valid = false;

            while (!valid)
            {
                //Get basic inputs:
                Console.Write("Enter grid size (rows, cols): ");
                gridSize = Console.ReadLine().Split(',').Select(int.Parse).ToArray();
                Console.Write("Enter starting location (row, col):");
                start = Console.ReadLine().Split(',').Select(int.Parse).ToArray();
                Console.Write("Enter target location (row, col):");
                target = Console.ReadLine().Split(',').Select(int.Parse).ToArray();

                //Validate inputs:
                if (gridSize[0] <= 0 || gridSize[1] <= 0)
                {
                    Console.WriteLine("Grid size must be positive integers.");
                    continue;
                }

                if (start[0] < 0 || start[0] >= gridSize[0] || start[1] < 0 || start[1] >= gridSize[1])
                {
                    Console.WriteLine("Starting location must be within the grid.");
                    continue;
                }

                if (target[0] < 0 || target[0] >= gridSize[0] || target[1] < 0 || target[1] >= gridSize[1])
                {
                    Console.WriteLine("Target location must be within the grid.");
                    continue;
                }

                valid = true;
            }

            //Create required objects:
            Grid grid = new Grid(gridSize[0], gridSize[1]);
            Node startNode = grid.GetCell(start[0], start[1]);
            Node targetNode = grid.GetCell(target[0], target[1]);

            Algorithm algorithm = new Algorithm(grid);
            //Path path = algorithm.FindPath(startNode, targetNode);

            //foreach (Node node in path.Nodes)
            //{
            //    Console.WriteLine($"({node.Row}, {node.Col})");
            //}

            Board board = new Board(grid);
        }
    }
}