using FluentValidation;
using Sudoku.Application.Helpers;

namespace Sudoku.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        // UserName
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("نام کاربری نمی‌تواند خالی باشد.")
            .MaximumLength(50)
            .WithMessage("نام کاربری نمی‌تواند بیش از ۵۰ کاراکتر باشد.")
            .WithName(p => nameof(p.UserName));

        // Password
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("رمز عبور نمی‌تواند خالی باشد.")
            .Matches(Regexs.Password)
            .WithMessage("رمز عبور باید شامل حروف بزرگ، حروف کوچک، عدد و کاراکتر ویژه باشد.")
            .WithName(p => nameof(p.Password));

        // PhoneNumber
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("شماره موبایل نمی‌تواند خالی باشد.")
            .Matches(Regexs.PhoneNumber)
            .WithMessage("شماره موبایل وارد شده معتبر نمی‌باشد.")
            .WithName(p => nameof(p.PhoneNumber));

        // Roles
        RuleFor(x => x.Roles)
            .NotNull()
            .WithMessage("نقش کاربر نمی‌تواند خالی باشد.")
            .WithName(p => nameof(p.Roles));

        // IsActive
        RuleFor(x => x.IsActive)
            .NotNull()
            .WithMessage("وضعیت فعال بودن کاربر باید مشخص شود.")
            .WithName(p => nameof(p.IsActive));
    }
}