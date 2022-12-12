using Application.Abstractions.Services.Group;
using Application.Abstractions.Services.Repositories;
using Application.Helpers;
using Domain.Domains;
using Microsoft.AspNetCore.Mvc;
using Presentation.Data.DTO.Group;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class GroupsController : ControllerBase
{
    private readonly IGroupRepository _groupRepository;

    public GroupsController(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAction([FromBody] GroupRequestDto groupRequest)
    {
        Group group = new();

        ObjectMapper.Map(groupRequest, group);

        int id = await _groupRepository.Create(group);

        if (id == -1)
            return BadRequest("Assigned department doesn't exist.");

        return CreatedAtAction(nameof(CreateAction), id);
    }

    [HttpPut("edit")]
    public async Task<IActionResult> EditAction([FromBody] GroupEditRequestDto groupRequest)
    {
        var groupWithAverages = await _groupRepository.GetById(groupRequest.Id);

        if (groupWithAverages is null)
            return NotFound("Group not found.");

        Group group = new();
        ObjectMapper.Map(groupRequest, group);

        await _groupRepository.Update(group);

        return Ok(group.Id);
    }

    [HttpDelete("delete/{groupId}")]
    public async Task<IActionResult> DeleteAction([FromServices] IGroupService groupService , int groupId)
    {
        bool groupHasStudents = await groupService.GroupHasStudents(groupId);

        if (groupHasStudents)
            return BadRequest("This group contains students.");

        var deleted = await _groupRepository.Delete(groupId);

        if (!deleted)
            return NotFound("Group not found.");

        return Ok($"Successfully deleted group with group ID {groupId}");
    }

    [HttpGet("getById")]
    public async Task<IActionResult> GetByIdAction(int groupId)
    {
        var group = await _groupRepository.GetById(groupId);

        if (group is null)
            return NotFound();

        GroupResponseDto groupDto = new();

        ObjectMapper.Map(group, groupDto);

        return Ok(groupDto);
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllAction()
    {
        var groups = await _groupRepository.GetAll();

        var groupDtos = groups
            .Select(g =>
            {
                GroupResponseDto groupDto = new();
                ObjectMapper.Map(g, groupDto);

                return groupDto;
            })
            .ToList();

        return Ok(groupDtos);
    }
}
