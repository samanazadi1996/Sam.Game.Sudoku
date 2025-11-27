using Microsoft.EntityFrameworkCore;
using Sudoku.Application.Helpers;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sudoku.Application.Features.Accounts.Commands.ChangeNickName;

public class ChangeNickNameCommandHandler(IUnitOfWork unitOfWork, IAuthenticatedUserService authenticatedUser) : IRequestHandler<ChangeNickNameCommand, BaseResult>
{
    public async Task<BaseResult> Handle(ChangeNickNameCommand request, CancellationToken cancellationToken = default)
    {
        var userId = authenticatedUser.GetUserId();

        var user = await unitOfWork.Users.Get().Where(p => p.Id == userId).FirstOrDefaultAsync();

        if (user == null)
        {
            return new Error(ErrorCode.NotFound, Messages.AccountMessages.Account_NotFound_with_UserName(authenticatedUser.GetUserName()), nameof(authenticatedUser.GetUserName));
        }

        user.NickName = request.NickName.Trim();

        await unitOfWork.SaveChangesAsync();

        return BaseResult.Ok();
    }
}