using Domain.Domains;

namespace Application.Abstractions.Services.Repositories;

public interface IDepartmentRepository
{
    public Task<Department?> GetById(int id, CancellationToken token);
    public Task<List<Department>> GetAll(CancellationToken token);
}
