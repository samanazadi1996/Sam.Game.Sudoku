using Microsoft.Extensions.DependencyInjection;
using Sudoku.Application.Interfaces;
using Sudoku.Infrastructure.Resources.Services;

namespace Sudoku.Infrastructure.Resources
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddResourcesInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<ITranslator, Translator>();

            return services;
        }
    }
}
