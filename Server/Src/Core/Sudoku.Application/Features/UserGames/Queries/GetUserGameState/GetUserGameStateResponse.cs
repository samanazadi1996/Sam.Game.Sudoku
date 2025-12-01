using Sudoku.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Sudoku.Application.Features.UserGames.Queries.GetUserGameState;

public class GetUserGameStateResponse
{

    public List<GetUserGameStateActiveStateResponse> ActiveStates { get; set; }
    public GetUserGameStateSavedGameResponse SavedGame { get; set; }
}
public class GetUserGameStateActiveStateResponse
{
    public GameLevel Level { get; set; }
    public bool IsActive { get; set; }
    public GameLevel? NeedPointLevelToUnlock { get; set; }
    public int? NeedPointToUnlock { get; set; }
}
public class GetUserGameStateSavedGameResponse
{

    public DateTime CreatedDaateTime { get; set; }
    public long Time { get; set; }
    public GameLevel Level { get; set; }
}