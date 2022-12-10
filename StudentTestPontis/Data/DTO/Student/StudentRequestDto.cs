﻿using System.ComponentModel.DataAnnotations;

namespace StudentTestPontis.Data.DTO.Student;

public sealed class StudentRequestDto
{
    [Required, MaxLength(100)]
    public string FirstName { get; set; }

    [Required, MaxLength(100)]
    public string LastName { get; set; }

    [Required, MaxLength(30), EmailAddress]
    public string Email { get; set; }

    [Required, Range(1, 5)]
    public int AcademicPerformance { get; set; }

    [Required]
    public int GroupId { get; set; }
}
