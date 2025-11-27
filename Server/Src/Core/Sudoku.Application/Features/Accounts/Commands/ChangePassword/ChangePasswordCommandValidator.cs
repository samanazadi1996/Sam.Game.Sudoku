using FluentValidation;
using Sudoku.Application.Helpers;

namespace Sudoku.Application.Features.Accounts.Commands.ChangePassword;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6)
            .Matches(Regexs.Password)
            .WithName(p => nameof(p.Password));

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
            .Matches(Regexs.Password)
            .WithName(p => nameof(p.ConfirmPassword));

    }
}
