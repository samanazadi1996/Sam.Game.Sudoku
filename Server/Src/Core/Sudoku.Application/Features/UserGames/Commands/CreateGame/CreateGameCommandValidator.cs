using Sudoku.Application.Interfaces;
using FluentValidation;
using Sudoku.Domain.Enums;

namespace Sudoku.Application.Features.UserGames.Commands.CreateGame;

public class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
{
    public CreateGameCommandValidator()
    {
        RuleFor(p => p.GameLevel)
            .IsInEnum();
    }
}