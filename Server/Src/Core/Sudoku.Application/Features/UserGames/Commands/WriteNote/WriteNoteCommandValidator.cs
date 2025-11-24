using Sudoku.Application.Interfaces;
using FluentValidation;

namespace Sudoku.Application.Features.UserGames.Commands.WriteNote;

public class WriteNoteCommandValidator : AbstractValidator<WriteNoteCommand>
{
    public WriteNoteCommandValidator()
    {
        RuleFor(p => p.Row)
            .NotEmpty().LessThan(10).GreaterThan(0);

        RuleFor(p => p.Col)
            .NotEmpty().LessThan(10).GreaterThan(0);

        RuleFor(p => p.Number)
            .NotEmpty().LessThan(10).GreaterThan(0);
    }
}