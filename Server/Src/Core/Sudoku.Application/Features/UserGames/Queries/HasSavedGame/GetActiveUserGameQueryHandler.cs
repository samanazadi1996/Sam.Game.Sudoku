using Microsoft.EntityFrameworkCore;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sudoku.Application.Features.UserGames.Queries.HasSavedGame;

public class HasSavedGameQueryHandler(IUnitOfWork unitOfWork,IAuthenticatedUserService authenticatedUser) : IRequestHandler<HasSavedGameQuery, BaseResult<HasSavedGameResponse>>
{
    public async Task<BaseResult<HasSavedGameResponse>> Handle(HasSavedGameQuery request, CancellationToken cancellationToken)
    {
        var userId = authenticatedUser.GetUserId();

        var entity =await unitOfWork.UserGames.Get().Where(p => p.UserId == userId)
            .Where(p => p.UserGameStatus == Domain.Enums.UserGameStatus.Active)
            .Include(p => p.Game)
            .FirstOrDefaultAsync();

        if (entity == null)
            return new Error(ErrorCode.NotFound, "notFound");

        return new HasSavedGameResponse() {
            Level = entity.Game.Level,
            CreatedDaateTime=entity.Created,
            Time = entity.Time,            
        };
    }
}