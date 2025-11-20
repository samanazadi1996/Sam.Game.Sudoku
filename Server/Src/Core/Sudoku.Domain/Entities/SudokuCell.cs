using Sudoku.Domain.Enums;
using System;

namespace Sudoku.Domain.Entities
{
    public class SudokuCell
    {
        public int? Number { get; set; }
        public SudokuCellStatus Status { get; set; }
        public int[]? Note { get; set; }

        public SudokuCell()
        {

        }
        public SudokuCell(int number, SudokuCellStatus status)
        {
            Number = number;
            Status = status;
        }
    }

}
