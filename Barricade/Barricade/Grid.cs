using System.Collections.Generic;

namespace AStarAlgorithm
{
    internal class Grid
    {
        private readonly Node[,] grid;
        private readonly int rows;
        private readonly int cols;

        public Grid(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            this.grid = new Node[rows, cols];
            SetUpGrid();
        }

        private void SetUpGrid()
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Node temp = new Node(row, col);
                    grid[row, col] = temp;
                }
            }
        }

        public int Rows
        {
            get { return rows; }
        }

        public int Cols
        {
            get { return cols; }
        }

        public Node GetNode(int row, int col)
        {
            return grid[row, col];
        }

        public List<Node> GetNeighboursWithWalls(Node current, bool[,] horizontal, bool[,] vertical)
        {
            int row = current.Row;
            int col = current.Col;

            if (row < 0 || row >= rows || col < 0 || col >= cols)
                return new List<Node>();

            List<Node> neighbours = new List<Node>();

            //Up: blocked by a horizontal wall at [row-1, col]
            if (row > 0 && !horizontal[row - 1, col])
                neighbours.Add(grid[row - 1, col]);

            //Down: blocked by a horizontal wall at [row, col]
            if (row < rows - 1 && !horizontal[row, col])
                neighbours.Add(grid[row + 1, col]);

            //Left: blocked by a vertical wall at [row, col-1]
            if (col > 0 && !vertical[row, col - 1])
                neighbours.Add(grid[row, col - 1]);

            //Right: blocked by a vertical wall at [row, col]
            if (col < cols - 1 && !vertical[row, col])
                neighbours.Add(grid[row, col + 1]);

            return neighbours;
        }

        public IEnumerable<Node> GetAllNodes()
        {
            foreach (var node in grid)
                yield return node;
        }
    }
}