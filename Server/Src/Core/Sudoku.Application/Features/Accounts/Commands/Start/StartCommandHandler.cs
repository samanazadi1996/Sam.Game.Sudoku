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

namespace Sudoku.Application.Features.Accounts.Commands.Start;

public class StartCommandHandler(IUnitOfWork unitOfWork, AccountSharedService accountSharedService, ICryptographyServices cryptography) : IRequestHandler<StartCommand, BaseResult<AuthenticationResponse>>
{
    public async Task<BaseResult<AuthenticationResponse>> Handle(StartCommand request, CancellationToken cancellationToken = default)
    {
        var userName = await GetUserName();
        var password = RandomHelper.Getstring(15);

        var user = new User
        {
            Id = IdGenerator.Generate(),
            UserName = userName,
            PasswordHash = cryptography.Hash(password),
            ProfileImage = RandomHelper.GetProfileImage(),
            IsActive = true
        }.UpdateSecurityStamp();


        await unitOfWork.Users.AddAsync(user);
        await unitOfWork.SaveChangesAsync();

        return accountSharedService.GetAuthenticationResponse(user);

    }

    private async Task<string> GetUserName()
    {
        string uName;
        bool existUserName;

        do
        {
            uName = RandomHelper.Getstring(8);
            existUserName = await unitOfWork.Users.Get().AnyAsync(p => p.UserName == uName);

        } while (existUserName);

        return uName;
    }
}