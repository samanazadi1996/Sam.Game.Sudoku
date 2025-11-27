using Sudoku.Domain.Enums;
using System.Collections.Generic;

namespace Sudoku.Application.Features.Accounts.Queries.GetProfile;

public class GetProfileResponse
{
    public bool Self { get; set; }
    public string NickName { get; set; }
    public int Age { get; set; }
    public double Level { get; set; }
    public string UserName { get; set; }
    public string ProfileImage { get; set; }
    public List<GetProfileRespotrGamesResponse> ReportGames { get; set; }
}
public class GetProfileRespotrGamesResponse
{
    public GameLevel GameLevel { get; set; }
    public int EndedSucceess { get; set; }
    public int EndedFailed { get; set; }
    public int Inactive { get; set; }
}