using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Sudoku.Application.Features.UserGames.Queries.GetActiveUserGame;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;
using Sudoku.WebApi.Infrastructure.Extensions;
using System.Threading.Tasks;

namespace Sudoku.WebApi.Endpoints;

public class UserGameEndpoint : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder builder)
    {
        builder.MapGet(GetActiveUserGame).RequireAuthorization();

    }

    async Task<BaseResult<GetActiveUserGameResponse>> GetActiveUserGame(IMediator mediator)
        => await mediator.Send<GetActiveUserGameQuery, BaseResult<GetActiveUserGameResponse>>(new GetActiveUserGameQuery());

}
