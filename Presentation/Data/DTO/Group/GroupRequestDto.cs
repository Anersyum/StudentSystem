using Domain.Domains;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Data.DTO.Group;

public sealed class GroupRequestDto
{
    [Required]
    public int DepartmentId { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }
}
