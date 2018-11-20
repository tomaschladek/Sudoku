using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using Sudoku.Dtos;
using SudokuSolverSdk.Dtos;
using SudokuSolverSdk.Strategies;

namespace Sudoku
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private SudokuDto _sudoku;

        public MainWindow()
        {
            InitializeComponent();
            _sudoku = new SudokuDto(
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

            PopulateSudoku(new ResultDto(_sudoku,_sudoku,0,0));
        }

        private void PopulateSudoku(ResultDto sudoku)
        {
            SudokuGrid.ItemsSource = new ObservableCollection<SudokuRowDto>(sudoku.Solution?.Data.Select(row => new SudokuRowDto(row)).ToList() ?? sudoku.Original.Data.Select(row => new SudokuRowDto(row)).ToList());
            Duration.Content = $"{sudoku.Duration}s";
            CallsCount.Content = $"{sudoku.RecursionCount}";
        }

        private void Strategy_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var timer = new Stopwatch();
            timer.Start();
            var counter = 0;
            var result = _sudoku;
            var original = new SudokuDto(_sudoku);
            var strategy = default(IStrategy);
            switch (Strategy.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    strategy = new BackTrackingStrategy();
                    break;
                case 2:
                    strategy = new BackJumpingStrategy();
                    break;
                case 3:
                    strategy = new MaintainingArcConsistencyBackTrackingStrategy();
                    break;
            }

            var repetitions = 25;
            if (strategy != null)
            {
                for (int index = 0; index < repetitions; index++)
                {
                    result = strategy.Solve(new SudokuDto(original), ref counter);
                }
            }
            timer.Stop();
            var duration = timer.ElapsedMilliseconds == 0 ? 1 : timer.ElapsedMilliseconds/ repetitions;
            PopulateSudoku(new ResultDto(original, result, counter/ repetitions, duration));

        }
    }
}
