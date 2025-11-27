using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;
using System;
using System.Collections.Generic;

namespace Sudoku.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<BaseResult<Guid>>
{
    public string UserName { get; set; }
    public string NickName { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public IList<Guid> Roles { get; set; }
}