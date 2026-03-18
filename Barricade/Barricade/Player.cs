using AStarAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barricade
{
    internal class Player
    {
        private string name;
        private Node position;

        public Player(Node startPosition, int targetRow, string name)
        {
            position = startPosition;
            this.name = name;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Node Position
        {
            get { return position; }
        }

        public void Move(Node newPosition)
        {
            position = newPosition;
        }
    }
}