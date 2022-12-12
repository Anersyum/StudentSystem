using Application.Abstractions.Services.Repositories;
using Application.Helpers;
using Domain.Domains;
using Microsoft.AspNetCore.Mvc;
using Presentation.Data.DTO.Department;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class DepartmentController : ControllerBase
{
    private readonly IDepartmentRepository _department;

    public DepartmentController(IDepartmentRepository department)
    {
        _department = department;
    }

    [HttpGet("getById")]
    public async Task<IActionResult> GetByIdAction(int departmentId)
    {
        var department = await _department.GetById(departmentId);

        if (department is null)
            return NotFound($"Department with the Id of {departmentId} not found.");

        DepartmentResponseDto response = new();

        ObjectMapper.Map(department, response);

        return Ok(response);
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllAction()
    {
        var departments = await _department.GetAll();

        var response = departments
            .Select(g =>
            {
                DepartmentResponseDto departmentResponseDto = new();
                ObjectMapper.Map(g, departmentResponseDto);

                return departmentResponseDto;
            })
            .ToList();

        return Ok(response);
    }
}
