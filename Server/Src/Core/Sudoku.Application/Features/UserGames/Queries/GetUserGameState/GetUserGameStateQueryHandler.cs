using Microsoft.EntityFrameworkCore;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sudoku.Application.Features.UserGames.Queries.GetUserGameState;

public class GetUserGameStateQueryHandler(IUnitOfWork unitOfWork, IAuthenticatedUserService authenticatedUser) : IRequestHandler<GetUserGameStateQuery, BaseResult<GetUserGameStateResponse>>
{
    public async Task<BaseResult<GetUserGameStateResponse>> Handle(GetUserGameStateQuery request, CancellationToken cancellationToken)
    {
        var userId = authenticatedUser.GetUserId();

        var entity = await unitOfWork.UserGames.Get().Where(p => p.UserId == userId)
            .Where(p => p.UserGameStatus == Domain.Enums.UserGameStatus.Active)
            .Include(p => p.Game)
            .FirstOrDefaultAsync();

        return new GetUserGameStateResponse()
        {
            SavedGame = entity is not null ? new()
            {
                Level = entity.Game.Level,
                CreatedDaateTime = entity.Created,
                Time = entity.Time,

            } : null
        };
    }
}