using Sudoku.Domain.Entities;
using Sudoku.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Sudoku.Application.Features.UserGames.Queries.HasSavedGame;

public class HasSavedGameResponse
{
    public DateTime CreatedDaateTime { get; set; }
    public long Time { get; set; }
    public GameLevel Level { get; internal set; }
}