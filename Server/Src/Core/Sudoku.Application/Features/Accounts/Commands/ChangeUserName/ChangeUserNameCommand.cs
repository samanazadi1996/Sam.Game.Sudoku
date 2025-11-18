using Sudoku.Application.DTOs.DomanDtos;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;

namespace Sudoku.Application.Features.Accounts.Commands.ChangeUserName;

public class ChangeUserNameCommand :  IRequest<BaseResult<AuthenticationResponse>>
{
    public string UserName { get; set; }

}