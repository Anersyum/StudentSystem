using Application.Abstractions.Services.Group;
using Application.Abstractions.Services.Repositories;

namespace Application.Services.Group;

public sealed class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepository;

    public GroupService(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<bool> GroupIsInSameDepartment(int oldGroupId, int newGroupId, CancellationToken token)
    {
        var oldDepartmentId = (await _groupRepository.GetById(oldGroupId, token))?.DepartmentId;
        var newDepartmentId = (await _groupRepository.GetById(newGroupId, token))?.DepartmentId;

        return oldDepartmentId == newDepartmentId;
    }

    public async Task<bool> GroupHasStudents(int groupId, CancellationToken token)
    {
        var group = await _groupRepository.GetById(groupId, token);

        return group?.StudentCount > 0;
    }
}
