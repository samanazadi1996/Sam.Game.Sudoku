using System;
using System.Collections.Generic;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;

namespace Sudoku.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<BaseResult<Guid>>
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public IList<Guid> Roles { get; set; }
}