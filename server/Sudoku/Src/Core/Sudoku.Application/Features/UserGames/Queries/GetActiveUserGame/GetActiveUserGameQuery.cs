using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;

namespace Sudoku.Application.Features.UserGames.Queries.GetActiveUserGame;

public class GetActiveUserGameQuery : IRequest<BaseResult<GetActiveUserGameResponse>>
{
}