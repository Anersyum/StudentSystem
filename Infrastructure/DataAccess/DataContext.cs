using Domain.Domains;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Infrastructure.DataAccess;

public sealed class DataContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Department> Departments { get; set; }

    public DataContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // probably seed the data

        modelBuilder.Entity<Student>()
            .HasOne(s => s.Group)
            .WithMany(g => g.Students)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Student>().ToTable(tb => tb.HasTrigger("UpdateAcademicAverage"));

        var jsonFile = File.ReadAllText("Students.json");
        List<Student> students = JsonSerializer.Deserialize<List<Student>>(jsonFile)!;

        List<Group> groups = new()
        {
            new()
            {
                Id = 1,
                Name = "TestA",
                DepartmentId = 1
            },
            new()
            {
                Id = 2,
                Name = "TestB",
                DepartmentId = 2
            }
            ,
            new()
            {
                Id = 3,
                Name = "TestC",
                DepartmentId = 1
            }
        };

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

        modelBuilder.Entity<Group>()
            .HasData(groups);

        modelBuilder.Entity<Student>()
            .HasData(students);
    }
}
