using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Presentation.Data.DTO.Student;

namespace IntegrationTests.Controllers.Student;

public sealed class StudentControllerTests : IClassFixture<WebAppFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private readonly WebAppFactory<Program> _appFactory;

    public StudentControllerTests(WebAppFactory<Program> appFactory)
    {
        _httpClient = appFactory.CreateClient(new WebApplicationFactoryClientOptions()
        {
            AllowAutoRedirect = false
        });
        _appFactory = appFactory;
    }

    [Fact]
    public async Task AssignGroup_ShouldAssignStudentToGroup_WhenGroupInSameDepartment()
    {
        AssignGroupDto request = new()
        {
            StudentId = 1,
            GroupId = 3,
        };

        var response = await _httpClient.PostAsJsonAsync("https://localhost:7097/api/Students/assignGroup", request);
        response.Should().BeSuccessful();
    }

    [Fact]
    public async Task AssignGroup_ShouldNotAssignGroup_WhenGroupIsNotInTheSameDepartment()
    {
        AssignGroupDto request = new()
        {
            StudentId = 1,
            GroupId = 2,
        };

        var response = await _httpClient.PostAsJsonAsync("https://localhost:7097/api/Students/assignGroup", request);
        response.Should().HaveStatusCode(System.Net.HttpStatusCode.BadRequest);
    }
}
