using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Sudoku.Application.Features.UserGames.Commands.Check;
using Sudoku.Application.Features.UserGames.Commands.CreateGame;
using Sudoku.Application.Features.UserGames.Queries.GetActiveUserGame;
using Sudoku.Application.Features.UserGames.Queries.HasSavedGame;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;
using Sudoku.Domain.Entities;
using Sudoku.WebApi.Infrastructure.Extensions;
using System.Threading.Tasks;

namespace Sudoku.WebApi.Endpoints;

public class UserGameEndpoint : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder builder)
    {
        builder.MapGet(GetActiveUserGame).RequireAuthorization();

        builder.MapGet(HasSavedGame).RequireAuthorization();

        builder.MapPost(CreateGame).RequireAuthorization();

        builder.MapPost(Check).RequireAuthorization();

    }

    async Task<BaseResult<GetActiveUserGameResponse>> GetActiveUserGame(IMediator mediator)
        => await mediator.Send<GetActiveUserGameQuery, BaseResult<GetActiveUserGameResponse>>(new GetActiveUserGameQuery());

    async Task<BaseResult<HasSavedGameResponse>> HasSavedGame(IMediator mediator)
        => await mediator.Send<HasSavedGameQuery, BaseResult<HasSavedGameResponse>>(new HasSavedGameQuery());

    async Task<BaseResult> CreateGame(IMediator mediator, CreateGameCommand request)
        => await mediator.Send<CreateGameCommand, BaseResult>(request);
    async Task<BaseResult<SudokuCell>> Check(IMediator mediator, CheckCommand request)
        => await mediator.Send<CheckCommand, BaseResult<SudokuCell>>(request);

}
