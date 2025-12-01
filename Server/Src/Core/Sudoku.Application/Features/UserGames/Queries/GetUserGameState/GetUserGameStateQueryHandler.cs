using Microsoft.EntityFrameworkCore;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;
using System.Collections.Generic;
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

        var group = await unitOfWork.UserGames.Get()
            .Where(p => p.UserId == userId)
            .Where(p => p.UserGameStatus == Domain.Enums.UserGameStatus.EndedSucceess)
            .Include(p => p.Game)
            .GroupBy(p => p.Game.Level)
            .Select(p => new { p.Key, CountEndedSucceess = p.Count() })
            .ToListAsync();


        var levels = new List<GetUserGameStateActiveStateResponse>
        {
            new()
            {
                Level = Domain.Enums.GameLevel.Easy,
                IsActive = true,
            },
            new()
            {
                Level = Domain.Enums.GameLevel.Medium,
                IsActive = false,
                NeedPointLevelToUnlock = Domain.Enums.GameLevel.Easy,
                NeedPointToUnlock = 5
            },
            new()
            {
                Level = Domain.Enums.GameLevel.Hard,
                IsActive = false,
                NeedPointLevelToUnlock = Domain.Enums.GameLevel.Medium,
                NeedPointToUnlock = 5
            },
            new()
            {
                Level = Domain.Enums.GameLevel.Master,
                IsActive = false,
                NeedPointLevelToUnlock = Domain.Enums.GameLevel.Hard,
                NeedPointToUnlock = 8

            },
            new()
            {
                Level = Domain.Enums.GameLevel.Extreme,
                IsActive = false,
                NeedPointLevelToUnlock = Domain.Enums.GameLevel.Master,
                NeedPointToUnlock = 10
            }
        };

        foreach (var item in group)
        {
            var tmp = levels.First(p => p.NeedPointLevelToUnlock == item.Key);
            if (item.CountEndedSucceess >= tmp.NeedPointToUnlock)
            {
                tmp.IsActive = true;
                tmp.NeedPointToUnlock = null;
                tmp.NeedPointLevelToUnlock = null;
            }
            else
            {
                tmp.NeedPointToUnlock = tmp.NeedPointToUnlock - item.CountEndedSucceess;
            }
        }

        return new GetUserGameStateResponse()
        {
            ActiveStates = levels,
            SavedGame = entity is not null ? new()
            {
                Level = entity.Game.Level,
                CreatedDaateTime = entity.Created,
                Time = entity.Time,

            } : null
        };
    }
}