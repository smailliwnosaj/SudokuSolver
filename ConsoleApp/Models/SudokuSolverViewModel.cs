using System;
using System.Collections.Generic;

namespace SudokuSolver.ConsoleApp.Models
{
    public class SudokuSolverViewModel
    {
        public int TotalAnalyticalEntries { get; set; }
        public int TotalHypothesis { get; set; }
        public int TotalUniqueSolutions { get; set; }
        public TimeSpan ProcessingDuration { get; set; }
        public List<Puzzle> Solutions { get; set; }
    }
}
