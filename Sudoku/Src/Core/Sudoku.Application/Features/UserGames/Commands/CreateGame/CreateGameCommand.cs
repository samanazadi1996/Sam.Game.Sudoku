using Sudoku.Application.Wrappers;
using Sudoku.Application.Interfaces;
using System;
using System.Collections.Generic;
using Sudoku.Domain.Enums;

namespace Sudoku.Application.Features.UserGames.Commands.CreateGame;

public class CreateGameCommand : IRequest<BaseResult>
{
    public GameLevel GameLevel { get; set; }
}