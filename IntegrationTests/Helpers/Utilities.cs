using Domain.Domains;
using Infrastructure.DataAccess;

namespace IntegrationTests.Helpers;

public static class Utilities
{
    private static void InitializeDbForTests(DataContext db)
    {
        db.Departments.AddRange(GetSeedingDepartments());
        db.Groups.AddRange(GetSeedingGroups());
        db.GroupsWithAverages.AddRange(GetSeedingGroupsWithAverages());
        db.Students.AddRange(GetSeedingStudents());
        db.SaveChanges();
    }

    public static void ReinitializeDbForTests(DataContext db)
    {
        db.Students.RemoveRange(db.Students);
        db.Departments.RemoveRange(db.Departments);
        db.Groups.RemoveRange(db.Groups);
        db.GroupsWithAverages.RemoveRange(db.GroupsWithAverages);
        InitializeDbForTests(db);
    }

    public static List<Department> GetSeedingDepartments()
    {
        return new List<Department>()
            {
                new() { Id = 1, Name = "TestA" },
                new() { Id = 2, Name = "TestB" },
            };
    }

    public static List<Student> GetSeedingStudents()
    {
        return new List<Student>()
            {
                new() { Id = 1, FirstName = "Amor", LastName = "Osmic", Email = "example@gmail.com", GroupId = 1 },
                new() { Id = 2, FirstName = "TestA", LastName = "Osmic1", Email = "example@gmail.com", GroupId = 1 },
                new() { Id = 3, FirstName = "TestB", LastName = "Osmic2", Email = "example@gmail.com", GroupId = 2 },
                new() { Id = 4, FirstName = "TestC", LastName = "Osmic3", Email = "example@gmail.com", GroupId = 2 },
            };
    }

    public static List<Group> GetSeedingGroups()
    {
        return new List<Group>()
            {
                new() { Id = 1, Name = "TestGroupA", DepartmentId = 1 },
                new() { Id = 2, Name = "TestGroupB", DepartmentId = 2 },
                new() { Id = 3, Name = "TestGroupC", DepartmentId = 1 }
            };
    }

    public static List<GroupWithAverages> GetSeedingGroupsWithAverages()
    {
        return new List<GroupWithAverages>()
            {
                new() { Id = 1, Name = "TestGroupA", DepartmentId = 1, AcademicAverage = 1, StudentCount = 10 },
                new() { Id = 2, Name = "TestGroupB", DepartmentId = 2, AcademicAverage = 1, StudentCount = 5 },
                new() { Id = 3, Name = "TestGroupC", DepartmentId = 1, AcademicAverage = 1, StudentCount = 0 }
            };
    }
}
