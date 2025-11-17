using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Sudoku.Application.DTOs.DomanDtos;
using Sudoku.Application.Features.Accounts.Commands.Authenticate;
using Sudoku.Application.Features.Accounts.Commands.LogOut;
using Sudoku.Application.Features.Accounts.Commands.Start;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;
using Sudoku.WebApi.Infrastructure.Extensions;
using System.Threading.Tasks;

namespace Sudoku.WebApi.Endpoints;

public class AccountEndpoint : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder builder)
    {
        builder.MapPost(Authenticate);

        builder.MapPost(Start);

        builder.MapPost(LogOut).RequireAuthorization();
    }

    async Task<BaseResult<AuthenticationResponse>> Authenticate(IMediator mediator, AuthenticateCommand model)
        => await mediator.Send<AuthenticateCommand, BaseResult<AuthenticationResponse>>(model);

    async Task<BaseResult<AuthenticationResponse>> Start(IMediator mediator)
        => await mediator.Send<StartCommand, BaseResult<AuthenticationResponse>>(new StartCommand());

    async Task<BaseResult> LogOut(IMediator mediator)
        => await mediator.Send<LogOutCommand, BaseResult>(new LogOutCommand());

}
