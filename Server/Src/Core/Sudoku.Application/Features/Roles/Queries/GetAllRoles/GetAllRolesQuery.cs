using System.Collections.Generic;
using Sudoku.Application.DTOs.DomanDtos;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;

namespace Sudoku.Application.Features.Roles.Queries.GetAllRoles;

public class GetAllRolesQuery : IRequest<BaseResult<List<RoleDto>>>
{
}