// See https://aka.ms/new-console-template for more information


var cureentDir = Directory.GetCurrentDirectory().Split("bin").First();

var dbContext = Path.Combine(cureentDir, "..", "..", "Src", "Infrastructure", "Sudoku.Infrastructure.Persistence", "Contexts", "ApplicationDbContext.cs");
var uow = Path.Combine(cureentDir, "..", "..", "Src", "Infrastructure", "Sudoku.Infrastructure.Persistence", "Contexts", "UnitOfWork.cs");
var iuow = Path.Combine(cureentDir, "..", "..", "Src", "Core", "Sudoku.Application", "Interfaces", "IUnitOfWork.cs");

var props = File.ReadLines(dbContext).Where(p => p.Contains("DbSet<"));

var iuowProps = props.Select(p => p.Trim().Split(' ')).Select(p => $"   public {p[1].Replace("DbSet", "IRepository")} {p[2]} {{ get; }}");

var temp = props.Select(p => p.Trim().Split(' ')).Select(p => $"{p[1].Replace("DbSet", "IRepository")} {p[2]}");


File.WriteAllText(iuow, GetiuowContent(iuowProps));


Console.WriteLine("Hello, World!");

string GetiuowContent(IEnumerable<string> props)
{
    var str = @$"using System.Linq;
using System.Threading.Tasks;
using Sudoku.Application.DTOs;
using Sudoku.Application.Interfaces.Repositories;
using Sudoku.Application.Parameters;
using Sudoku.Domain.Entities;

namespace Sudoku.Application.Interfaces;

public interface IUnitOfWork
{{

{string.Join(Environment.NewLine, props)}

    Task<bool> SaveChangesAsync();
    Task<PaginationResponseDto<TEntity>> Paged<TEntity>(IQueryable<TEntity> query, PaginationRequestParameter request) where TEntity : class;
}}";

    return str;
}