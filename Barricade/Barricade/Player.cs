using System;

namespace Barricade
{
    internal class Player
    {
        private string name;
        private char symbol;
        private readonly ConsoleColor colour;
        private int row;
        private int col;
        private readonly int targetRow;
        private int wallCount;

        public Player(int startRow, int startCol, int targetRow, string name, char symbol, ConsoleColor colour)
        {
            this.name = name;
            this.symbol = symbol;
            this.colour = colour;
            this.row = startRow;
            this.col = startCol;
            this.targetRow = targetRow;

            wallCount = 10;
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

        public int Row
        {
            get { return row; }
        }

        public int Col
        {
            get { return col; }
        }

        public int TargetRow
        {
            get { return targetRow; }
        }

        public int WallCount
        {
            get { return wallCount; }
            set { wallCount = value; }
        }

        public void Move(int[] movement)
        {
            row += movement[0];
            col += movement[1];
        }
    }
}