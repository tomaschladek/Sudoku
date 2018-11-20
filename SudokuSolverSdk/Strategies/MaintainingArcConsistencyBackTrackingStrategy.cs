using System.Collections.Generic;
using System.Linq;
using SudokuSolverSdk.Dtos;

namespace SudokuSolverSdk.Strategies
{
    public class MaintainingArcConsistencyBackTrackingStrategy : AbstractStrategy, IStrategy
    {
        public SudokuDto Solve(SudokuDto sudoku, ref int counter)
        {
            IList<IList<IList<int>>> enumerations = new List<IList<IList<int>>>();
            for (int rowIndex = 0; rowIndex < 9; rowIndex++)
            {
                enumerations.Add(new List<IList<int>>());
                for (int columnIndex = 0; columnIndex < 9; columnIndex++)
                {
                    enumerations.Last().Add(new List<int>(Enumerable.Range(1, 9)));
                }
            }

            enumerations = MaintainArcConsistency(sudoku, enumerations, GetOriginalSet(sudoku).ToArray());
            return Resolve(sudoku, ref counter, enumerations);
        }

        public SudokuDto Resolve(SudokuDto sudoku, ref int counter, IList<IList<IList<int>>> enumerations)
        {
            var coordinates = sudoku.GetFirstEmpty();
            if (coordinates == null)
            {
                // All fields filled - END condition
                return sudoku;
            }
            foreach (var value in enumerations[coordinates.Row][coordinates.Column])
            {
                if (sudoku.Isvalid(value, coordinates))
                {
                    counter++;
                    sudoku.SetValue(value, coordinates);
                    var newEnumerations = MaintainArcConsistency(sudoku, Copy(enumerations), coordinates);
                    if (newEnumerations == null)
                    {
                        sudoku.SetValue(0, coordinates);
                        continue;
                    }
                    var result = Resolve(sudoku, ref counter, newEnumerations);
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