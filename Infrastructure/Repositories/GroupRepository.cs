using Application.Abstractions.Services.Repositories;
using Domain.Domains;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class GroupRepository : IGroupRepository
{
    private readonly DataContext _dataContext;

    public GroupRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<int> Create(Group group, CancellationToken token)
    {
        if (!_dataContext.Departments.Any(department => department.Id == group.DepartmentId))
            return -1;

        await _dataContext.Groups.AddAsync(group, token);
        await _dataContext.SaveChangesAsync(token);

        return group.Id;
    }

    public async Task<bool> Delete(Group group, CancellationToken token)
    {
        var entity = _dataContext.Groups.Remove(group);
        
        if (entity is null)
            return false;

        await _dataContext.SaveChangesAsync(token);
        
        return true;
    }

    public async Task<bool> Delete(int id, CancellationToken token)
    {
        var group = await _dataContext.Groups.FirstOrDefaultAsync(g => g.Id == id, token);

        if (group is null)
            return false;
        
        _dataContext.Groups.Remove(group);
        await _dataContext.SaveChangesAsync(token);

        return true;
    }

    public async Task<List<GroupWithAverages>> GetAll(CancellationToken token)
    {
        var groups =  await _dataContext.GroupsWithAverages.ToListAsync(token);

        return groups;
    }

    public async Task<GroupWithAverages?> GetById(int id, CancellationToken token)
    {
        return await _dataContext.GroupsWithAverages.FirstOrDefaultAsync(g => g.Id == id, token);
    }

    public async Task<int> Update(Group group, CancellationToken token)
    {
        if (group == null)
            return 0;

        _dataContext.Groups.Update(group);
        await _dataContext.SaveChangesAsync(token);

        return group.Id;
    }
}
