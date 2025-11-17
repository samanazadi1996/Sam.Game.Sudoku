namespace Sudoku.Domain.Entities
{
    public class SudokuCell
    {
        public int Number { get; set; }
        public bool Visible { get; set; }
        public int[]? Note { get; set; }

        public SudokuCell(int number = 0, bool visible = false)
        {
            Number = number;
            Visible = visible;
        }
    }

}
