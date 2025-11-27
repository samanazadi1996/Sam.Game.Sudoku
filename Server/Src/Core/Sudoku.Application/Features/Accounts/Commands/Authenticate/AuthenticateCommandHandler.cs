using Microsoft.EntityFrameworkCore;
using Sudoku.Application.DTOs.DomanDtos;
using Sudoku.Application.Features.Accounts.Commands.Shared;
using Sudoku.Application.Helpers;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;
using Sudoku.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sudoku.Application.Features.Accounts.Commands.Authenticate;

public class AuthenticateCommandHandler(IUnitOfWork unitOfWork, AccountSharedService accountSharedService, ICryptographyServices cryptography) : IRequestHandler<AuthenticateCommand, BaseResult<AuthenticationResponse>>
{
    public async Task<BaseResult<AuthenticationResponse>> Handle(AuthenticateCommand request, CancellationToken cancellationToken = default)
    {
        var user = await unitOfWork.Users.Get().Where(p => p.UserName.ToUpper() == request.UserName.ToUpper())
            .Include(p => p.UserRoles).ThenInclude(p => p.Role)
            .FirstOrDefaultAsync();
        if (user == null)
        {
            return new Error(ErrorCode.NotFound, Messages.AccountMessages.Account_NotFound_with_UserName(request.UserName), nameof(request.UserName));
        }

        if (!PasswordSignInAsync(user, request.Password))
        {
            return new Error(ErrorCode.FieldDataInvalid, Messages.AccountMessages.Invalid_password(), nameof(request.Password));
        }

        user.UpdateSecurityStamp();
        await unitOfWork.SaveChangesAsync();

        return accountSharedService.GetAuthenticationResponse(user);
    }
    private bool PasswordSignInAsync(User user, string password)
    {
        var hash = cryptography.Hash(password);

        return user.PasswordHash.Equals(hash);
    }

}