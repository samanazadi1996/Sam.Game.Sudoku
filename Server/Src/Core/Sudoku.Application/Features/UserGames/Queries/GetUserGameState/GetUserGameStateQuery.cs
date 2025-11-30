using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;

namespace Sudoku.Application.Features.UserGames.Queries.GetUserGameState;

public class GetUserGameStateQuery : IRequest<BaseResult<GetUserGameStateResponse>>
{
}