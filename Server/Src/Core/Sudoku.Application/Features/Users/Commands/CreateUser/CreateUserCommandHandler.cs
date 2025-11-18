using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sudoku.Application.Helpers;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;
using Sudoku.Domain.Entities;
using System;

namespace Sudoku.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler(IUnitOfWork unitOfWork, ICryptographyServices cryptography) : IRequestHandler<CreateUserCommand, BaseResult<Guid>>
{
    public async Task<BaseResult<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var exists = await unitOfWork.Users.Get()
            .AnyAsync(u => u.UserName.ToUpper() == request.UserName.ToUpper(), cancellationToken);

        if (exists) new Error(ErrorCode.Duplicate, Messages.AccountMessages.UserNameAlreadyExists(request.UserName), nameof(request.UserName));

        var validRoles = await unitOfWork.Roles.Get()
            .Where(r => request.Roles.Contains(r.Id))
            .Select(r => r.Id)
            .ToListAsync(cancellationToken);

        var invalidRoles = request.Roles.Except(validRoles).ToList();
        if (invalidRoles.Any())
            return invalidRoles.Select(p => new Error(ErrorCode.NotFound, Messages.RoleMessages.RoleNotFound(p), nameof(request.Roles))).ToList();

        var user = new User
        {
            Id = IdGenerator.Generate(),
            UserName = request.UserName,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            PasswordHash = cryptography.Hash(request.Password),
            IsActive = request.IsActive,
        }.UpdateSecurityStamp();
        if (validRoles.Any())
        {
            user.UserRoles = validRoles.Select(roleId => new UserRole
            {
                Id = IdGenerator.Generate(),
                RoleId = roleId
            }).ToList();
        }
        await unitOfWork.Users.AddAsync(user);
        await unitOfWork.SaveChangesAsync();

        return user.Id;
    }
}