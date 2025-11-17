using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sudoku.Domain.Entities;
using System.Collections.Generic;
using System.Text.Json;

namespace Sudoku.Infrastructure.Persistence.Contexts.Configurations;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {

        builder.Property(g => g.Level)
               .IsRequired();

        builder.Property(g => g.Description)
            .IsRequired(false)
               .HasMaxLength(500);

        builder.Property(g => g.Data)
               .HasConversion(
                    v => JsonSerializer.Serialize(v),
                    v => JsonSerializer.Deserialize<List<List<SudokuCell>>>(v) ?? new List<List<SudokuCell>>()
                );
    }
}
public class UserGameConfiguration : IEntityTypeConfiguration<UserGame>
{
    public void Configure(EntityTypeBuilder<UserGame> builder)
    {

        builder.Property(g => g.Data)
               .HasConversion(
                    v => JsonSerializer.Serialize(v),
                    v => JsonSerializer.Deserialize<List<List<SudokuCell>>>(v) ?? new List<List<SudokuCell>>()
                );
    }
}
