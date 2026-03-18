using System;
using System.Collections.Generic;

namespace AStarAlgorithm
{
    internal class Algorithm
    {
        private readonly Grid grid;

        public Algorithm(Grid grid)
        {
            this.grid = grid;
        }

        public Path FindPath(Node start, Node end)
        {
            //Reset all nodes:
            foreach (Node node in grid.GetAllNodes())
            {
                node.GCost = int.MaxValue;
                node.HCost = 0;
                node.Parent = null;
            }

            List<Node> openNodes = new List<Node>();
            List<Node> closedNodes = new List<Node>();

            openNodes.Add(start);

            while (openNodes.Count > 0)
            {
                Node current = openNodes[0];

                foreach (Node node in openNodes)
                {
                    if (node.FCost < current.FCost || (node.FCost == current.FCost && node.HCost < current.HCost))
                    {
                        current = node;
                    }
                }

                openNodes.Remove(current);
                closedNodes.Add(current);

                if (current == end)
                {
                    return RetracePath(start, end);
                }

                foreach (Node neighbour in grid.GetNeighbours(current))
                {
                    if (closedNodes.Contains(neighbour) || !neighbour.CanAccess)
                    {
                        continue;
                    }

                    int cost = current.GCost + 1;

                    if (cost < neighbour.GCost || !openNodes.Contains(neighbour))
                    {
                        neighbour.GCost = cost;
                        neighbour.HCost = CalcHeuristic(neighbour, end);
                        neighbour.Parent = current;

                        if (!openNodes.Contains(neighbour))
                        {
                            openNodes.Add(neighbour);
                        }
                    }
                }
            }
            return null; //if issue with path, return null
        }

        private int CalcHeuristic(Node start, Node end)
        {
            return Math.Abs(start.Row - end.Row) + Math.Abs(start.Col - end.Col);
        }

        private Path RetracePath(Node start, Node end)
        {
            List<Node> nodes = new List<Node>();
            Node current = end;

            while (current != start)
            {
                if (current == null)
                    return null;

                nodes.Add(current);
                current = current.Parent;
            }

            nodes.Reverse();

            return new Path(nodes);
        }
    }
}