using System.ComponentModel.DataAnnotations;

namespace Presentation.Data.DTO.Student;

public sealed class StudentToEditRequestDto
{
    [Required]
    public int Id { get; set; }
    [Required, MaxLength(100)]
    public string FirstName { get; set; }

    [Required, MaxLength(100)]
    public string LastName { get; set; }

    [Required, MaxLength(30), EmailAddress]
    public string Email { get; set; }

    [Required]
    public int AcademicPerformance { get; set; }

    [Required]
    public int GroupId { get; set; }
}
