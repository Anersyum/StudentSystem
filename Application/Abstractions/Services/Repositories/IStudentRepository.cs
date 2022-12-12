using Domain.Domains;

namespace Application.Abstractions.Services.Repositories;

public interface IStudentRepository
{
    public Task<int> Create(Student student, CancellationToken token);
    public Task<int> Update(Student student, CancellationToken token);
    public Task<bool> Delete(Student student, CancellationToken token);
    public Task<bool> DeleteById(int id, CancellationToken token);
    public Task<Student?> GetById(int id, CancellationToken token);
    public Task<List<Student>> GetByGroupId(int groupId, CancellationToken token);
    public Task<List<Student>> GetByGroup(Domain.Domains.Group group, CancellationToken token);
    public Task<List<Student>> GetByDepartmentId(int departmentId, CancellationToken token);
    public Task<List<Student>> GetByDepartment(Department department, CancellationToken token);
    public Task<List<Student>> GetAll(CancellationToken token);
}
