using AStarAlgorithm;
using System;

namespace Barricade
{
    internal class Player
    {
        private string name;
        private char symbol;
        private readonly ConsoleColor colour;
        private readonly Node position;
        private readonly int targetRow;

        public Player(Node startPosition, int targetRow, string name, char symbol, ConsoleColor colour)
        {
            this.name = name;
            this.symbol = symbol;
            this.colour = colour;
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

        public ConsoleColor Colour
        {
            get { return colour; }
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