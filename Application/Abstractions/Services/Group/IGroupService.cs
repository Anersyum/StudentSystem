using Application.Abstractions.Services.Repositories;

namespace Application.Abstractions.Services.Group;

public interface IGroupService
{
    public Task<bool> GroupIsInSameDepartment(int oldGroupId, int newGroupId, CancellationToken token);
    public Task<bool> GroupHasStudents(int groupId, CancellationToken token);
}
