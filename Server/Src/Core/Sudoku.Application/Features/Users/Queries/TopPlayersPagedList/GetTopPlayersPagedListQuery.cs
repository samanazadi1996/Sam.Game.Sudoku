using Sudoku.Application.Interfaces;
using Sudoku.Application.Parameters;
using Sudoku.Application.Wrappers;

namespace Sudoku.Application.Features.Users.Queries.GetTopPlayersPagedList;

public class GetTopPlayersPagedListQuery : PaginationRequestParameter, IRequest<PagedResponse<GetTopPlayersPagedListResponse>>
{
}