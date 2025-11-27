using Sudoku.Application.DTOs.DomanDtos;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;

namespace Sudoku.Application.Features.Accounts.Commands.ChangeNickName;

public class ChangeNickNameCommand :  IRequest<BaseResult>
{
    public string NickName { get; set; }

}