using System;

namespace Sudoku.Application.Interfaces;

public interface IAuthenticatedUserService
{
    Guid GetUserId();
    string GetUserName();
    bool IsInRole(string roleName);

}
