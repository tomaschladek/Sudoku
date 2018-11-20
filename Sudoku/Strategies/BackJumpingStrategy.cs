using System.Collections.Generic;
using System.Linq;
using Sudoku.Dtos;

namespace Sudoku.Strategies
{
    public class BackJumpingStrategy : AbstractStrategy, IStrategy
    {
        public SudokuDto Solve(SudokuDto sudoku, ref int counter)
        {
            return Resolve(sudoku, ref counter).Sudoku;
        }

        private ReturnDto Resolve(SudokuDto sudoku, ref int counter)
        {
            var coordinates = sudoku.GetFirstEmpty();
            if (coordinates == null)
            {
                // All fields filled - END condition
                return new ReturnDto(sudoku, null);
            }

            var conflictSet = new HashSet<CoordinatesDto>();
            foreach (var value in Enumerable.Range(1, 9))
            {
                if (sudoku.Isvalid(value, coordinates))
                {
                    counter++;
                    sudoku.SetValue(value, coordinates);
                    var result = Resolve(sudoku, ref counter);
                    if (result.Conflicts == null)
                    {
                        // No success for given subtree
                        return result;
                    }

                    if (result.Conflicts.Any()
                    && !result.Conflicts.Contains(coordinates))
                    {
                        // Jumping back
                        sudoku.SetValue(0, coordinates);
                        return result;
                    }

                    result.Conflicts.Remove(coordinates);
                    conflictSet.UnionWith(result.Conflicts);
                }
                else
                {
                    // Not possible to add value
                    var localConflictSet = sudoku.GetConflicts(value, coordinates);
                    conflictSet.UnionWith(localConflictSet);
                }
            }
            // No success for current subtree
            sudoku.SetValue(0, coordinates);
            return new ReturnDto(null, conflictSet);
        }

        private class ReturnDto
        {
            public SudokuDto Sudoku { get; set; }
            public ISet<CoordinatesDto> Conflicts { get; set; }

            public ReturnDto(SudokuDto sudoku, ISet<CoordinatesDto> conflicts)
            {
                Sudoku = sudoku;
                Conflicts = conflicts;
            }
        }
    }
}