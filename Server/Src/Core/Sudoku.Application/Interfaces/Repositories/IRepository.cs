using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.Application.Interfaces.Repositories;

public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(object id);
    IQueryable<T> Get();
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}