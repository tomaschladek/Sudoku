namespace SudokuSolverSdk.Dtos
{
    internal class IntervalDto
    {
        public int From { get; set; }
        public int To { get; set; }

        public IntervalDto(int @from, int to)
        {
            From = @from;
            To = to;
        }
    }
}