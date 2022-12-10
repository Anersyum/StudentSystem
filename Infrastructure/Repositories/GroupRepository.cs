using Domain.Abstractions.Interfaces;
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

    public async Task<int> Create(Group group)
    {
        _dataContext.Groups.Add(group);
        await _dataContext.SaveChangesAsync();

        return group.Id;
    }

    public async Task<bool> Delete(Group group)
    {
        var entity = _dataContext.Groups.Remove(group);
        
        if (entity is null)
            return false;

        await _dataContext.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var group = await _dataContext.Groups.FirstOrDefaultAsync(g => g.Id == id);

        if (group is null)
            return false;
        
        _dataContext.Groups.Remove(group);
        await _dataContext.SaveChangesAsync();

        return true;
    }

    public async Task<List<Group>> GetAll()
    {
        return await _dataContext.Groups.ToListAsync();
    }

    public async Task<Group?> GetById(int id)
    {
        return await _dataContext.Groups.FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<int> Update(Group group)
    {
        if (group == null)
            return 0;

        _dataContext.Groups.Update(group);
        await _dataContext.SaveChangesAsync();

        return group.Id;
    }
}
