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
        private Node position;

        public Player(Node startPosition)
        {
            position = startPosition;
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