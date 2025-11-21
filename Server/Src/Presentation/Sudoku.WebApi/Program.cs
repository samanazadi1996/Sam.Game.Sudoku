using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Sudoku.Application;
using Sudoku.Application.Interfaces;
using Sudoku.Infrastructure.Persistence;
using Sudoku.Infrastructure.Persistence.Contexts;
using Sudoku.Infrastructure.Persistence.Seeds;
using Sudoku.WebApi.Infrastructure.Extensions;
using Sudoku.WebApi.Infrastructure.Middlewares;
using Sudoku.WebApi.Infrastructure.Services;


var builder = WebApplication.CreateBuilder(args);

bool useInMemoryDatabase = builder.Configuration.GetValue<bool>("UseInMemoryDatabase");

builder.Services.AddApplicationLayer();
builder.Services.AddSqlPersistenceInfrastructure(builder.Configuration, useInMemoryDatabase);
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<ICryptographyServices, CryptographyServices>();
builder.Services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
builder.Services.AddMediator();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCustomSwagger();
builder.Services.AddAnyCors();
builder.Services.AddAuthorization();
builder.Services.AddCustomLocalization(builder.Configuration);
builder.Services.AddHealthChecks();
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    if (!useInMemoryDatabase)
    {
        await services.GetRequiredService<ApplicationDbContext>().Database.MigrateAsync();
    }

    //Seed Data
    await DefaultData.SeedAsync(services.GetRequiredService<ApplicationDbContext>());
}

app.UseCustomLocalization();
app.UseAnyCors();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCustomSwagger();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHealthChecks("/health");
app.MapEndpoints();
app.UseSerilogRequestLogging();
app.UseStaticFiles();

app.Run();

public partial class Program
{
}
