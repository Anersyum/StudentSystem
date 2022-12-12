using Domain.Abstractions.Classes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Domains;

public sealed class Student : Entity
{
    [Required, MaxLength(100)]
    public string FirstName { get; set; }

    [Required, MaxLength(100)]
    public string LastName { get; set; }

    [Required, MaxLength(50), EmailAddress, Column(TypeName = "varchar(50)")]
    public string Email { get; set; }

    [Required, Range(1, 5)]
    public double AcademicPerformance { get; set; }

    [Required]
    public int GroupId { get; set; }
    [NotMapped]
    public Group Group { get; set; }
}
