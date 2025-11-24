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
            .NotEmpty().LessThan(10).GreaterThan(0);

        RuleFor(p => p.Col)
            .NotEmpty().LessThan(10).GreaterThan(0);

        RuleFor(p => p.Number)
            .NotEmpty().LessThan(10).GreaterThan(0);
    }
}