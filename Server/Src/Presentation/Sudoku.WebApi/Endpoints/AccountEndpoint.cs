using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Sudoku.Application.DTOs.DomanDtos;
using Sudoku.Application.Features.Accounts.Commands.Authenticate;
using Sudoku.Application.Features.Accounts.Commands.ChangeNickName;
using Sudoku.Application.Features.Accounts.Commands.ChangePassword;
using Sudoku.Application.Features.Accounts.Commands.ChangeUserName;
using Sudoku.Application.Features.Accounts.Commands.LogOut;
using Sudoku.Application.Features.Accounts.Commands.Start;
using Sudoku.Application.Features.Accounts.Queries.GetProfile;
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

        builder.MapPost(ChangeUserName).RequireAuthorization();

        builder.MapPost(ChangeNickName).RequireAuthorization();

        builder.MapPost(ChangePassword).RequireAuthorization();

        builder.MapGet(GetProfile).RequireAuthorization();
    }

    async Task<BaseResult<AuthenticationResponse>> Authenticate(IMediator mediator, AuthenticateCommand model)
        => await mediator.Send<AuthenticateCommand, BaseResult<AuthenticationResponse>>(model);

    async Task<BaseResult<AuthenticationResponse>> ChangePassword(IMediator mediator, ChangePasswordCommand model)
        => await mediator.Send<ChangePasswordCommand, BaseResult<AuthenticationResponse>>(model);
    async Task<BaseResult> ChangeNickName(IMediator mediator, ChangeNickNameCommand model)
        => await mediator.Send<ChangeNickNameCommand, BaseResult>(model);

    async Task<BaseResult<AuthenticationResponse>> ChangeUserName(IMediator mediator, ChangeUserNameCommand model)
        => await mediator.Send<ChangeUserNameCommand, BaseResult<AuthenticationResponse>>(model);

    async Task<BaseResult<AuthenticationResponse>> Start(IMediator mediator)
        => await mediator.Send<StartCommand, BaseResult<AuthenticationResponse>>(new StartCommand());

    async Task<BaseResult> LogOut(IMediator mediator)
        => await mediator.Send<LogOutCommand, BaseResult>(new LogOutCommand());

    async Task<BaseResult<GetProfileResponse>> GetProfile(IMediator mediator, [AsParameters] GetProfileQuery model)
        => await mediator.Send<GetProfileQuery, BaseResult<GetProfileResponse>>(model);


}
