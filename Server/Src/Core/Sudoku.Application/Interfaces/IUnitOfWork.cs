using Sudoku.Application.DTOs;
using Sudoku.Application.Interfaces.Repositories;
using Sudoku.Application.Parameters;
using Sudoku.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.Application.Interfaces;

public interface IUnitOfWork
{

    public IRepository<User> Users { get; }
    public IRepository<Role> Roles { get; }
    public IRepository<UserGame> UserGames { get; }
    public IRepository<Game> Games { get; }

    Task<bool> SaveChangesAsync();
    Task<PaginationResponseDto<TEntity>> Paged<TEntity>(IQueryable<TEntity> query, PaginationRequestParameter request) where TEntity : class;
}