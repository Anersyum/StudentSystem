﻿using Domain.Domains;

namespace Application.Abstractions.Services.Repositories;

public interface IStudentRepository
{
    public Task<int> Create(Student student);
    public Task<int> Update(Student student);
    public Task<bool> Delete(Student student);
    public Task<bool> DeleteById(int id);
    public Task<Student?> GetById(int id);
    public Task<List<Student>> GetByGroupId(int groupId);
    public Task<List<Student>> GetByGroup(Domain.Domains.Group group);
    public Task<List<Student>> GetByDepartmentId(int departmentId);
    public Task<List<Student>> GetByDepartment(Department department);
    public Task<List<Student>> GetAll();
}