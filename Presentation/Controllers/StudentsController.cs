using Application.Abstractions.Services.Group;
using Application.Abstractions.Services.Repositories;
using Application.Helpers;
using Domain.Domains;
using Microsoft.AspNetCore.Mvc;
using Presentation.Data.DTO.Student;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class StudentsController : ControllerBase
{
    private readonly IStudentRepository _studentRepository;

    public StudentsController(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAction([FromBody] StudentRequestDto studentRequest, CancellationToken token)
    {
        Student student = new();

        ObjectMapper.Map(studentRequest, student);

        int id = await _studentRepository.Create(student, token);

        if (id == -1)
            return BadRequest("Assigned group doesn't exits");

        return CreatedAtAction(nameof(CreateAction), id);
    }

    [HttpPost("edit")]
    public async Task<IActionResult> EditAction([FromBody] StudentToEditRequestDto studentRequest, CancellationToken token)
    {
        var student = await _studentRepository.GetById(studentRequest.Id, token);

        if (student is null)
            return NotFound($"Student with the ID {studentRequest.Id} not found");

        ObjectMapper.Map(studentRequest, student);

        await _studentRepository.Update(student, token);

        return Ok(student);
    }

    [HttpPost("assignGroup")]
    public async Task<IActionResult> AssignGroupAction(
        [FromBody] AssignGroupDto request, 
        [FromServices] IGroupService groupService,
        CancellationToken token)
    {
        var student = await _studentRepository.GetById(request.StudentId, token);

        if (student is null)
            return NotFound($"Student with the Id {request.StudentId} was not found");

        bool groupIsInTheSameDepartment = await groupService.GroupIsInSameDepartment(student.GroupId, request.GroupId, token);

        if (!groupIsInTheSameDepartment)
            return BadRequest("Group must be in the same department");

        student.GroupId = request.GroupId;

        await _studentRepository.Update(student, token);

        return Ok();
    }

    [HttpDelete("delete/{studentId}")]
    public async Task<IActionResult> DeleteAction(int studentId, CancellationToken token)
    {
        bool deleted = await _studentRepository.DeleteById(studentId, token);

        if (!deleted)
            return NotFound($"User with the id {studentId} not found.");

        return Ok();
    }

    [HttpGet("getById")]
    public async Task<IActionResult> GetByIdAction(int studentId, CancellationToken token)
    {
        var student = await _studentRepository.GetById(studentId, token);

        if (student is null)
            return NotFound($"User with the id {studentId} not found.");

        StudentResponseDto response = new();

        ObjectMapper.Map(student, response);

        return Ok(response);
    }

    [HttpGet("getByGroupId")]
    public async Task<IActionResult> GetByGroupIdAction(int groupId, CancellationToken token)
    {
        var students = await _studentRepository.GetByGroupId(groupId, token);

        List<StudentResponseDto> response = students.Select(s =>
        {
            StudentResponseDto responseDto = new();
            ObjectMapper.Map(s, responseDto);

            return responseDto;
        }).ToList();

        return Ok(response);
    }

    [HttpGet("getByDepartmentId")]
    public async Task<IActionResult> GetByDepartmentIdAction(int departmentId, CancellationToken token)
    {
        var students = await _studentRepository.GetByDepartmentId(departmentId, token);

        List<StudentResponseDto> response = students.Select(s =>
        {
            StudentResponseDto responseDto = new();
            ObjectMapper.Map(s, responseDto);

            return responseDto;
        }).ToList();

        return Ok(response);
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllAction(CancellationToken token)
    {
        var allStudents = await _studentRepository.GetAll(token);

        List<StudentResponseDto> response = allStudents.Select(s =>
        {
            StudentResponseDto responseDto = new();
            ObjectMapper.Map(s, responseDto);

            return responseDto;
        }).ToList();

        return Ok(response);
    }
}
