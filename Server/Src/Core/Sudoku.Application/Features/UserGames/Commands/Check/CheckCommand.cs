using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;
using Sudoku.Domain.Entities;

namespace Sudoku.Application.Features.UserGames.Commands.Check;

public class CheckCommand : IRequest<BaseResult<SudokuCell>>
{
    public int Row { get; set; }
    public int Col{ get; set; }
    public int Number{ get; set; }
}