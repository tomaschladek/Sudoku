using Sudoku.Dtos;

namespace Sudoku.Strategies
{
    public interface IStrategy
    {
        SudokuDto Solve(SudokuDto sudoku, ref int counter);
    }
}