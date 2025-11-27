using FluentValidation;
using Sudoku.Application.Helpers;

namespace Sudoku.Application.Features.Accounts.Commands.ChangeUserName;

public class ChangeUserNameCommandValidator : AbstractValidator<ChangeUserNameCommand>
{
    public ChangeUserNameCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .MinimumLength(4)
            .Matches(Regexs.UserName)
            .WithName(p => nameof(p.UserName));

    }
}
