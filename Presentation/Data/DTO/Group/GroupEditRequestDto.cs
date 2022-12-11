using System.ComponentModel.DataAnnotations;

namespace Presentation.Data.DTO.Group;

public sealed class GroupEditRequestDto
{
    [Required]
    public int Id { get; set; }
    [Required, MaxLength(100)]
    public string Name { get; set; }
}
