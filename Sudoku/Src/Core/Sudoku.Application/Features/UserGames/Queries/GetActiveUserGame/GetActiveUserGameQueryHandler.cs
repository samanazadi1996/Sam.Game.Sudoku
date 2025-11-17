using Microsoft.EntityFrameworkCore;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sudoku.Application.Features.UserGames.Queries.GetActiveUserGame;

public class GetActiveUserGameQueryHandler(IUnitOfWork unitOfWork,IAuthenticatedUserService authenticatedUser) : IRequestHandler<GetActiveUserGameQuery, BaseResult<GetActiveUserGameResponse>>
{
    public async Task<BaseResult<GetActiveUserGameResponse>> Handle(GetActiveUserGameQuery request, CancellationToken cancellationToken)
    {
        var userId = authenticatedUser.GetUserId();
        var entity =await unitOfWork.UserGames.Get().Where(p => p.UserId == userId)
            .Where(p => p.UserGameStatus == Domain.Enums.UserGameStatus.Active)
            .Include(p => p.Game)
            .FirstOrDefaultAsync();

        if (entity == null)
            return new Error(ErrorCode.NotFound, "notFound");

        return new GetActiveUserGameResponse() {
            Level = entity.Game.Level,
            CreatedDaateTime=entity.Created,
            Data=entity.Data,
            Hint=entity.Hint,
            Time = entity.Time,
            Wrong=entity.Wrong,
            
        };
    }
}