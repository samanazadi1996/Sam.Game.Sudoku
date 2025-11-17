var cureentDir = Directory.GetCurrentDirectory().Split("bin").First();

var dbContext = Path.Combine(cureentDir, "..", "..", "Src", "Infrastructure", "Sudoku.Infrastructure.Persistence", "Contexts", "ApplicationDbContext.cs");
var uow = Path.Combine(cureentDir, "..", "..", "Src", "Infrastructure", "Sudoku.Infrastructure.Persistence", "Contexts", "UnitOfWork.cs");
var iuow = Path.Combine(cureentDir, "..", "..", "Src", "Core", "Sudoku.Application", "Interfaces", "IUnitOfWork.cs");

var props = File.ReadLines(dbContext).Where(p => p.Contains("DbSet<"));


var temp = props.Select(p => p.Trim().Split(' ')).Select(p => $"{p[1].Replace("DbSet", "IRepository")} {p[2]}");


File.WriteAllText(iuow, GetiuowContent());
File.WriteAllText(uow, GetuowContent());


Console.WriteLine("Hello, World!");

string GetuowContent()
{
    var iuowProps = props.Select(p => p.Trim().Split(' ')).Select(p => $"        public {p[1].Replace("DbSet", "IRepository")} {p[2]} {{ get; }}");

    var ctor = props.Select(p => p.Trim().Split(' '))
        .Select(p => $"            {p[1].Replace("DbSet", "IRepository")} {(p[2][0] + "").ToLower()}{p[2].Substring(1)}");

    var binds = props.Select(p => p.Trim().Split(' '))
        .Select(p => $"            {p[2]} = {(p[2][0] + "").ToLower()}{p[2].Substring(1)};");

    var str = $@"using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Sudoku.Application.DTOs;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Interfaces.Repositories;
using Sudoku.Application.Parameters;
using Sudoku.Domain.Entities;

namespace Sudoku.Infrastructure.Persistence.Contexts
{{
    public class UnitOfWork : IUnitOfWork
    {{
        private ApplicationDbContext DbContext {{ get; }}

{string.Join(Environment.NewLine, iuowProps)}

        public UnitOfWork(
            ApplicationDbContext dbContext,
{string.Join("," + Environment.NewLine, ctor)}
            )
        {{
            DbContext = dbContext;
{string.Join(Environment.NewLine, binds)}
        }}
        public async Task<bool> SaveChangesAsync()
        {{
            return await DbContext.SaveChangesAsync() > 0;
        }}
        public bool SaveChanges()
        {{
            return DbContext.SaveChanges() > 0;
        }}
        public async Task<PaginationResponseDto<TEntity>> Paged<TEntity>(IQueryable<TEntity> query, PaginationRequestParameter request) where TEntity : class
        {{
            int count = await query.CountAsync();

            System.Collections.Generic.List<TEntity> pagedResult = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .AsNoTracking()
                .ToListAsync();

            return new(pagedResult, count, request.PageNumber, request.PageSize);
        }}

    }}
}}
";
    return str;
}
string GetiuowContent()
{
    var iuowProps = props.Select(p => p.Trim().Split(' ')).Select(p => $"    public {p[1].Replace("DbSet", "IRepository")} {p[2]} {{ get; }}");


    var str = @$"using System.Linq;
using System.Threading.Tasks;
using Sudoku.Application.DTOs;
using Sudoku.Application.Interfaces.Repositories;
using Sudoku.Application.Parameters;
using Sudoku.Domain.Entities;

namespace Sudoku.Application.Interfaces;

public interface IUnitOfWork
{{

{string.Join(Environment.NewLine, iuowProps)}

    Task<bool> SaveChangesAsync();
    Task<PaginationResponseDto<TEntity>> Paged<TEntity>(IQueryable<TEntity> query, PaginationRequestParameter request) where TEntity : class;
}}";

    return str;
}