using Domain.Domains;

namespace Domain.Abstractions.Interfaces;

public interface IGroupRepository
{
    public Task<int> Create(Group group);
    public Task<int> Update(Group group);
    public Task<bool> Delete(Group group);
    public Task<bool> Delete(int id);
    public Task<Group?> GetById(int id);
    public Task<List<Group>> GetAll();

}
