using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sudoku.Application.Helpers;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;

namespace Sudoku.Application.Features.Accounts.Commands.LogOut;

public class LogOutCommandHandler(IUnitOfWork unitOfWork, IAuthenticatedUserService authenticatedUser) : IRequestHandler<LogOutCommand, BaseResult>
{
    public async Task<BaseResult> Handle(LogOutCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.Get().Where(p => p.Id == authenticatedUser.GetUserId())
            .Include(p => p.UserRoles).ThenInclude(p => p.Role)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return new Error(ErrorCode.NotFound, Messages.AccountMessages.Account_NotFound_with_UserName(authenticatedUser.GetUserName()), nameof(authenticatedUser.GetUserName));
        }

        user.UpdateSecurityStamp();
        await unitOfWork.SaveChangesAsync();

        return BaseResult.Ok();
    }
}