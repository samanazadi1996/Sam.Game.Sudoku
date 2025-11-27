using Microsoft.EntityFrameworkCore;
using Sudoku.Application.DTOs.DomanDtos;
using Sudoku.Application.Features.Accounts.Commands.Shared;
using Sudoku.Application.Helpers;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sudoku.Application.Features.Accounts.Commands.ChangePassword;

public class ChangePasswordCommandHandler(IUnitOfWork unitOfWork, IAuthenticatedUserService authenticatedUser, ICryptographyServices cryptography, AccountSharedService accountSharedService) : IRequestHandler<ChangePasswordCommand, BaseResult<AuthenticationResponse>>
{
    public async Task<BaseResult<AuthenticationResponse>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken = default)
    {
        var user = await unitOfWork.Users.Get().Where(p => p.Id == authenticatedUser.GetUserId())
            .Include(p => p.UserRoles).ThenInclude(p => p.Role)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return new Error(ErrorCode.NotFound, Messages.AccountMessages.Account_NotFound_with_UserName(authenticatedUser.GetUserName()), nameof(authenticatedUser.GetUserName));
        }

        user.UpdateSecurityStamp();

        user.PasswordHash = cryptography.Hash(request.Password);

        await unitOfWork.SaveChangesAsync();

        return accountSharedService.GetAuthenticationResponse(user);
    }
}