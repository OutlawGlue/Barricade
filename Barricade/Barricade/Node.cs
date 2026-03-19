namespace AStarAlgorithm
{
    internal class Node
    {
        private int row;
        private int col;
        private readonly bool canAccess; //indicates if the node is walkable (not a wall)
        private Node parent; //reference to the parent node for path reconstruction

        private int gCost = int.MaxValue; //current cost from start to this node

        private int hCost = 0; //estimated cost from this node to target (heuristic)
        private int fCost => gCost + hCost; //total cost (gCost + hCost)

        public Node(int row, int col)
        {
            this.row = row;
            this.col = col;
            this.canAccess = true; //works for now, but change this when there are walls
        }

        public int Row
        {
            get { return row; }
            set { row = value; }
        }

        public int Col
        {
            get { return col; }
            set { col = value; }
        }

        public bool CanAccess
        {
            get { return canAccess; }
        }

        public Node Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public int GCost
        {
            get { return gCost; }
            set { gCost = value; }
        }

        public int HCost
        {
            get { return hCost; }
            set { hCost = value; }
        }

        public int FCost
        {
            get { return fCost; }
        }
    }
}