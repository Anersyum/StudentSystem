using Application.Helpers;
using Domain.Domains;
using FluentAssertions;
using Presentation.Data.DTO.Student;

namespace Tests.UnitTests.Helpers;

public sealed class ObjectMapperTest
{
    [Fact]
    public void Map_ShouldMapToObject_WhenObjectToMapFromIsProvided()
    {
        Student student = new()
        {
            FirstName = "TestSubject",
            LastName = "TestLastName",
            Email = "SomeTestEmail@email.com",
            GroupId = 1,
            AcademicPerformance = 2
        };

        StudentResponseDto response = new();

        ObjectMapper.Map(student, response);

        student.FirstName.Should().BeEquivalentTo(response.FirstName);
        student.LastName.Should().BeEquivalentTo(response.LastName);
        student.GroupId.Should().Be(response.GroupId);
        student.AcademicPerformance.Should().Be(response.AcademicPerformance);
        student.Email.Should().BeEquivalentTo(response.Email);
    }

    [Fact]
    public void Map_ShouldThrowException_WhenOneObjectIsNull()
    {
        Student student = new()
        {
            FirstName = "TestSubject",
            LastName = "TestLastName",
            Email = "SomeTestEmail@email.com",
            GroupId = 1,
            AcademicPerformance = 2
        };

        StudentResponseDto response = null;

        Action action = () => ObjectMapper.Map(student, response);

        action.Should().Throw<ArgumentNullException>();
    }
}