using Microsoft.EntityFrameworkCore;
using Sudoku.Application.Helpers;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sudoku.Application.Features.Users.Commands.RankUp;

public class RankUpCommandHandler(IUnitOfWork unitOfWork, IAuthenticatedUserService authenticatedUser) : IRequestHandler<RankUpCommand, BaseResult<double>>
{
    public async Task<BaseResult<double>> Handle(RankUpCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.UserName))
        {
            request.UserName = authenticatedUser.GetUserName();
        }

        var user = await unitOfWork.Users.Get().Where(p => p.UserName == request.UserName)
            .Include(p => p.UserRoles).ThenInclude(p => p.Role)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return new Error(ErrorCode.NotFound, Messages.AccountMessages.Account_NotFound_with_UserName(request.UserName), nameof(request.UserName));
        }
        var rank = Math.Floor(user.Level);

        var temp = 1.0 / rank;

        user.Level += temp;

        await unitOfWork.SaveChangesAsync();

        return user.Level;
    }
}