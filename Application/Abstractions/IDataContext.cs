using Domain.Domains;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions;

public interface IDataContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<GroupWithAverages> GroupsWithAverages { get; set; }
}
