using ConsoleApp.Models;
using System;

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
            // 38 provided numbers - 1 possible outcome - 5 solution - max hypothesis of 2
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

            //var puzzle = new string[9] {
            //    " , , , , , , , , ",
            //    " , , , , , , , , ",
            //    " , , , , , , , , ",
            //    " , , , , , , , , ",
            //    " , , , , , , , , ",
            //    " , , , , , , , , ",
            //    " , , , , , , , , ",
            //    " , , , , , , , , ",
            //    " , , , , , , , , "
            //};

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
            // 21 provided numbers - 69853 possible outcomes - 1 solution - max hypothesis of 2
            var puzzle = new string[9] {
                "8, , , , , , , , ",
                " , ,3,6, , , , , ",
                " ,7, , ,9, ,2, , ",
                " ,5, , , ,7, , , ",
                " , , , ,4,5,7, , ",
                " , , ,1, , , ,3, ",
                " , ,1, , , , ,6,8",
                " , ,8,5, , , ,1, ",
                " ,9, , , , ,4, , "
            };

            // Arto Inkala has made what he claims is the hardest sudoku puzzle ever. According to the Finnish puzzle maker 
            // "I called the puzzle AI Escargot, because it looks like a snail. Solving it is like an intellectual culinary pleasure." 
            // If you're open for the challenge, AI Escargot presumably requires you to wrap your brain around eight casual 
            // relationships simultaneously, whereas your everyday "very hard" sudoku piece, only require you to think about 
            // a meager one or two of these relationships at once.
            // 24 provided numbers - 2795 possible outcomes - 1 solution - max hypothesis of 2
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
            // 23 provided numbers - 1009 possible outcomes - 1 solution - max hypothesis of 2
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
            // I tested this problem with each number missing.  This version is the most difficult version of this puzzle.

            // http://sw-amt.ws/sudoku/level-very-deep/zz-www.sudokuwiki.org-0177-base/zz-www.sudokuwiki.org-0177-base.html
            // 23 provided numbers, 21685 possible outcomes - 1 solution - max hypothesis of 2
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
            // 23 provided numbers - 100353 possible outcomes - 1 solution - max hypothesis of 2
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
            // 21 provided numbers - 40799 possible outcomes - 1 solution - max hypothesis of 2
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
            // 17 provided numbers - 330543 possible outcomes - 1 solution - max hypothesis of 2
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
            // 23 provided numbers - 2383 possible outcomes - 1 solution - max hypothesis of 2
            //var puzzle = new string[9] {
            //    " , ,2, , ,8, , , ",
            //    " ,4, , , ,6, , ,2",
            //    " , , ,1,7, ,8, , ",
            //    "7, , , , , , , , ",
            //    " ,8, , , , , ,5, ",
            //    " , ,9, , , , , ,3",
            //    " , ,4, ,3,7, ,9, ",
            //    "3,5, , , , , ,7, ",
            //    " , , ,4,8, ,6, , "
            //};
            // I tested this problem with each number missing.  This version is the most difficult version of this puzzle.

            // Create an instance of our Sudoku class
            var solver = new Models.SudokuSolver(puzzle);
        }

    }

}
