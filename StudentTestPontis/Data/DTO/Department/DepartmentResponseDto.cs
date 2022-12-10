using System.Text.Json.Serialization;

namespace StudentTestPontis.Data.DTO.Department;

public sealed class DepartmentResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double AcademicAverage { get; set; }
}
