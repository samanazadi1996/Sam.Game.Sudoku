using Microsoft.EntityFrameworkCore;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sudoku.Application.Features.Users.Queries.GetTopPlayersPagedList;

public class GetTopPlayersPagedListQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetTopPlayersPagedListQuery, PagedResponse<GetTopPlayersPagedListResponse>>
{
    public async Task<PagedResponse<GetTopPlayersPagedListResponse>> Handle(GetTopPlayersPagedListQuery request, CancellationToken cancellationToken)
    {
        var query = unitOfWork.Users.Get()
            .Include(p=>p.UserGames)

            .Select(p => new GetTopPlayersPagedListResponse()
            {
                UserName = p.UserName,
                ProfileImage = p.ProfileImage,
                NickName = p.NickName,
                Level = Math.Floor(p.Level),
                CountEndedSucceess =p.UserGames.Count(x => x.UserGameStatus == Domain.Enums.UserGameStatus.EndedSucceess)
            }).OrderByDescending(p=>p.CountEndedSucceess);

        return await unitOfWork.Paged(query, request);
    }
}