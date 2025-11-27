using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;

namespace Sudoku.Application.Features.Accounts.Queries.GetProfile;

public class GetProfileQuery : IRequest<BaseResult<GetProfileResponse>>
{
    public string UserName { get; set; }
}