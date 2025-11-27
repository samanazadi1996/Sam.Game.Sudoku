using Microsoft.EntityFrameworkCore;
using Sudoku.Application.Features.Accounts.Commands.Shared;
using Sudoku.Application.Helpers;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;
using Sudoku.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sudoku.Application.Features.Accounts.Queries.GetProfile;

public class GetProfileQueryHandler(IUnitOfWork unitOfWork, IAuthenticatedUserService authenticatedUser) : IRequestHandler<GetProfileQuery, BaseResult<GetProfileResponse>>
{
    public async Task<BaseResult<GetProfileResponse>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var self = false;
        string userName = request.UserName;
        if (string.IsNullOrEmpty(userName))
        {
            userName = authenticatedUser.GetUserName();
        }

        var user = await unitOfWork.Users.Get().Where(p => p.UserName.ToLower() == userName.ToLower())
            .Include(p => p.UserRoles).ThenInclude(p => p.Role)
            .FirstOrDefaultAsync();

        if (user == null)
            return new Error(ErrorCode.NotFound, Messages.AccountMessages.Account_NotFound_with_UserName(userName), nameof(userName));

        if (user.UserName.Equals(authenticatedUser.GetUserName(), StringComparison.OrdinalIgnoreCase))
            self = true;

        var userGames = unitOfWork.UserGames.Get()
            .Where(p => p.UserId == user.Id)
            .Include(p => p.Game);

        var resportGames = await userGames.GroupBy(p => p.Game.Level)
            .Select(p => new GetProfileRespotrGamesResponse
            {
                GameLevel = p.Key,
                EndedSucceess = p.Count(x => x.UserGameStatus == Domain.Enums.UserGameStatus.EndedSucceess),
                EndedFailed = p.Count(x => x.UserGameStatus == Domain.Enums.UserGameStatus.EndedFailed),
                Inactive = p.Count(x => x.UserGameStatus == Domain.Enums.UserGameStatus.Inactive)

            }).ToListAsync();

        var age = (int)(System.DateTime.Now - user.Created).TotalDays;
        return new GetProfileResponse()
        {
            Self = self,
            Age = age,
            UserName = user.UserName,
            ProfileImage = user.ProfileImage,
            NickName = user.NickName,
            Level =Math.Floor(user.Level),
            ReportGames = resportGames
        };
    }
}