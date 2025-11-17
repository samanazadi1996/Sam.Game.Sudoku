using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Sudoku.Application.Features.Accounts.Commands.Shared;
using System.Reflection;

namespace Sudoku.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient<AccountSharedService>();

            return services;
        }
    }
}
