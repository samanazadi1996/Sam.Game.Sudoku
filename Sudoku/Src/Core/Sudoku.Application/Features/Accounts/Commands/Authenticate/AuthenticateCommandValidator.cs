using FluentValidation;
using Sudoku.Application.Helpers;

namespace Sudoku.Application.Features.Accounts.Commands.Authenticate;

public class AuthenticateCommandValidator : AbstractValidator<AuthenticateCommand>
{
    public AuthenticateCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithName(p => nameof(p.UserName));

        RuleFor(x => x.Password)
            .NotEmpty()
            .Matches(Regexs.Password)
            .WithName(p => nameof(p.Password));
    }
}