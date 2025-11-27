using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;

namespace Sudoku.Application.Features.Users.Commands.RankUp;

public class RankUpCommand : IRequest<BaseResult<double>>
{
    public string UserName { get; set; }
}