using Microsoft.EntityFrameworkCore;
using Sudoku.Application.DTOs.DomanDtos;
using Sudoku.Application.Features.Accounts.Commands.Shared;
using Sudoku.Application.Helpers;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sudoku.Application.Features.Accounts.Commands.ChangeUserName;

public class ChangeUserNameCommandHandler(IUnitOfWork unitOfWork, IAuthenticatedUserService authenticatedUser, AccountSharedService accountSharedService) : IRequestHandler<ChangeUserNameCommand, BaseResult<AuthenticationResponse>>
{
    public async Task<BaseResult<AuthenticationResponse>> Handle(ChangeUserNameCommand request, CancellationToken cancellationToken = default)
    {
        var userName = request.UserName.Trim();

        if (await unitOfWork.Users.Get().AnyAsync(p => p.UserName == userName))
            return new Error(ErrorCode.Duplicate, Messages.AccountMessages.Username_is_already_taken(userName), nameof(request.UserName));

        var user = await unitOfWork.Users.Get().Where(p => p.Id == authenticatedUser.GetUserId())
            .Include(p => p.UserRoles).ThenInclude(p => p.Role)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return new Error(ErrorCode.NotFound, Messages.AccountMessages.Account_NotFound_with_UserName(authenticatedUser.GetUserName()), nameof(authenticatedUser.GetUserName));
        }

        user.UpdateSecurityStamp();

        user.UserName= userName;

        await unitOfWork.SaveChangesAsync();

        return accountSharedService.GetAuthenticationResponse(user);
    }
}