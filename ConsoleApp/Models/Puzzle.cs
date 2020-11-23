

using System;

namespace SudokuSolver.ConsoleApp.Models
{
    public class Puzzle
    {
        public int?[][] Array { get; set; }
        public bool Solved { get; set; }
        public bool CanSolve { get; set; }

        public static Puzzle GetFromArray(string[] rowCSVstrings)
        {
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
            return puzzle;
        }
    }
}
