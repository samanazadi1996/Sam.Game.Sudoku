using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sudoku.Application.DTOs.DomanDtos;
using Sudoku.Application.Helpers;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;

namespace Sudoku.Application.Features.Users.Queries.GetUsersPagedList;

public class GetUsersPagedListQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUsersPagedListQuery, PagedResponse<UserDto>>
{
    public async Task<PagedResponse<UserDto>> Handle(GetUsersPagedListQuery request, CancellationToken cancellationToken)
    {
        var query = unitOfWork.Users.Get()
            .WhereIfNotNull(request.UserName, p => p.UserName.Contains(request.UserName))
            .WhereIfNotNull(request.NickName, p => p.NickName.Contains(request.NickName))
            .WhereIfNotNull(request.IsActive, p => p.IsActive == request.IsActive)

            .Include(p => p.UserRoles).ThenInclude(p => p.Role)

            .Select(p => new UserDto(p));

        return await unitOfWork.Paged(query, request);
    }
}