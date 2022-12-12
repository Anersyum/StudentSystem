using Domain.Abstractions.Classes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Domains;

public sealed class Group : Entity
{
    [Required]
    public int DepartmentId { get; set; }
    public Department Department { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }
    public List<Student> Students { get; set; } = new(); 
}
