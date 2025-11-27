using Microsoft.EntityFrameworkCore;
using Sudoku.Application.Helpers;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;
using Sudoku.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sudoku.Application.Features.UserGames.Commands.WriteSudokuCell;

public class WriteSudokuCellCommandHandler(IUnitOfWork unitOfWork, IAuthenticatedUserService authenticatedUser) : IRequestHandler<WriteSudokuCellCommand, BaseResult<SudokuCell>>
{
    public async Task<BaseResult<SudokuCell>> Handle(WriteSudokuCellCommand request, CancellationToken cancellationToken)
    {
        var userId = authenticatedUser.GetUserId();

        var entity = await unitOfWork.UserGames.Get().Where(p => p.UserId == userId)
                .Where(p => p.UserGameStatus == Domain.Enums.UserGameStatus.Active)
                .Include(p => p.Game)
                .FirstOrDefaultAsync();

        if (entity == null)
            return new Error(ErrorCode.NotFound, Messages.UserGameMessages.ActiveGameNotFound());

        var game = entity.Game;

        var colGame = game.Data[request.Row][request.Col];
        var col = entity.Data[request.Row][request.Col];

        col.Number = request.Number;
        col.Note = null;

        if (colGame.Number == request.Number)
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