using SudokuSolverSdk.Dtos;

namespace Sudoku.Dtos
{
    public class ResultDto
    {
        public SudokuDto Original { get; set; }
        public SudokuDto Solution { get; set; }
        public int RecursionCount { get; set; }
        public double Duration { get; set; }

        public ResultDto(SudokuDto original, SudokuDto solution, int recursionCount, long duration)
        {
            Original = original;
            Solution = solution;
            RecursionCount = recursionCount;
            Duration = (duration/1000d);
        }
    }
}