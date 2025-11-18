using Sudoku.Domain.Entities;
using System;

namespace Sudoku.Application.DTOs.DomanDtos;

public class RoleDto
{
    public RoleDto()
    {

    }
    public RoleDto(Role role)
    {
        Id = role.Id;
        Name = role.Name;
        Title = role.Title;
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
}
