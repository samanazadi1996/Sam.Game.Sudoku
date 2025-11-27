using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;
using Sudoku.Domain.Entities;

namespace Sudoku.Application.Features.UserGames.Commands.ClearColumn;

public class ClearColumnCommand : IRequest<BaseResult<SudokuCell>>
{
    public int Row { get; set; }
    public int Col { get; set; }
}