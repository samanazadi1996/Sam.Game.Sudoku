using System;
using System.Collections.Generic;

namespace Sudoku.Application.DTOs.DomanDtos;

public class AuthenticationResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public IList<string> Roles { get; set; }
    public string JwToken { get; set; }
    public string ProfileImage { get; set; }
}