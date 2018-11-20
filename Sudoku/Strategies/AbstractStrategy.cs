using System.Collections.Generic;
using System.Linq;
using Sudoku.Dtos;

namespace Sudoku.Strategies
{
    public abstract class AbstractStrategy
    {
        protected static HashSet<CoordinatesDto> GetOriginalSet(SudokuDto sudoku)
        {
            var originalSet = new HashSet<CoordinatesDto>();
            for (int rowIndex = 0; rowIndex < 9; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < 9; columnIndex++)
                {
                    if (sudoku.Data[rowIndex][columnIndex] != 0)
                    {
                        originalSet.Add(new CoordinatesDto(rowIndex, columnIndex));
                    }
                }
            }

            return originalSet;
        }


        protected IList<IList<IList<int>>> MaintainArcConsistency(SudokuDto sudoku, IList<IList<IList<int>>> enumerations, params CoordinatesDto[] initialValues)
        {
            var queue = new Queue<CoordinatesDto>(initialValues);
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                var value = sudoku.Data[node.Row][node.Column];
                foreach (var rowIndex in Enumerable.Range(0, 9).Except(new[] { node.Row }))
                {
                    if (IsDeadEnd(sudoku, enumerations, queue, value, rowIndex, node.Column))
                    {
                        return null;
                    }
                }

                foreach (var columnIndex in Enumerable.Range(0, 9).Except(new[] { node.Column }))
                {
                    if (IsDeadEnd(sudoku, enumerations, queue, value, node.Row, columnIndex))
                    {
                        return null;
                    }
                }

                var rowInterval = GetBoxInterval(node.Row);
                var columnInterval = GetBoxInterval(node.Column);

                for (int rowIndex = rowInterval.From; rowIndex < rowInterval.To; rowIndex++)
                {
                    for (int columnIndex = columnInterval.From; columnIndex < columnInterval.To; columnIndex++)
                    {
                        if (IsDeadEnd(sudoku, enumerations, queue, value, rowIndex, columnIndex))
                        {
                            return null;
                        }
                    }
                }
            }
            return enumerations;
        }

        private static bool IsDeadEnd(SudokuDto sudoku, IList<IList<IList<int>>> enumerations,
            Queue<CoordinatesDto> queue, int value, int rowIndex, int columnIndex)
        {
            if (sudoku.Data[rowIndex][columnIndex] != 0)
            {
                return false;
            }
            var enumerationLocal = enumerations[rowIndex][columnIndex];
            if (enumerationLocal.Contains(value))
            {
                enumerationLocal.Remove(value);
                if (!enumerationLocal.Any())
                {
                    return true;
                }
                var coordinatesDto = new CoordinatesDto(rowIndex, columnIndex);
                if (!queue.Contains(coordinatesDto))
                {
                    queue.Enqueue(coordinatesDto);
                }
            }

            return false;
        }

        private static IntervalDto GetBoxInterval(int value)
        {
            var @from = 3 * (value / 3);
            var to = 3 * (value / 3 + 1);
            var interval = new IntervalDto(@from, to);
            return interval;
        }

        protected IList<IList<IList<int>>> Copy(IList<IList<IList<int>>> enumerations)
        {
            return enumerations.Select(row => (IList<IList<int>>)new List<IList<int>>(row.Select(cell => new List<int>(cell)))).ToList();
        }
    }
}