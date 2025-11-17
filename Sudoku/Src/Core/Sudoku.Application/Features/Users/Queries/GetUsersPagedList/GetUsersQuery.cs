using Sudoku.Application.DTOs.DomanDtos;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Parameters;
using Sudoku.Application.Wrappers;

namespace Sudoku.Application.Features.Users.Queries.GetUsersPagedList;

public class GetUsersPagedListQuery : PaginationRequestParameter, IRequest<PagedResponse<UserDto>>
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool? IsActive { get; set; }
}