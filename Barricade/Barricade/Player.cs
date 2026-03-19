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
        private char symbol;
        private string colour; //change to color later
        private Node position;
        private int targetRow;

        public Player(Node startPosition, int targetRow, string name, char symbol)
        {
            this.name = name;
            this.symbol = symbol;
            position = startPosition;
            this.targetRow = targetRow;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public char Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }

        public Node Position
        {
            get { return position; }
        }

        public int TargetRow
        {
            get { return targetRow; }
        }

        public void Move(int[] movement)
        {
            position.Row += movement[0];
            position.Col += movement[1];
        }
    }
}