using Sudoku.Application.Interfaces;
using FluentValidation;

namespace Sudoku.Application.Features.UserGames.Commands.ClearColumn;

public class ClearColumnCommandValidator : AbstractValidator<ClearColumnCommand>
{
    public ClearColumnCommandValidator()
    {
        RuleFor(p => p.Row)
            .NotNull().LessThanOrEqualTo(8).GreaterThanOrEqualTo(0);

        RuleFor(p => p.Col)
            .NotNull().LessThanOrEqualTo(8).GreaterThanOrEqualTo(0);
    }
}