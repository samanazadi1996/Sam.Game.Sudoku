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
            int[,] board = new int[9, 9];
            FillBoard(board);

            var visibleCount = gameLevel switch
            {
                GameLevel.Easy => 40,
                GameLevel.Medium => 30,
                GameLevel.Hard => 24,
                GameLevel.Master => 20,
                GameLevel.Extreme => 17,
                _ => 30
            };

            var cells = ConvertToSudokuCells(board, visibleCount);

            return new Game()
            {
                Level = gameLevel,
                Data = cells
            };

            static bool FillBoard(int[,] board)
            {
                for (int r = 0; r < 9; r++)
                {
                    for (int c = 0; c < 9; c++)
                    {
                        if (board[r, c] == 0)
                        {
                            var nums = Enumerable.Range(1, 9)
                                .OrderBy(x => _rnd.Next());

                            foreach (var num in nums)
                            {
                                if (IsValid(board, r, c, num))
                                {
                                    board[r, c] = num;

                                    if (FillBoard(board))
                                        return true;

                                    board[r, c] = 0;
                                }
                            }

                            return false;
                        }
                    }
                }
                return true;
            }

            static bool IsValid(int[,] board, int row, int col, int num)
            {
                for (int i = 0; i < 9; i++)
                    if (board[row, i] == num || board[i, col] == num)
                        return false;

                int sr = row - row % 3;
                int sc = col - col % 3;

                for (int r = 0; r < 3; r++)
                    for (int c = 0; c < 3; c++)
                        if (board[sr + r, sc + c] == num)
                            return false;

                return true;
            }

            static List<List<SudokuCell>> ConvertToSudokuCells(int[,] board, int visibleCount)
            {
                var positions = Enumerable.Range(0, 81)
                                          .OrderBy(_ => _rnd.Next())
                                          .Take(visibleCount)
                                          .ToHashSet();

                var result = new List<List<SudokuCell>>();

                for (int r = 0; r < 9; r++)
                {
                    var row = new List<SudokuCell>();

                    for (int c = 0; c < 9; c++)
                    {
                        int index = r * 9 + c;
                        bool visible = positions.Contains(index);

                        row.Add(new SudokuCell(board[r, c], visible));
                    }

                    result.Add(row);
                }

                return result;
            }
        }
    }
}
