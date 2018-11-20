using System.Collections.Generic;

namespace Sudoku.Dtos
{
    public class SudokuRowDto
    {
        public string A1 { get; set; }
        public string A2 { get; set; }
        public string A3 { get; set; }
        public string A4 { get; set; }
        public string A5 { get; set; }
        public string A6 { get; set; }
        public string A7 { get; set; }
        public string A8 { get; set; }
        public string A9 { get; set; }

        public SudokuRowDto(IList<int> row)
        {
            A1 = row[0] != 0 ? row[0].ToString() : "";
            A2 = row[1] != 0 ? row[1].ToString() : "";
            A3 = row[2] != 0 ? row[2].ToString() : "";
            A4 = row[3] != 0 ? row[3].ToString() : "";
            A5 = row[4] != 0 ? row[4].ToString() : "";
            A6 = row[5] != 0 ? row[5].ToString() : "";
            A7 = row[6] != 0 ? row[6].ToString() : "";
            A8 = row[7] != 0 ? row[7].ToString() : "";
            A9 = row[8] != 0 ? row[8].ToString() : "";
        }
    }
}