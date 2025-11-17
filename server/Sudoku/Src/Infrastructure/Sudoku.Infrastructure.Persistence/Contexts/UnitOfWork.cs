using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Sudoku.Application.DTOs;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Interfaces.Repositories;
using Sudoku.Application.Parameters;
using Sudoku.Domain.Entities;

namespace Sudoku.Infrastructure.Persistence.Contexts
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext DbContext { get; }

        public IRepository<User> Users { get; }
        public IRepository<Role> Roles { get; }
        public IRepository<UserGame> UserGames { get; }
        public IRepository<Game> Games { get; }

        public UnitOfWork(
            ApplicationDbContext dbContext,
            IRepository<User> users,
            IRepository<Role> roles,
            IRepository<UserGame> userGames,
            IRepository<Game> games
            )
        {
            DbContext = dbContext;
            Users = users;
            Roles = roles;
            UserGames = userGames;
            Games = games;
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await DbContext.SaveChangesAsync() > 0;
        }
        public bool SaveChanges()
        {
            return DbContext.SaveChanges() > 0;
        }
        public async Task<PaginationResponseDto<TEntity>> Paged<TEntity>(IQueryable<TEntity> query, PaginationRequestParameter request) where TEntity : class
        {
            int count = await query.CountAsync();

            System.Collections.Generic.List<TEntity> pagedResult = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .AsNoTracking()
                .ToListAsync();

            return new(pagedResult, count, request.PageNumber, request.PageSize);
        }

    }
}
