using SudokuSolver.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp.Models
{
    public class SudokuSolver
    {
        #region Local Variables
        private List<Puzzle> _Puzzles { get; set; }
        private int _IterationCount { get; set; }
        private int _PuzzleCount { get; set; }
        private DateTime _StartTime { get; set; }
        private DateTime _EndTime { get; set; }
        private int _TotalAnalyticalEntries { get; set; }
        private int _TotalHypothesis { get; set; }
        private int _TotalUniqueSolutions { get; set; }
        #endregion

        #region Constructors
        protected SudokuSolver() { }

        public SudokuSolver(string[] rowCSVstrings)
        {
            _StartTime = DateTime.UtcNow;
            _EndTime = DateTime.UtcNow;
            _TotalAnalyticalEntries = 0;
            _TotalHypothesis = 0;
            _TotalUniqueSolutions = 0;

            _Puzzles = new List<Puzzle> ();
            var array = new int?[9][];

            if (rowCSVstrings == null || rowCSVstrings.Length != 9)
            {
                throw new FormatException("Error: Sudoku.cs constructor's rowCSVstrings property must be a string array of 9 comma-seperated-value strings.  Each string must have 0-9 integers between 1 and 9.  Use empty string for numbers you do not know the value of.");
            }

            for (int x = 0; x < 9; x++)
            {
                int?[] row = new int?[9];

                var rowItemsAsStrings = rowCSVstrings[x].Split(',');
                if (rowItemsAsStrings.Length != 9)
                {
                    throw new FormatException("Error: Sudoku.cs constructor's rowCSVstrings property must be a string array of 9 comma-seperated-value strings.  Each string must have 0-9 integers between 1 and 9.  Use empty string for numbers you do not know the value of.");
                }

                for (int y = 0; y < 9; y++) // For each row
                {
                    int value;
                    int.TryParse(rowItemsAsStrings[y].Trim(), out value);

                    if (value >= 1 && value <= 9) // For each integer in each row
                    {
                        row[y] = value;
                    }
                    else
                    {
                        row[y] = null;
                    }
                }

                array[x] = row;
            }

            var puzzle = new Puzzle() { Solved = false, CanSolve = true, Array = array };
            _Puzzles.Add(puzzle);
            _PuzzleCount++;

            Console.WriteLine("This is the Sudoku we will try to solve:");
            DisplaySudokuInConsole(puzzle);

            SolveAllPuzzlesRecursively();

            Console.WriteLine("\nSolutions will appear below:\n");
            foreach (var p in _Puzzles.Where(x => x.Solved == true && x.CanSolve == true))
            {
                _TotalUniqueSolutions++;
                DisplaySudokuInConsole(p);
            }

            Console.WriteLine("Total unique solutions: " + _TotalUniqueSolutions + ".");
            Console.WriteLine("Total analytical entries required: " + _TotalAnalyticalEntries + ".");
            Console.WriteLine("Total hypothetical guesses required: " + _TotalHypothesis + ".");
            Console.WriteLine("Total processing duration: " + (DateTime.UtcNow - _StartTime));

            System.Threading.Thread.Sleep(10000000);
        }
        #endregion

        #region Private methods
        private void DisplaySudokuInConsole(Puzzle puzzle)
        {
            Console.WriteLine("\nPuzzle {Solved:" + puzzle.Solved + ", CanSolve: " + puzzle.CanSolve + "}");
            for (int x = 0; x < 9; x++)
            {
                Console.WriteLine(
                    " " + puzzle.Array[x][0].ToString() + " " +
                    " " + puzzle.Array[x][1].ToString() + " " +
                    " " + puzzle.Array[x][2].ToString() + " " +
                    " " + puzzle.Array[x][3].ToString() + " " +
                    " " + puzzle.Array[x][4].ToString() + " " +
                    " " + puzzle.Array[x][5].ToString() + " " +
                    " " + puzzle.Array[x][6].ToString() + " " +
                    " " + puzzle.Array[x][7].ToString() + " " +
                    " " + puzzle.Array[x][8].ToString() + "");
            }
            Console.WriteLine("\n");
        }

        private void SolveAllPuzzlesRecursively()
        {
            var hasUnsolvedPuzzles = true;
            var found = true;
            while (hasUnsolvedPuzzles == true && found == true)
            {
                found = false;
                for (int i = 0; i < _PuzzleCount; i++)
                {
                    if (_Puzzles[i].Solved == false && _Puzzles[i].CanSolve == true)
                    {
                        found = true;
                        SolveAllStepsOfSudokuRecursively(_Puzzles[i]);
                    }
                }
                hasUnsolvedPuzzles = _Puzzles.Where(x => x.Solved == false && x.CanSolve == true).Count() > 0;
            }
        }

        private void SolveAllStepsOfSudokuRecursively(Puzzle puzzle)
        {
            while (puzzle.Solved == false && puzzle.CanSolve == true)
            {
                _IterationCount++;
                Console.WriteLine("Processing step #" + _IterationCount);
                SolveSingleStepOfSudoku(puzzle);
            }
        }

        private void SolveSingleStepOfSudoku(Puzzle puzzle)
        {
            var lowestHypothesisLevel = 10;
            var solved = true;
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    var value = GetValueForCoordinate(puzzle, x, y);
                    if (value == null)
                    {
                        var possibleValues = GetPossibleValuesForCoordinate(puzzle, x, y);
                        var possibleValuesCount = possibleValues.Count;
                        //if (possibleValuesCount == 0)
                        //    continue;

                        if (possibleValuesCount == 1)
                        {
                            SetValueForCoordinate(puzzle, x, y, possibleValues[0]);
                            return;
                        }
                        solved = false;
                        if (possibleValuesCount > 1)
                        {
                            if (possibleValuesCount < lowestHypothesisLevel) lowestHypothesisLevel = possibleValuesCount;
                        }
                    }
                }
            }
            if (solved == true)
            {
                Console.WriteLine("\nPuzzle is solved!!!!");
                puzzle.Solved = true;
                DisplaySudokuInConsole(puzzle);
                return;
            }
            puzzle.CanSolve = false;
            if (lowestHypothesisLevel <= 4) // 4 should be an arbitrary number between 2 and 9 depending on how many hypothesis should be attempted.  (NOTE: I've never seen a puzzle requiring greater than 2)
            {
                SplitPuzzleForMultipleHypothesis(puzzle, lowestHypothesisLevel);
                // We created multiple puzzles from the source puzzle.  Source puzzle must be marked as not solveable.
                return;
            }
            //throw new Exception("Puzzle cannot be solved with the currently available logic.");
        }

        private void SplitPuzzleForMultipleHypothesis(Puzzle puzzle, int hypothesisLevel)
        {
            _TotalHypothesis = _TotalHypothesis + hypothesisLevel;
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    var value = GetValueForCoordinate(puzzle, x, y);
                    if (value == null)
                    {
                        var possibleValues = GetPossibleValuesForCoordinate(puzzle, x, y);
                        var possibleValuesCount = possibleValues.Count;
                        if (possibleValuesCount > 0 && possibleValuesCount <= hypothesisLevel)
                        {
                            Console.WriteLine("\nHypothesis Required: " + possibleValuesCount + " puzzles will be created with each possible value.\n");
                            for (int i = 0; i < hypothesisLevel; i++)
                            {
                                var newPuzzle = new Puzzle() {
                                    Array = new int?[9][],
                                    CanSolve = true,
                                    Solved = false
                                };
                                for (int x1 = 0; x1 < 9; x1++)
                                {
                                    newPuzzle.Array[x1] = new int?[9];
                                    for (int y1 = 0; y1 < 9; y1++)
                                    {
                                        newPuzzle.Array[x1][y1] = puzzle.Array[x1][y1];
                                    }
                                }
                                SetValueForCoordinate(newPuzzle, x, y, possibleValues[i]);
                                _Puzzles.Add(newPuzzle);
                                _PuzzleCount++;
                            }
                            return;
                        }
                    }
                }
            }
        }

        private void SetValueForCoordinate(Puzzle puzzle, int x, int y, int? value)
        {
            _TotalAnalyticalEntries++;
            puzzle.Array[x][y] = value;
        }

        private int? GetValueForCoordinate(Puzzle puzzle, int x, int y)
        {
            return puzzle.Array[x][y];
        }

        private List<int> GetPossibleValuesForCoordinate(Puzzle puzzle, int x, int y)
        {
            var result = new List<int>();

            var rowValues = puzzle.Array[x];
            var columnValues = new int?[9]
            {
                puzzle.Array[0][y],
                puzzle.Array[1][y],
                puzzle.Array[2][y],
                puzzle.Array[3][y],
                puzzle.Array[4][y],
                puzzle.Array[5][y],
                puzzle.Array[6][y],
                puzzle.Array[7][y],
                puzzle.Array[8][y]
            };
            var boxRows = y > 5 ? new int[3] { 6, 7, 8 } : y > 2 ? new int[3] { 3, 4, 5 } : new int[3] { 0, 1, 2 };
            var boxColumns = x > 5 ? new int[3] { 6, 7, 8 } : x > 2 ? new int[3] { 3, 4, 5 } : new int[3] { 0, 1, 2 };
            var BoxValues = new int?[9]
            {
                puzzle.Array[boxColumns[0]][boxRows[0]],
                puzzle.Array[boxColumns[0]][boxRows[1]],
                puzzle.Array[boxColumns[0]][boxRows[2]],
                puzzle.Array[boxColumns[1]][boxRows[0]],
                puzzle.Array[boxColumns[1]][boxRows[1]],
                puzzle.Array[boxColumns[1]][boxRows[2]],
                puzzle.Array[boxColumns[2]][boxRows[0]],
                puzzle.Array[boxColumns[2]][boxRows[1]],
                puzzle.Array[boxColumns[2]][boxRows[2]]

                //puzzle.Array[boxRows[0]][boxColumns[0]],
                //puzzle.Array[boxRows[0]][boxColumns[1]],
                //puzzle.Array[boxRows[0]][boxColumns[2]],
                //puzzle.Array[boxRows[1]][boxColumns[0]],
                //puzzle.Array[boxRows[1]][boxColumns[1]],
                //puzzle.Array[boxRows[1]][boxColumns[2]],
                //puzzle.Array[boxRows[2]][boxColumns[0]],
                //puzzle.Array[boxRows[2]][boxColumns[1]],
                //puzzle.Array[boxRows[2]][boxColumns[2]]
            };

            for (int i = 1; i <= 9; i++)
            {
                if (!(Array.IndexOf(rowValues, i) > -1) && !(Array.IndexOf(columnValues, i) > -1) && !(Array.IndexOf(BoxValues, i) > -1))
                {
                    result.Add(i);
                }
            }

            // TODO: Do something
            return result;
        }
        #endregion
    }
}
