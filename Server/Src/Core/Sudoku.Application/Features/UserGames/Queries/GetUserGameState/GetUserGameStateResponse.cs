using Sudoku.Domain.Enums;
using System;

namespace Sudoku.Application.Features.UserGames.Queries.GetUserGameState;

public class GetUserGameStateResponse
{
    public GetUserGameStateSavedGameResponse SavedGame { get;  set; }
}
public class GetUserGameStateSavedGameResponse
{

    public DateTime CreatedDaateTime { get; set; }
    public long Time { get; set; }
    public GameLevel Level { get; set; }
}