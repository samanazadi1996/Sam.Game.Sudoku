using FluentValidation;

namespace Sudoku.Application.Features.UserGames.Commands.CreateGame;

public class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
{
    public CreateGameCommandValidator()
    {
        RuleFor(p => p.GameLevel)
            .IsInEnum();
    }
}