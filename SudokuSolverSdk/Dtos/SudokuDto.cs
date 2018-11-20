using System.Collections.Generic;
using System.Linq;

namespace SudokuSolverSdk.Dtos
{
    public class SudokuDto
    {
        public IList<IList<int>> Data { get; set; }

        public SudokuDto(IList<IList<int>> data)
        {
            Data = data;
        }

        public SudokuDto(SudokuDto sudoku) : this(sudoku.Data.Select(row => (IList<int>) new List<int>(row)).ToList())
        {
        }

        public CoordinatesDto GetFirstEmpty()
        {
            for (int rowIndex = 0; rowIndex < 9; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < 9; columnIndex++)
                {
                    if (Data[rowIndex][columnIndex] == 0)
                    {
                        return new CoordinatesDto(rowIndex, columnIndex);
                    }
                }
            }

            return null;
        }

        public bool Isvalid(int value, CoordinatesDto coordinates)
        {
            return IsValidRow(value, coordinates.Row)
                   && IsValidColumn(value, coordinates.Column)
                   && IsValidBox(value, coordinates);
        }

        private bool IsValidBox(int value, CoordinatesDto coordinates)
        {
            return GetConflictBox(value, coordinates) == null;

        }

        private static int GetFrom(int value)
        {
            var from = 3 * (value / 3);
            return from;
        }

        private bool IsValidColumn(int value, int columnIndex)
        {
            return GetConflictColumn(value,columnIndex) == null;
        }

        private bool IsValidRow(int value, int rowIndex)
        {
            return GetConflictRow(value,rowIndex) == null;
        }

        public void SetValue(int value, CoordinatesDto coordinates)
        {
            Data[coordinates.Row][coordinates.Column] = value;
        }

        public HashSet<CoordinatesDto> GetConflicts(int value, CoordinatesDto coordinates)
        {
            var set = new HashSet<CoordinatesDto>();

            var row = GetConflictRow(value, coordinates.Row);
            var column = GetConflictColumn(value, coordinates.Column);
            var box = GetConflictBox(value, coordinates);

            if (box != null)
            {
                set.Add(box);
            }

            if (row != null)
            {
                set.Add(row);
            }

            if (column != null)
            {
                set.Add(column);
            }

            return set;
        }

        private CoordinatesDto GetConflictBox(int value, CoordinatesDto coordinates)
        {
            var rowFrom = GetFrom(coordinates.Row);
            var columnFrom = GetFrom(coordinates.Column);

            for (int rowIndex = rowFrom; rowIndex < rowFrom+3; rowIndex++)
            {
                for (int columnIndex = columnFrom; columnIndex < columnFrom+3; columnIndex++)
                {
                    if (Data[rowIndex][columnIndex] == value)
                    {
                        return new CoordinatesDto(rowIndex,columnIndex);
                    }
                }
            }

            return null;
        }

        private CoordinatesDto GetConflictColumn(int value, int columnIndex)
        {
            for (int rowIndex = 0; rowIndex < 9; rowIndex++)
            {
                if (Data[rowIndex][columnIndex] == value)
                {
                    return new CoordinatesDto(rowIndex, columnIndex);
                }
            }

            return null;
        }

        private CoordinatesDto GetConflictRow(int value, int rowIndex)
        {
            for (int columnIndex = 0; columnIndex < 9; columnIndex++)
            {
                if (Data[rowIndex][columnIndex] == value)
                {
                    return new CoordinatesDto(rowIndex, columnIndex);
                }
            }
            return null;
        }
    }
}