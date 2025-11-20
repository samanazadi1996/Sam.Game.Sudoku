//using Sudoku.Application.Interfaces;
//using FluentValidation;

//namespace Sudoku.Application.Features.UserGames.Commands.Check;

//public class CheckCommandValidator : AbstractValidator<CheckCommand>
//{
//    public CheckCommandValidator(ITranslator translator)
//    {
//        RuleFor(p => p.MyProperty)
//            .NotNull()
//            .WithName(p => translator[nameof(p.MyProperty)]);
//    }
//}