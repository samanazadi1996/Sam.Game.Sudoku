using Sudoku.Application.DTOs.DomanDtos;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;

namespace Sudoku.Application.Features.Accounts.Commands.ChangePassword;

public class ChangePasswordCommand : IRequest<BaseResult<AuthenticationResponse>>
{
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

}