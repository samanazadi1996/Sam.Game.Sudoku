using Microsoft.EntityFrameworkCore;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;
using Sudoku.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sudoku.Application.Features.UserGames.Commands.Check;

public class CheckCommandHandler(IUnitOfWork unitOfWork, IAuthenticatedUserService authenticatedUser) : IRequestHandler<CheckCommand, BaseResult<SudokuCell>>
{
    public async Task<BaseResult<SudokuCell>> Handle(CheckCommand request, CancellationToken cancellationToken)
    {
        var userId = authenticatedUser.GetUserId();

        var entity = await unitOfWork.UserGames.Get().Where(p => p.UserId == userId)
                .Where(p => p.UserGameStatus == Domain.Enums.UserGameStatus.Active)
                .Include(p => p.Game)
                .FirstOrDefaultAsync();

        if (entity == null)
            return new Error(ErrorCode.NotFound, "notFound");

        var game = entity.Game;

        var success = game.Data[request.Row][request.Col].Number == request.Number;

        var col = entity.Data[request.Row][request.Col];

        col.Number = request.Number;

        if (success)
        {
            col.Status = Domain.Enums.SudokuCellStatus.Success;
        }
        else
        {
            col.Status = Domain.Enums.SudokuCellStatus.Error;
            entity.Wrong++;
        }

        unitOfWork.UserGames.Update(entity);
        await unitOfWork.SaveChangesAsync();

        return col;
    }
}