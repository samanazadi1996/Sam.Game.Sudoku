using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Sudoku.Application.DTOs.DomanDtos;
using Sudoku.Application.Features.Users.Commands.CreateUser;
using Sudoku.Application.Features.Users.Commands.RankUp;
using Sudoku.Application.Features.Users.Queries.GetTopUsersPagedList;
using Sudoku.Application.Features.Users.Queries.GetUsersPagedList;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;
using Sudoku.Domain.Entities;
using Sudoku.WebApi.Infrastructure.Extensions;
using System;
using System.Threading.Tasks;

namespace Sudoku.WebApi.Endpoints;

public class UserEndpoint : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder builder)
    {
        builder.MapGet(GetUsersPagedList).RequireAuthorization(p => p.RequireRole(Role.AdminRoleName));

        builder.MapGet(GetTopUsersPagedList).RequireAuthorization();

        builder.MapPost(CreateUser).RequireAuthorization(p => p.RequireRole(Role.AdminRoleName));

        builder.MapPost(RankUp).RequireAuthorization(p => p.RequireRole(Role.AdminRoleName));

    }

    async Task<PagedResponse<UserDto>> GetUsersPagedList(IMediator mediator, [AsParameters] GetUsersPagedListQuery model)
        => await mediator.Send<GetUsersPagedListQuery, PagedResponse<UserDto>>(model);

    async Task<PagedResponse<GetTopUsersPagedListResponse>> GetTopUsersPagedList(IMediator mediator, [AsParameters] GetTopUsersPagedListQuery model)
        => await mediator.Send<GetTopUsersPagedListQuery, PagedResponse<GetTopUsersPagedListResponse>>(model);

    async Task<BaseResult<Guid>> CreateUser(IMediator mediator, CreateUserCommand model)
        => await mediator.Send<CreateUserCommand, BaseResult<Guid>>(model);

    async Task<BaseResult<double>> RankUp(IMediator mediator, RankUpCommand model)
        => await mediator.Send<RankUpCommand, BaseResult<double>>(model);

}
