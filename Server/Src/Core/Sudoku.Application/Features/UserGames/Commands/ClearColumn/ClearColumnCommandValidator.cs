using Sudoku.Application.Interfaces;
using FluentValidation;

namespace Sudoku.Application.Features.UserGames.Commands.ClearColumn;

public class ClearColumnCommandValidator : AbstractValidator<ClearColumnCommand>
{
    public ClearColumnCommandValidator()
    {
        RuleFor(p => p.Row)
            .NotEmpty().LessThan(10).GreaterThan(0);

        RuleFor(p => p.Col)
            .NotEmpty().LessThan(10).GreaterThan(0);
    }
}