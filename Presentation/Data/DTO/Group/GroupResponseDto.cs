using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Presentation.Data.DTO.Group;

public sealed class GroupResponseDto
{
    public int DepartmentId { get; set; }
    public Domain.Domains.Department Department { get; set; }
    public string Name { get; set; }
    public double AcademicAverage 
    {
        get;
        set;
    }
}
