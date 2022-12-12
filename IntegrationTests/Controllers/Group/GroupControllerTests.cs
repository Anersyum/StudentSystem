using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTests.Controllers.Group;

public sealed class GroupControllerTests : IClassFixture<WebAppFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private readonly WebAppFactory<Program> _appFactory;

    public GroupControllerTests(WebAppFactory<Program> appFactory)
    {
        _httpClient = appFactory.CreateClient(new WebApplicationFactoryClientOptions()
        {
            AllowAutoRedirect = false
        });
        _appFactory = appFactory;
    }

    [Fact]
    public async Task Delete_ShouldDeleteGroup_WhenGroupHasNoStudents()
    {
        int groupId = 3;

        var response = await _httpClient.DeleteAsync($"https://localhost:7097/api/Groups/delete/{groupId}");
        response.Should().BeSuccessful();
    }

    [Fact]
    public async Task Delete_ShouldReturnBadRequest_WhenGroupHasStudents()
    {
        int groupId = 1;

        var response = await _httpClient.DeleteAsync($"https://localhost:7097/api/Groups/delete/{groupId}");
        response.Should().HaveStatusCode(System.Net.HttpStatusCode.BadRequest);
    }
}
