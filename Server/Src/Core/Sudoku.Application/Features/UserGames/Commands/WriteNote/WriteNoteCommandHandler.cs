using Microsoft.EntityFrameworkCore;
using Sudoku.Application.Helpers;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;
using Sudoku.Domain.Entities;
using Sudoku.Domain.Enums;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sudoku.Application.Features.UserGames.Commands.WriteNote;

public class WriteNoteCommandHandler(IUnitOfWork unitOfWork, IAuthenticatedUserService authenticatedUser) : IRequestHandler<WriteNoteCommand, BaseResult<SudokuCell>>
{

    public async Task<BaseResult<SudokuCell>> Handle(WriteNoteCommand request, CancellationToken cancellationToken)
    {
        var userId = authenticatedUser.GetUserId();

        var entity = await unitOfWork.UserGames.Get().Where(p => p.UserId == userId)
                .Where(p => p.UserGameStatus == Domain.Enums.UserGameStatus.Active)
                .FirstOrDefaultAsync();

        if (entity == null)
            return new Error(ErrorCode.NotFound, Messages.UserGameMessages.ActiveGameNotFound());

        var col = entity.Data[request.Row][request.Col];
        if (col.Status != SudokuCellStatus.Fixed)
        {
            col.Note ??= [];

            if (col.Note.Count > 8)
                return new Error(ErrorCode.OutOfRange, Messages.UserGameMessages.NotesLimitReached());

            col.Number = null;
            col.Status = SudokuCellStatus.Empty;
            col.Note.Add(request.Number);
            col.Note = col.Note.Distinct().ToList();
            unitOfWork.UserGames.Update(entity);
            await unitOfWork.SaveChangesAsync();
        }

        return col;
    }
}