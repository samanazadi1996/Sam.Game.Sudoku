using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;
using Sudoku.Domain.Entities;

namespace Sudoku.Application.Features.UserGames.Commands.WriteNote;

public class WriteNoteCommand : IRequest<BaseResult<SudokuCell>>
{
    public int Row { get; set; }
    public int Col { get; set; }
    public int Number { get; set; }
}