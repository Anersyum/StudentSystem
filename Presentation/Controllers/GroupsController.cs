using Application.Helpers;
using Domain.Abstractions.Interfaces;
using Domain.Domains;
using Microsoft.AspNetCore.Mvc;
using Presentation.Data.DTO.Group;

namespace StudentTestPontis.Controllers;

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

        await _groupRepository.Create(group);

        return CreatedAtAction(nameof(CreateAction), group.Id);
    }

    [HttpPut("edit")]
    public async Task<IActionResult> EditAction([FromBody] GroupEditRequestDto groupRequest)
    {
        var group = await _groupRepository.GetById(groupRequest.Id);

        if (group is null)
            return NotFound("Group not found.");

        ObjectMapper.Map(groupRequest, group);

        await _groupRepository.Update(group);

        return Ok(group.Id);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteAction([FromBody] int groupId)
    {
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
                ObjectMapper.Map<Group, GroupResponseDto>(g, groupDto);

                return groupDto;
            })
            .ToList();

        return Ok(groupDtos);
    }
}
