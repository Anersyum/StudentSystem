using Application.Abstractions.Services.Repositories;
using Domain.Domains;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class StudentRepository : IStudentRepository
{
    private readonly DataContext _dataContext;

    public StudentRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<int> Create(Student student, CancellationToken token)
    {
        if (!_dataContext.Groups.Any(group => group.Id == student.GroupId))
            return -1;

        var createdStudent = await _dataContext.Students.AddAsync(student, token);
        await _dataContext.SaveChangesAsync(token);

        return createdStudent.Entity.Id;
    }

    public async Task<bool> Delete(Student student, CancellationToken token)
    {
        return await DeleteById(student.Id, token);
    }

    public async Task<bool> DeleteById(int id, CancellationToken token)
    {
        var student = await GetById(id, token);

        if (student is null)
            return false;

        _dataContext.Students.Remove(student);
        await _dataContext.SaveChangesAsync(token);

        return true;
    }

    public async Task<List<Student>> GetAll(CancellationToken token)
    {
        return await _dataContext.Students.ToListAsync(token);
    }

    public async Task<List<Student>> GetByDepartment(Department department, CancellationToken token)
    {
        return await GetByDepartmentId(department.Id, token);
    }

    public async Task<List<Student>> GetByDepartmentId(int departmentId, CancellationToken token)
    {
        var groups = await _dataContext.Groups
            .Where(g => g.DepartmentId == departmentId)
            .Select(g => g.Students)
            .ToListAsync(token);

        List<Student> students = new();

        foreach (var student in groups)
        {
            students.AddRange(student);
        }

        return students;
    }

    public async Task<List<Student>> GetByGroup(Group group, CancellationToken token)
    {
        return await GetByGroupId(group.Id, token);
    }

    public async Task<List<Student>> GetByGroupId(int groupId, CancellationToken token)
    {
        return await _dataContext.Students.Where(s => s.GroupId == groupId).ToListAsync(token);
    }

    public async Task<Student?> GetById(int id, CancellationToken token)
    {
        return await _dataContext.Students.FirstOrDefaultAsync(s => s.Id == id, token);
    }

    public async Task<int> Update(Student student, CancellationToken token)
    {
        _dataContext.Students.Update(student);
        await _dataContext.SaveChangesAsync(token);

        return student.Id;
    }
}
