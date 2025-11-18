using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sudoku.Application.DTOs;
using Sudoku.Application.DTOs.Account.Requests;
using Sudoku.Application.DTOs.Account.Responses;
using Sudoku.Application.Interfaces.UserInterfaces;
using Sudoku.Application.Wrappers;
using Sudoku.Infrastructure.Identity.Contexts;

namespace Sudoku.Infrastructure.Identity.Services
{
    public class GetUserServices(IdentityContext identityContext) : IGetUserServices
    {
        public async Task<PagedResponse<UserDto>> GetPagedUsers(GetAllUsersRequest model)
        {
            var skip = (model.PageNumber - 1) * model.PageSize;

            var users = identityContext.Users
                .Select(p => new UserDto()
                {
                    Name = p.Name,
                    Email = p.Email,
                    UserName = p.UserName,
                    PhoneNumber = p.PhoneNumber,
                    Id = p.Id,
                    Created = p.Created,
                });

            if (!string.IsNullOrEmpty(model.Name))
            {
                users = users.Where(p => p.Name.Contains(model.Name));
            }

            return new PaginationResponseDto<UserDto>(
                await users.Skip(skip).Take(model.PageSize).ToListAsync(),
                await users.CountAsync(),
                model.PageNumber,
                model.PageSize);

        }
    }
}
