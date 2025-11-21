using Sudoku.Domain.Common;
using Sudoku.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Domain.Entities
{
    public class Game : AuditableBaseEntity
    {
        private static readonly Random _rnd = new Random();

        public GameLevel Level { get; set; }
        public List<List<SudokuCell>> Data { get; set; }

        public string Description { get; set; }

        public IList<UserGame> UserGames { get; set; } = new List<UserGame>();

        public static Game Generate(GameLevel gameLevel)
        {
            // 1) Generate a full valid board deterministically via base pattern + shuffles
            int[,] full = GenerateFullBoard();

            // 2) Determine how many cells to keep (visible) per difficulty
            int visibleCount = gameLevel switch
            {
                GameLevel.Easy => 40,
                GameLevel.Medium => 30,
                GameLevel.Hard => 24,
                GameLevel.Master => 20,
                GameLevel.Extreme => 17,
                _ => 30
            };

            // 3) Remove cells smartly while preserving unique solution
            int[,] puzzle = GeneratePuzzleWithUniqueSolution(full, visibleCount);

            // 4) Convert to domain cells
            var cells = ConvertToSudokuCells(puzzle, full);

            return new Game()
            {
                Level = gameLevel,
                Data = cells
            };
        }

        #region Board generation (base pattern + shuffles)

        // base pattern then shuffle rows/cols/bands to get different valid full boards quickly
        private static int[,] GenerateFullBoard()
        {
            int[,] board = new int[9, 9];
            // base pattern: classic sudoku pattern
            for (int r = 0; r < 9; r++)
            {
                for (int c = 0; c < 9; c++)
                {
                    board[r, c] = ((r * 3 + r / 3 + c) % 9) + 1;
                }
            }

            // apply a set of randomized but valid transforms
            // swap numbers mapping, swap rows within bands, swap columns within stacks, swap bands, swap stacks
            ShuffleNumberMapping(board);
            for (int i = 0; i < 10; i++)
            {
                ShuffleRowsWithinBands(board);
                ShuffleColsWithinStacks(board);
                SwapRowBands(board);
                SwapColStacks(board);
            }

            return board;
        }

        private static void ShuffleNumberMapping(int[,] board)
        {
            var map = Enumerable.Range(1, 9).OrderBy(_ => _rnd.Next()).ToArray();
            for (int r = 0; r < 9; r++)
                for (int c = 0; c < 9; c++)
                    board[r, c] = map[board[r, c] - 1];
        }

        private static void ShuffleRowsWithinBands(int[,] board)
        {
            int band = _rnd.Next(0, 3);
            int r1 = band * 3 + _rnd.Next(0, 3);
            int r2 = band * 3 + _rnd.Next(0, 3);
            if (r1 == r2) return;
            for (int c = 0; c < 9; c++)
                (board[r1, c], board[r2, c]) = (board[r2, c], board[r1, c]);
        }

        private static void ShuffleColsWithinStacks(int[,] board)
        {
            int stack = _rnd.Next(0, 3);
            int c1 = stack * 3 + _rnd.Next(0, 3);
            int c2 = stack * 3 + _rnd.Next(0, 3);
            if (c1 == c2) return;
            for (int r = 0; r < 9; r++)
                (board[r, c1], board[r, c2]) = (board[r, c2], board[r, c1]);
        }

        private static void SwapRowBands(int[,] board)
        {
            int b1 = _rnd.Next(0, 3);
            int b2 = _rnd.Next(0, 3);
            if (b1 == b2) return;
            for (int r = 0; r < 3; r++)
            {
                int row1 = b1 * 3 + r;
                int row2 = b2 * 3 + r;
                for (int c = 0; c < 9; c++)
                    (board[row1, c], board[row2, c]) = (board[row2, c], board[row1, c]);
            }
        }

        private static void SwapColStacks(int[,] board)
        {
            int s1 = _rnd.Next(0, 3);
            int s2 = _rnd.Next(0, 3);
            if (s1 == s2) return;
            for (int c = 0; c < 3; c++)
            {
                int col1 = s1 * 3 + c;
                int col2 = s2 * 3 + c;
                for (int r = 0; r < 9; r++)
                    (board[r, col1], board[r, col2]) = (board[r, col2], board[r, col1]);
            }
        }

        #endregion

        #region Puzzle generation with uniqueness guarantee

        // Try to remove numbers until only visibleCount cells remain (visibleCount = number of revealed cells)
        // We attempt removals in random order, and only keep a removal if the puzzle still has a unique solution.
        private static int[,] GeneratePuzzleWithUniqueSolution(int[,] fullBoard, int visibleCount)
        {
            int[,] puzzle = (int[,])fullBoard.Clone();
            var cellIndices = Enumerable.Range(0, 81).OrderBy(_ => _rnd.Next()).ToList();

            int currentVisible = 81;
            foreach (var idx in cellIndices)
            {
                if (currentVisible <= visibleCount) break;

                int r = idx / 9;
                int c = idx % 9;

                if (puzzle[r, c] == 0) continue;

                int backup = puzzle[r, c];
                puzzle[r, c] = 0;

                // check uniqueness: if not unique, revert
                if (!HasUniqueSolution(puzzle))
                {
                    puzzle[r, c] = backup;
                }
                else
                {
                    currentVisible--;
                }
            }

            // Fallback: if we couldn't reach exact visibleCount (rare), just return current puzzle
            return puzzle;
        }

        // Checks whether a puzzle has exactly one solution.
        // Uses backtracking solver but stops if found more than 1 solution (early exit).
        private static bool HasUniqueSolution(int[,] puzzle)
        {
            int solutions = 0;
            int[,] board = (int[,])puzzle.Clone();

            void Backtrack()
            {
                if (solutions > 1) return; // early stop if more than one solution found

                // find next empty
                int minR = -1, minC = -1;
                int minOptions = 10;
                bool foundEmpty = false;

                // heuristic: choose cell with smallest candidate set (min remaining values)
                for (int r = 0; r < 9; r++)
                {
                    for (int c = 0; c < 9; c++)
                    {
                        if (board[r, c] != 0) continue;
                        foundEmpty = true;
                        var candidates = GetCandidates(board, r, c);
                        if (candidates.Count == 0) return; // dead end
                        if (candidates.Count < minOptions)
                        {
                            minOptions = candidates.Count;
                            minR = r;
                            minC = c;
                        }
                    }
                }

                if (!foundEmpty)
                {
                    solutions++;
                    return;
                }

                var options = GetCandidates(board, minR, minC);
                foreach (var val in options)
                {
                    board[minR, minC] = val;
                    Backtrack();
                    board[minR, minC] = 0;
                    if (solutions > 1) return; // early stop
                }
            }

            Backtrack();
            return solutions == 1;
        }

        // get valid candidates for a position
        private static List<int> GetCandidates(int[,] board, int row, int col)
        {
            bool[] used = new bool[10]; // 1..9
            for (int i = 0; i < 9; i++)
            {
                if (board[row, i] != 0) used[board[row, i]] = true;
                if (board[i, col] != 0) used[board[i, col]] = true;
            }

            int sr = row - row % 3;
            int sc = col - col % 3;
            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 3; c++)
                    if (board[sr + r, sc + c] != 0)
                        used[board[sr + r, sc + c]] = true;

            var list = new List<int>(9);
            for (int v = 1; v <= 9; v++)
                if (!used[v]) list.Add(v);

            // randomize candidate order to diversify solver path
            return list.OrderBy(_ => _rnd.Next()).ToList();
        }

        #endregion

        #region Convert to SudokuCell

        private static List<List<SudokuCell>> ConvertToSudokuCells(int[,] puzzle, int[,] fullBoard)
        {
            var result = new List<List<SudokuCell>>();
            for (int r = 0; r < 9; r++)
            {
                var row = new List<SudokuCell>();
                for (int c = 0; c < 9; c++)
                {
                    if (puzzle[r, c] == 0)
                        // مقدار اصلی را حفظ می‌کنیم ولی استاتوس Empty
                        row.Add(new SudokuCell(fullBoard[r, c], SudokuCellStatus.Empty));
                    else
                        row.Add(new SudokuCell(puzzle[r, c], SudokuCellStatus.Fixed));
                }
                result.Add(row);
            }
            return result;
        }

        #endregion
    }
}
