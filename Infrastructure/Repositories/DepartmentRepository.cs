using Application.Abstractions.Services.Repositories;
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

    public async Task<List<Department>> GetAll(CancellationToken token)
    {
        var departments = await _dataContext.Departments.Include(d => d.GroupsWithAverages).ToListAsync(token);

        foreach (var department in departments)
        {
            bool hasNoGroups = department.GroupsWithAverages is null || department.GroupsWithAverages.Count == 0;
            
            department.AcademicAverage = Math.Round(
                hasNoGroups ? 0 : department.GroupsWithAverages!.Average(g => g.AcademicAverage),
                2);
        }

        return departments;
    }

    public async Task<Department?> GetById(int id, CancellationToken token)
    {
        var department = await _dataContext.Departments
            .Where(d => d.Id == id)
            .Include(d => d.GroupsWithAverages)
            .FirstOrDefaultAsync(token);

        if (department is not null)
        {
            bool hasNoGroups = department.GroupsWithAverages is null || department.GroupsWithAverages.Count == 0;

            department.AcademicAverage = Math.Round(
                hasNoGroups ? 0 : department.GroupsWithAverages!.Average(g => g.AcademicAverage),
                2);
        }

        return department;
    }
}
