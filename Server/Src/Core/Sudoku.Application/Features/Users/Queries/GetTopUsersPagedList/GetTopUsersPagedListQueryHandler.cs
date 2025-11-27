using Microsoft.EntityFrameworkCore;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sudoku.Application.Features.Users.Queries.GetTopUsersPagedList;

public class GetTopUsersPagedListQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetTopUsersPagedListQuery, PagedResponse<GetTopUsersPagedListResponse>>
{
    public async Task<PagedResponse<GetTopUsersPagedListResponse>> Handle(GetTopUsersPagedListQuery request, CancellationToken cancellationToken)
    {
        var query = unitOfWork.Users.Get()
            .Include(p=>p.UserGames)

            .Select(p => new GetTopUsersPagedListResponse()
            {
                UserName = p.UserName,
                CountEndedSucceess=p.UserGames.Count(x => x.UserGameStatus == Domain.Enums.UserGameStatus.EndedSucceess)
            }).OrderByDescending(p=>p.CountEndedSucceess);

        return await unitOfWork.Paged(query, request);
    }
}