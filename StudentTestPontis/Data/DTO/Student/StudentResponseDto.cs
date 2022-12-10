namespace StudentTestPontis.Data.DTO.Student;

public sealed class StudentResponseDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public int AcademicPerformance { get; set; }

    public int GroupId { get; set; }
}
