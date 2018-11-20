using System;

namespace Sudoku.Dtos
{
    public class CoordinatesDto : IComparable
    {
        public int Row { get; }
        public int Column { get; }

        public CoordinatesDto(int row, int column)
        {
            Row = row;
            Column = column;
        }

        protected bool Equals(CoordinatesDto other)
        {
            return Row == other.Row && Column == other.Column;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CoordinatesDto) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Row * 397) ^ Column;
            }
        }

        public int CompareTo(object obj)
        {
            if (obj is CoordinatesDto other)
            {
                if (Row > other.Row)
                {
                    return 1;
                }
                if (Row < other.Row)
                {
                    return -1;
                }

                if (Column > other.Column)
                {
                    return 1;
                }
                if (Column < other.Column)
                {
                    return -1;
                }
            }

            return 0;
        }

        public override string ToString()
        {
            return $"[{Row}][{Column}]";
        }


    }
}