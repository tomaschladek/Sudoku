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
        private readonly IIoManager _ioManager;

        public MainWindow()
        {
            InitializeComponent();
            _ioManager = new IoManager();
            _sudoku = _ioManager.GetSudoku();

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
            var result = _sudoku;
            var original = new SudokuDto(_sudoku);
            var strategy = GetStrategy();
            var repetitions = 25;

            var duration = GetExecutionDuration(out var counter, ref result, original, strategy, repetitions);
            PopulateSudoku(new ResultDto(original, result, counter, duration));

        }

        private static long GetExecutionDuration(out int counter, ref SudokuDto result, SudokuDto original, IStrategy strategy, int repetitions)
        {
            counter = 0;
            var timer = new Stopwatch();
            timer.Start();

            if (strategy != null)
            {
                for (int index = 0; index < repetitions; index++)
                {
                    result = strategy.Solve(new SudokuDto(original), ref counter);
                }
            }

            timer.Stop();

            var duration = timer.ElapsedMilliseconds == 0 ? 1 : timer.ElapsedMilliseconds / repetitions;
            counter = counter / repetitions;
            return duration;
        }

        private IStrategy GetStrategy()
        {
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

            return strategy;
        }
    }
}
