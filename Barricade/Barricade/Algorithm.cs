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

        public Path FindPath(Node start, Node end, bool[,] horizontal, bool[,] vertical)
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

            start.GCost = 0;
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

                //Use wall-aware neighbour lookup:
                foreach (Node neighbour in grid.GetNeighboursWithWalls(current, horizontal, vertical))
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
            return null; //No valid path exists
        }

        //Check if any cell in target row reachable:
        public bool FindPathToRow(Node start, int targetRow, bool[,] horizontal, bool[,] vertical)
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

            start.GCost = 0;
            start.HCost = Math.Abs(start.Row - targetRow);
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

                //Any cell in the target row counts as a valid goal:
                if (current.Row == targetRow)
                    return true;

                //Use wall-aware neighbour lookup:
                foreach (Node neighbour in grid.GetNeighboursWithWalls(current, horizontal, vertical))
                {
                    if (closedNodes.Contains(neighbour) || !neighbour.CanAccess)
                    {
                        continue;
                    }

                    int cost = current.GCost + 1;

                    if (cost < neighbour.GCost || !openNodes.Contains(neighbour))
                    {
                        neighbour.GCost = cost;
                        neighbour.HCost = Math.Abs(neighbour.Row - targetRow);
                        neighbour.Parent = current;

                        if (!openNodes.Contains(neighbour))
                        {
                            openNodes.Add(neighbour);
                        }
                    }
                }
            }
            return false; //No valid path to target row exists
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