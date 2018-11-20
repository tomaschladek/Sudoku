using System.Linq;
using SudokuSolverSdk.Dtos;

namespace SudokuSolverSdk.Strategies
{
    public class BackTrackingStrategy : IStrategy
    {
        public SudokuDto Solve(SudokuDto sudoku, ref int counter)
        {
            var coordinates = sudoku.GetFirstEmpty();
            if (coordinates == null)
            {
                // All fields filled - END condition
                return sudoku;
            }
            foreach (var value in Enumerable.Range(1,9))
            {
                if (sudoku.Isvalid(value, coordinates))
                {
                    counter++;
                    sudoku.SetValue(value, coordinates);
                    var result = Solve(sudoku, ref counter);
                    if (result != null)
                    {
                        // No success for given subtree
                        return result;
                    }
                }
            }
            // No success for current subtree
            sudoku.SetValue(0, coordinates);
            return null;
        }
    }
}