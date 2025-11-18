//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Routing;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Sudoku.Application.DTOs.DomanDtos;
//using Sudoku.Application.Features.Roles.Queries.GetAllRoles;
//using Sudoku.Application.Interfaces;
//using Sudoku.Application.Wrappers;
//using Sudoku.WebApi.Infrastructure.Extensions;

//namespace Sudoku.WebApi.Endpoints;

//public class RoleEndpoint : EndpointGroupBase
//{
//    public override void Map(RouteGroupBuilder builder)
//    {
//        builder.MapGet(GetAllRoles).RequireAuthorization();

//    }

//    async Task<BaseResult<List<RoleDto>>> GetAllRoles(IMediator mediator, [AsParameters] GetAllRolesQuery model)
//        => await mediator.Send<GetAllRolesQuery, BaseResult<List<RoleDto>>>(model);

//}