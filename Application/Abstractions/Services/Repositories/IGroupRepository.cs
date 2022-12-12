namespace Application.Abstractions.Services.Repositories;

public interface IGroupRepository
{
    public Task<int> Create(Domain.Domains.Group group, CancellationToken token);
    public Task<int> Update(Domain.Domains.Group group, CancellationToken token);
    public Task<bool> Delete(Domain.Domains.Group group, CancellationToken token);
    public Task<bool> Delete(int id, CancellationToken token);
    public Task<Domain.Domains.GroupWithAverages?> GetById(int id, CancellationToken token);
    public Task<List<Domain.Domains.GroupWithAverages>> GetAll(CancellationToken token);
}
