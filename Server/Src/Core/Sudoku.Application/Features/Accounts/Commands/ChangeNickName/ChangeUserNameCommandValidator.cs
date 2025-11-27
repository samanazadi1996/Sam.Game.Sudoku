using Sudoku.Application.Helpers;
using Sudoku.Application.Interfaces;
using FluentValidation;

namespace Sudoku.Application.Features.Accounts.Commands.ChangeNickName;

public class ChangeNickNameCommandValidator : AbstractValidator<ChangeNickNameCommand>
{
    public ChangeNickNameCommandValidator()
    {
        RuleFor(x => x.NickName)
            .NotEmpty()
            .MinimumLength(3)
            .WithName(p => nameof(p.NickName));

    }
}
