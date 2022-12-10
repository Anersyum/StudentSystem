using Domain.Abstractions.Interfaces;
using Domain.Domains;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class DepartmentRepository : IDepartmentRepository
{
    private readonly DataContext _dataContext;

    public DepartmentRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<List<Department>> GetAll()
    {
        var departments = await _dataContext.Departments.Include(d => d.Groups).ToListAsync();

        foreach (var department in departments)
        {
            department.AcademicAverage = 
                department.Groups is null ? 0 : department.Groups.Average(g => g.AcademicAverage);
        }

        return departments;
    }

    public async Task<Department?> GetById(int id)
    {
        var department = await _dataContext.Departments
            .Where(d => d.Id == id)
            .Include(d => d.Groups)
            .FirstOrDefaultAsync();

        if (department is not null)
        {
            department.AcademicAverage = 
                department.Groups is null ? 0 : department.Groups.Average(g => g.AcademicAverage);
        }

        return department;
    }
}
