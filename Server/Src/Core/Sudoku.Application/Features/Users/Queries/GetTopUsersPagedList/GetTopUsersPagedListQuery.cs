using Sudoku.Application.Interfaces;
using Sudoku.Application.Parameters;
using Sudoku.Application.Wrappers;

namespace Sudoku.Application.Features.Users.Queries.GetTopUsersPagedList;

public class GetTopUsersPagedListQuery : PaginationRequestParameter, IRequest<PagedResponse<GetTopUsersPagedListResponse>>
{
}