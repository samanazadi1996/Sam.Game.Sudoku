using Microsoft.AspNetCore.Http;
using Sudoku.Application.Interfaces;
using System;
using System.Linq;
using System.Security.Claims;

namespace Sudoku.WebApi.Infrastructure.Services;

public class AuthenticatedUserService : IAuthenticatedUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public Guid GetUserId()
    {
        var uid = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(uid))
            return Guid.Empty;

        return Guid.Parse(uid);
    }

    public string GetUserName() =>
        _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);

    public bool IsInRole(string roleName)
    {
        var roles = _httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role);

        return roles?.Any(p => p.Value == roleName) ?? false;
    }
}
