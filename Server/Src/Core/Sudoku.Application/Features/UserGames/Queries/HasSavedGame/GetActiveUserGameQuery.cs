using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;

namespace Sudoku.Application.Features.UserGames.Queries.HasSavedGame;

public class HasSavedGameQuery : IRequest<BaseResult<HasSavedGameResponse>>
{
}