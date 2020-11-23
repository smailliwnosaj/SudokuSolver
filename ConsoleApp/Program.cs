using ConsoleApp.Models;
using SudokuSolver.ConsoleApp.Models;
using System;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            // SUDOKU SOLVER 
            // Solve any Sudoku puzzle....even the hardest puzzles in the world!
            // If a puzzle has multiple solutions, this will show every possible solution.

            // INSTRUCTIONS:
            // Puzzles are defined, below
            // To try a different puzzle, change the code...you should know how...don't make me teach you that part.
            // Debug the code.
            // You will see a console application start running.

            // START OF SUDOKU PUZZLE SAMPLES

            // Typical "Easy" puzzle
            // 38 provided numbers
            // Total unique solutions: 1.
            // Total analytical entries required: 43.
            // Total hypothetical guesses required: 0.
            // Total processing duration: 00:00:00.0289833
            //var puzzle = new string[9] {
            //    "9, ,8, , ,4, ,6,2",
            //    "3,1,2,6, , ,4, , ",
            //    "6,7,4, ,1, , ,3,8",
            //    "7, ,1, , , , , , ",
            //    " , , ,8,5, ,7, ,6",
            //    " ,2, , ,7,3,8,9, ",
            //    "8,6,3, ,9,7, ,5, ",
            //    " ,9,5, ,4,2, , , ",
            //    " , , , , , ,9, , "
            //};

            // HARDEST PROBLEM IN THE WORLD!!!!  Though, if you find a more difficult problem, please let me know.
            // http://www.free-sudoku.com/sudoku.php?id=127732
            // 17 provided numbers - 1748499 possible outcomes - 1 solution - max hypothesis of 2
            // Depending on how good your development system is, this solution could take +/- 40 minutes of (single thread) processing and a good deal of system memory.
            // This puzzle requires making hypothetical guesses about 800k times.
            // For each hypothetical guess, the code will split the puzzle into multiple puzzles.
            // The code will solve each of 1748499 puzzles until it finds the one unique solution.
            // 17 provided numbers
            //var puzzle = new string[9] {
            //    " ,5, , ,1, , , , ",
            //    " , , , , , ,4,6, ",
            //    " , , , , , ,3, ,8",
            //    "6, , ,3, , , , , ",
            //    " ,7, , , , , ,1, ",
            //    " , , , , ,4, , , ",
            //    " , , ,5,7, ,2, , ",
            //    "3, ,4, , , , , , ",
            //    " , , ,1, , , , , "
            //};

            // Non-Sudoku puzzle.  This is not a Sudoku because it has more than one solution.
            // 24 provided numbers
            // Total unique solutions: 8.
            // Total analytical entries required: 12504.
            // Total hypothetical guesses required: 4592.
            // Total processing duration: 00:00:03.4016929
            //var puzzle = new string[9] {
            //    " ,8, , , ,9,7,4,3",
            //    " ,5, , , ,8, ,1, ",
            //    " ,1, , , , , , , ",
            //    "8, , , , ,5, , , ",
            //    " , , ,8, ,4, , , ",
            //    " , , ,3, , , , ,6",
            //    " , , , , , , ,7, ",
            //    " ,3, ,5, , , ,8, ",
            //    "9,7,2,4, , , ,5, "
            //};

            // Hard Sudoku
            // 23 provided numbers
            // Total unique solutions: 1.
            // Total analytical entries required: 20804.
            // Total hypothetical guesses required: 5662.
            // Total processing duration: 00:00:03.9428131
            //var puzzle = new string[9] {
            //    " ,2,9,1, , , ,6, ",
            //    " ,7, , , , , , ,5",
            //    " , ,1,3, ,4, , , ",
            //    " , ,3, , , , , ,9",
            //    " , , ,6,4, ,8, , ",
            //    " , , ,5,3, , , ,2",
            //    " ,9, , , , ,2, , ",
            //    " , , ,2, , , , , ",
            //    "8, , , , , , ,5,7"
            //};

            // Finnish mathematician Arto Inkala devised this Sudoku puzzle in 2012, dubbing it the most 
            // difficult setup of the puzzle in the world. He named the thing Everest, because, well, 
            // conquering it is an incomprehensible feat.Inkala specifically designed Everest to be unsolvable 
            // to anyone but the most brilliant.For reference, most Sudoku grids are graded on a five - star scale, 
            // where five stars denotes the most challenging puzzles. According to this ranking system, 
            // Inkala rated Everest an eleven
            // 21 provided numbers
            // Total unique solutions: 1.
            // Total analytical entries required: 215825.
            // Total hypothetical guesses required: 75066.
            // Total processing duration: 00:00:14.7659485
            //var puzzle = new string[9] {
            //    "8, , , , , , , , ",
            //    " , ,3,6, , , , , ",
            //    " ,7, , ,9, ,2, , ",
            //    " ,5, , , ,7, , , ",
            //    " , , , ,4,5,7, , ",
            //    " , , ,1, , , ,3, ",
            //    " , ,1, , , , ,6,8",
            //    " , ,8,5, , , ,1, ",
            //    " ,9, , , , ,4, , "
            //};

            // Arto Inkala has made what he claims is the hardest sudoku puzzle ever. According to the Finnish puzzle maker 
            // "I called the puzzle AI Escargot, because it looks like a snail. Solving it is like an intellectual culinary pleasure." 
            // If you're open for the challenge, AI Escargot presumably requires you to wrap your brain around eight casual 
            // relationships simultaneously, whereas your everyday "very hard" sudoku piece, only require you to think about 
            // a meager one or two of these relationships at once.
            // 24 provided numbers
            // Total unique solutions: 1.
            // Total analytical entries required: 12553.
            // Total hypothetical guesses required: 2800.
            // Total processing duration: 00:00:03.4479183
            //var puzzle = new string[9] {
            //    "1, , , , ,7, ,9, ",
            //    " ,3, , ,2, , , ,8",
            //    " , ,9,6, , ,5, , ",
            //    " , ,5,3, , ,9, , ",
            //    " ,1, , ,8, , , ,2",
            //    "6, , , , ,4, , , ",
            //    "3, , , , , , ,1, ",
            //    " ,4,1, , , , , ,7",
            //    " , ,7, , , ,3, , "
            //};

            // The brainchild of Finnish mathematical whizz Arto Inkala, it took him three months to compile. 
            // 23 provided numbers
            // Total unique solutions: 1.
            // Total analytical entries required: 6349.
            // Total hypothetical guesses required: 1017.
            // Total processing duration: 00:00:02.8184742
            //var puzzle = new string[9] {
            //    " , ,5,3, , , , , ",
            //    "8, , , , , , ,2, ",
            //    " ,7, , ,1, ,5, , ",
            //    "4, , , , ,5,3, , ",
            //    " ,1, , ,7, , , ,6",
            //    " , ,3,2, , , ,8, ",
            //    " ,6, ,5, , , , ,9",
            //    " , ,4, , , , ,3, ",
            //    " , , , , ,9,7, , "
            //};

            // http://sw-amt.ws/sudoku/level-very-deep/zz-www.sudokuwiki.org-0177-base/zz-www.sudokuwiki.org-0177-base.html
            // 23 provided numbers
            // Total unique solutions: 1.
            // Total analytical entries required: 89827.
            // Total hypothetical guesses required: 22299.
            // Total processing duration: 00:00:07.5180742
            //var puzzle = new string[9] {
            //    " , , , ,7,3, ,6, ",
            //    " , , ,6, , ,4, ,9",
            //    "6, , , , , ,3, , ",
            //    "9, ,5, ,8, , , , ",
            //    " ,1, , , , ,5, , ",
            //    "3, , ,4, , , ,9, ",
            //    " ,8, , , ,2, , , ",
            //    "4, , ,7, , , ,3, ",
            //    " , ,2, , , , , ,1"
            //};

            // http://sw-amt.ws/sudoku/level-very-deep/zz-www.sudokuwiki.org-0249-base/zz-www.sudokuwiki.org-0249-base.html
            // 23 provided numbers
            // Total unique solutions: 1.
            // Total analytical entries required: 338146.
            // Total hypothetical guesses required: 106540.
            // Total processing duration: 00:00:21.7591018
            //var puzzle = new string[9] {
            //    " ,4, ,7, , ,1, , ",
            //    " , , , ,2, , ,9, ",
            //    "9, , , , , , , ,4",
            //    " , ,5, , ,3, , , ",
            //    "2, , , ,5, , , ,3",
            //    " ,6, ,4, , ,8, , ",
            //    " ,1, , , , , ,7,8",
            //    " , , , , , ,4, , ",
            //    " , ,4,8, , , ,6,1"
            //};

            // http://sw-amt.ws/sudoku/level-deep/xx-tarx0052-base/xx-tarx0052-base.html
            // 21 provided numbers
            // Total unique solutions: 1.
            // Total analytical entries required: 188450.
            // Total hypothetical guesses required: 64096.
            // Total processing duration: 00:00:13.3716503
            //var puzzle = new string[9] {
            //    " , , , , , , , ,2",
            //    " , ,8, , ,9,1, , ",
            //    "5, , , , , , ,4, ",
            //    " , , ,9, ,7, , , ",
            //    " , ,7, ,3, ,8, , ",
            //    " , , ,8, ,1, ,3, ",
            //    " ,4, , ,6, , , ,5",
            //    " , ,9,7, , ,3, , ",
            //    "2, , , , , , , , "
            //};

            // https://www.free-sudoku.com/sudoku.php?dchoix=evil
            // 17 provided numbers
            // Total unique solutions: 1.
            // Total analytical entries required: 765032.
            // Total hypothetical guesses required: 347818.
            // Total processing duration: 00:00:47.5223786
            //var puzzle = new string[9] {
            //    " , , , ,9,3, , , ",
            //    "2, , , , , ,9, , ",
            //    "6, , , , , , , , ",
            //    " , , ,4, , , ,6, ",
            //    " ,3, , , , , , ,1",
            //    " , , ,2, , , , , ",
            //    " ,7,1, , ,5, , , ",
            //    " , , , ,4, ,6,2, ",
            //    " , , , , , ,8, , "
            //};

            // Created by Jason Williams - 2020-03-12
            // 23 provided numbers
            // Total unique solutions: 1.
            // Total analytical entries required: 10217.
            // Total hypothetical guesses required: 2430.
            // Total processing duration: 00:00:03.1837216
            var puzzle = new string[9] {
                " , ,2, , ,8, , , ",
                " ,4, , , ,6, , ,2",
                " , , ,1,7, ,8, , ",
                "7, , , , , , , , ",
                " ,8, , , , , ,5, ",
                " , ,9, , , , , ,3",
                " , ,4, ,3,7, ,9, ",
                "3,5, , , , , ,7, ",
                " , , ,4,8, ,6, , "
            };

            Console.WriteLine("This is the Sudoku we will try to solve:");
            DisplaySudokuInConsole(Puzzle.GetFromArray(puzzle));

            // Create an instance of our Sudoku class
            var sudokuSolverViewModel = new Models.SudokuSolver(Puzzle.GetFromArray(puzzle)).ViewModel;

            Console.WriteLine("\nSolutions will appear below:\n");
            foreach (var p in sudokuSolverViewModel.Solutions)
            {
                DisplaySudokuInConsole(p);
            }

            Console.WriteLine("Total unique solutions: " + sudokuSolverViewModel.TotalUniqueSolutions + ".");
            Console.WriteLine("Total analytical entries required: " + sudokuSolverViewModel.TotalAnalyticalEntries + ".");
            Console.WriteLine("Total hypothetical guesses required: " + sudokuSolverViewModel.TotalHypothesis + ".");
            Console.WriteLine("Total processing duration: " + sudokuSolverViewModel.ProcessingDuration);

            System.Threading.Thread.Sleep(10000000);
        }

        private static void DisplaySudokuInConsole(Puzzle puzzle)
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

    }

}
