using Domain.Abstractions.Classes;

namespace Domain.Domains;

public sealed class GroupWithAverages : Entity
{
    public string Name { get; init; }
    public int DepartmentId { get; init; }
    public double AcademicAverage { get; init; }
    public int StudentCount { get; set; }
}
