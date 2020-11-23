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
        
        public SudokuSolverViewModel ViewModel { get; set; }
        
        #endregion

        #region Constructors
        protected SudokuSolver() { }

        public SudokuSolver(Puzzle puzzle)
        {
            _StartTime = DateTime.UtcNow;
            ViewModel = new SudokuSolverViewModel();
            ViewModel.Solutions = new List<Puzzle>();
            ViewModel.TotalAnalyticalEntries = 0;
            ViewModel.TotalHypothesis = 0;
            ViewModel.TotalUniqueSolutions = 0;

            puzzle.CanSolve = true;
            puzzle.Solved = false;

            _Puzzles = new List<Puzzle> ();
            _Puzzles.Add(puzzle);
            _PuzzleCount++;

            SolveAllPuzzlesRecursively();

            foreach (var p in _Puzzles.Where(x => x.Solved == true && x.CanSolve == true))
            {
                ViewModel.TotalUniqueSolutions++;
                ViewModel.Solutions.Add(p);
            }

            ViewModel.ProcessingDuration = DateTime.UtcNow - _StartTime;
        }
        #endregion

        #region Private methods
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
                puzzle.Solved = true;
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
            ViewModel.TotalHypothesis = ViewModel.TotalHypothesis + hypothesisLevel;
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
            ViewModel.TotalAnalyticalEntries++;
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
