using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Entities;

namespace Sudoku.Application.DTOs.DomanDtos;

public class UserDto
{
    public UserDto(User user)
    {
        Id = user.Id;
        UserName = user.UserName;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Created = user.Created;
        PhoneNumber = user.PhoneNumber;
        IsActive = user.IsActive;
        Roles = user.UserRoles?.Select(p => new RoleDto(p.Role));
    }
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Created { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public IEnumerable<RoleDto> Roles { get; set; }
}
