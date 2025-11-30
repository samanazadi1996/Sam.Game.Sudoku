namespace Sudoku.Application.Features.Users.Queries.GetTopPlayersPagedList;

public class GetTopPlayersPagedListResponse
{
    public string UserName { get; set; }
    public int CountEndedSucceess { get; set; }
    public double Level { get; set; }
    public string NickName { get; set; }
    public string ProfileImage { get; set; }
}