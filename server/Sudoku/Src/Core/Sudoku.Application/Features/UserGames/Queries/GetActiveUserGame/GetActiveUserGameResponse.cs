using Sudoku.Domain.Entities;
using Sudoku.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Sudoku.Application.Features.UserGames.Queries.GetActiveUserGame;

public class GetActiveUserGameResponse
{
    public DateTime CreatedDaateTime { get; set; }
    public long Time { get; set; }
    public int Wrong { get; set; }
    public int Hint { get; set; }
    public List<List<SudokuCell>> Data { get; set; }
    public GameLevel Level { get; internal set; }
}