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

    public async Task<int> Create(Student student)
    {
        if (_dataContext.Groups.Any(group => group.Id == student.GroupId))
            return -1;

        var createdStudent = await _dataContext.Students.AddAsync(student);
        await _dataContext.SaveChangesAsync();

        return createdStudent.Entity.Id;
    }

    public async Task<bool> Delete(Student student)
    {
        return await DeleteById(student.Id);
    }

    public async Task<bool> DeleteById(int id)
    {
        var student = await GetById(id);

        if (student is null)
            return false;

        _dataContext.Students.Remove(student);
        await _dataContext.SaveChangesAsync();

        return true;
    }

    public async Task<List<Student>> GetAll()
    {
        return await _dataContext.Students.ToListAsync();
    }

    public async Task<List<Student>> GetByDepartment(Department department)
    {
        return await GetByDepartmentId(department.Id);
    }

    public async Task<List<Student>> GetByDepartmentId(int departmentId)
    {
        var groups = await _dataContext.Groups
            .Where(g => g.DepartmentId == departmentId)
            .Select(g => g.Students)
            .ToListAsync();

        List<Student> students = new();

        foreach (var student in groups)
        {
            students.AddRange(student);
        }

        return students;
    }

    public async Task<List<Student>> GetByGroup(Group group)
    {
        return await GetByGroupId(group.Id);
    }

    public async Task<List<Student>> GetByGroupId(int groupId)
    {
        return await _dataContext.Students.Where(s => s.GroupId == groupId).ToListAsync();
    }

    public async Task<Student?> GetById(int id)
    {
        return await _dataContext.Students.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<int> Update(Student student)
    {
        _dataContext.Students.Update(student);
        await _dataContext.SaveChangesAsync();

        return student.Id;
    }
}
