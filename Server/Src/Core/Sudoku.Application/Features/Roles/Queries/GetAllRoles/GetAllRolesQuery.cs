using Sudoku.Application.DTOs.DomanDtos;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;
using System.Collections.Generic;

namespace Sudoku.Application.Features.Roles.Queries.GetAllRoles;

public class GetAllRolesQuery : IRequest<BaseResult<List<RoleDto>>>
{
}