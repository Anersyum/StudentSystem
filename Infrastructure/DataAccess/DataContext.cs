using Application.Abstractions;
using Domain.Domains;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Infrastructure.DataAccess;

public sealed class DataContext : DbContext, IDataContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<GroupWithAverages> GroupsWithAverages { get; set; }

    public DataContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // probably seed the data

        modelBuilder.Entity<Student>()
            .HasOne(s => s.Group)
            .WithMany(g => g.Students)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<GroupWithAverages>()
            .ToView("GroupsWithAverages")
            .HasKey(g => g.Id);

        List<Department> departments = new()
        {
            new()
            {
                Id = 1,
                Name = "DepartmentA"
            },
            new()
            {
                Id = 2,
                Name = "DepartmentB"
            }
        };

        modelBuilder.Entity<Department>()
            .HasData(departments);
    }
}
