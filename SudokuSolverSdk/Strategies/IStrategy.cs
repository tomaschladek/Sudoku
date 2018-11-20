using SudokuSolverSdk.Dtos;

namespace SudokuSolverSdk.Strategies
{
    public interface IStrategy
    {
        SudokuDto Solve(SudokuDto sudoku, ref int counter);
    }
}