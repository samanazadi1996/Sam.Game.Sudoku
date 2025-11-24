using Sudoku.Application.Interfaces;
using FluentValidation;

namespace Sudoku.Application.Features.UserGames.Commands.WriteNote;

public class WriteNoteCommandValidator : AbstractValidator<WriteNoteCommand>
{
    public WriteNoteCommandValidator()
    {
        RuleFor(p => p.Row)
            .NotNull().LessThanOrEqualTo(8).GreaterThanOrEqualTo(0);

        RuleFor(p => p.Col)
            .NotNull().LessThanOrEqualTo(8).GreaterThanOrEqualTo(0);

        RuleFor(p => p.Number)
            .NotNull().LessThan(10).GreaterThan(0);
    }
}