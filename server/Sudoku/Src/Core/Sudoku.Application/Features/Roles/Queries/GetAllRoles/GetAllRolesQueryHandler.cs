using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sudoku.Application.DTOs.DomanDtos;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;

namespace Sudoku.Application.Features.Roles.Queries.GetAllRoles;

public class GetAllRolesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllRolesQuery, BaseResult<List<RoleDto>>>
{
    public async Task<BaseResult<List<RoleDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {

        return await unitOfWork.Roles.Get().Select(p => new RoleDto(p)).ToListAsync();
    }
}