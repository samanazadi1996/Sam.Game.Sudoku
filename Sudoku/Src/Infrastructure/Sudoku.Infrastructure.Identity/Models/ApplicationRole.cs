using System;
using Microsoft.AspNetCore.Identity;

namespace Sudoku.Infrastructure.Identity.Models
{
    public class ApplicationRole(string name) : IdentityRole<Guid>(name)
    {
    }
}
