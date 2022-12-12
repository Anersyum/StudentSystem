namespace Application.Abstractions.Services.Repositories;

public interface IGroupRepository
{
    public Task<int> Create(Domain.Domains.Group group);
    public Task<int> Update(Domain.Domains.Group group);
    public Task<bool> Delete(Domain.Domains.Group group);
    public Task<bool> Delete(int id);
    public Task<Domain.Domains.GroupWithAverages?> GetById(int id);
    public Task<List<Domain.Domains.GroupWithAverages>> GetAll();
}
