using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sudoku.Domain.Entities;

namespace Sudoku.Infrastructure.Persistence.Contexts.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.UserName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.NickName)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(x => x.PhoneNumber)
            .IsRequired(false)
            .HasMaxLength(20);

        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.SecurityStamp)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.HasMany(x => x.UserRoles)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
