using System;
using System.Collections.Generic;
using Sudoku.Application.DTOs.DomanDtos;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;

namespace Sudoku.Application.Features.Accounts.Commands.Start;

public class StartCommand : IRequest<BaseResult<AuthenticationResponse>>
{

}
