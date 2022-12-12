using Domain.Abstractions.Classes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Domains;

public sealed class Department : Entity
{
    [Required, MaxLength(100)]
    public string Name { get; set; }
    public List<Group> Groups { get; set; } = new();
    public List<GroupWithAverages> GroupsWithAverages { get; set; } = new();
    [NotMapped]
    public double AcademicAverage { get; set; }
}
