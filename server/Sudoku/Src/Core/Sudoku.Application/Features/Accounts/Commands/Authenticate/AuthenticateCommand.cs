using System;
using System.Collections.Generic;
using Sudoku.Application.DTOs.DomanDtos;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;

namespace Sudoku.Application.Features.Accounts.Commands.Authenticate;

public class AuthenticateCommand : IRequest<BaseResult<AuthenticationResponse>>
{
    public string UserName { get; set; }

    public string Password { get; set; }

}