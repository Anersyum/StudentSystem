using Domain.Domains;

namespace Domain.Abstractions.Interfaces;

public interface IDepartmentRepository
{
    public Task<Department> GetById(int id);
    public Task<List<Department?>> GetAll();
}
