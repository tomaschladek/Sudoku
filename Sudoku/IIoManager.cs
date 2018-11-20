using System.Collections.Generic;
using SudokuSolverSdk.Dtos;

namespace Sudoku
{
    public interface IIoManager
    {
        SudokuDto GetSudoku();
    }

    public class IoManager : IIoManager
    {
        public SudokuDto GetSudoku()
        {
            return new SudokuDto(
                new List<IList<int>>
                {
                    new List<int> {0, 0, 0, 0, 0, 0, 6, 8, 0},
                    new List<int> {0, 0, 0, 0, 7, 3, 0, 0, 9},
                    new List<int> {3, 0, 9, 0, 0, 0, 0, 4, 5},
                    new List<int> {4, 9, 0, 0, 0, 0, 0, 0, 0},
                    new List<int> {8, 0, 3, 0, 5, 0, 9, 0, 2},
                    new List<int> {0, 0, 0, 0, 0, 0, 0, 3, 6},
                    new List<int> {9, 6, 0, 0, 0, 0, 3, 0, 8},
                    new List<int> {7, 0, 0, 6, 8, 0, 0, 0, 0},
                    new List<int> {0, 2, 8, 0, 0, 0, 0, 0, 0}
                });
        }
    }
}