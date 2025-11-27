using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;
using Sudoku.Domain.Enums;

namespace Sudoku.Application.Features.UserGames.Commands.CreateGame;

public class CreateGameCommand : IRequest<BaseResult>
{
    public GameLevel GameLevel { get; set; }
}