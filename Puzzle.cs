

namespace SudokuSolver.ConsoleApp.Models
{
    public class Puzzle
    {
        public int?[][] Array { get; set; }
        public bool Solved { get; set; }
        public bool CanSolve { get; set; }
    }
}
