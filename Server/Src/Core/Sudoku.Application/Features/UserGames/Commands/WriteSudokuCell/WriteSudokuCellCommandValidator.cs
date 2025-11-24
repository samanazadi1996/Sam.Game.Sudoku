using FluentValidation;
using FluentValidation.Validators;
using Sudoku.Application.Interfaces;
using System;

namespace Sudoku.Application.Features.UserGames.Commands.WriteSudokuCell;

public class WriteSudokuCellCommandValidator : AbstractValidator<WriteSudokuCellCommand>
{
    public WriteSudokuCellCommandValidator()
    {
        RuleFor(p => p.Row)
            .NotNull().LessThanOrEqualTo(8).GreaterThanOrEqualTo(0);

        RuleFor(p => p.Col)
            .NotNull().LessThanOrEqualTo(8).GreaterThanOrEqualTo(0);

        RuleFor(p => p.Number)
            .NotNull().LessThan(10).GreaterThan(0);
    }
}