using Microsoft.EntityFrameworkCore;
using Sudoku.Application.Helpers;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;
using Sudoku.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sudoku.Application.Features.UserGames.Commands.CheckFinally;

public class CheckFinallyCommandHandler(IUnitOfWork unitOfWork, IAuthenticatedUserService authenticatedUser) : IRequestHandler<CheckFinallyCommand, BaseResult>
{
    public async Task<BaseResult> Handle(CheckFinallyCommand request, CancellationToken cancellationToken)
    {
        var userId = authenticatedUser.GetUserId();

        var entity = await unitOfWork.UserGames.Get().Where(p => p.UserId == userId)
                .Where(p => p.UserGameStatus == Domain.Enums.UserGameStatus.Active)
                .Include(p => p.Game)
                .FirstOrDefaultAsync();

        if (entity == null)
            return new Error(ErrorCode.NotFound, Messages.UserGameMessages.ActiveGameNotFound());

        bool state = true;
        foreach (var tr in entity.Data)
        {
            foreach (var td in tr)
            {
                if (td.Status is not (Domain.Enums.SudokuCellStatus.Fixed or Domain.Enums.SudokuCellStatus.Success))
                {
                    state = false;
                }
            }

        }

        if (!state)
            return new Error(ErrorCode.NotFound, Messages.UserGameMessages.GameNotCompleted());



        entity.UserGameStatus = Domain.Enums.UserGameStatus.EndedSucceess;

        unitOfWork.UserGames.Update(entity);

        await unitOfWork.SaveChangesAsync();

        return BaseResult.Ok();
    }
}