using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Sudoku.Application.Interfaces;
using Sudoku.Domain.Entities;
using Sudoku.Infrastructure.Persistence.Extensions;

namespace Sudoku.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IAuthenticatedUserService authenticatedUser) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserGame> UserGames { get; set; }
        public DbSet<Game> Games { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            ChangeTracker.ApplyAuditing(authenticatedUser);

            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (Microsoft.EntityFrameworkCore.Metadata.IMutableEntityType entityType in builder.Model.GetEntityTypes())
            {
                Microsoft.EntityFrameworkCore.Metadata.IMutableProperty idProperty = entityType.FindProperty("Id");
                if (idProperty != null)
                {
                    builder.Entity(entityType.ClrType).HasKey("Id");
                }
            }

            this.ConfigureDecimalProperties(builder);

            base.OnModelCreating(builder);
        }
    }
}