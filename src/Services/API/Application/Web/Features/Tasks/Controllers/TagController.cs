using System.ComponentModel.DataAnnotations;
using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.DataAccess.Entities.Tasks;
using EnterpriseAssistant.Web.Features.Tasks.DataTransfer;
using EnterpriseAssistant.Web.Helpers.DataTransfer;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EnterpriseAssistant.Web.Features.Tasks.Controllers;

[Route("api/tag")]
public class TagController : ControllerBase
{
    private readonly EnterpriseAssistantDbContext _context;

    public TagController(EnterpriseAssistantDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all tags")]
    public async Task<ActionResult<IEnumerable<TagDto>>> GetAllTags([FromQuery] int pageNumber,
        [FromQuery, PageSize] int pageSize = PagedResult.DefaultPageSize)
    {
        var tags = await _context.Tags.ToPagedResultAsync(pageNumber, pageSize);
        return Ok(tags.ToPagedResultOf<TagDto>());
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create new tag")]
    public async Task<ActionResult<TagDto>> CreateTag([FromBody] TagCreateDto tag)
    {
        var createdTag = _context.Tags.Add(tag.Adapt<Tag>());
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(CreateTag), createdTag.Adapt<TagDto>());
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update a tag")]
    public async Task<ActionResult<TagDto>> UpdateTag([FromBody] TagUpdateDto tag)
    {
        var updatedTag = _context.Tags.Update(tag.Adapt<Tag>());
        await _context.SaveChangesAsync();
        return Ok(updatedTag.Adapt<TagDto>());
    }

    [HttpDelete("{id:long}")]
    [SwaggerOperation(Summary = "Delete a tag")]
    public async Task<ActionResult<TagDto>> DeleteTag([FromRoute, Range(1, long.MaxValue)] long id)
    {
        var deletedTagCount = await _context.Tags.Where(x => x.TagId == id).DeleteFromQueryAsync();
        if (deletedTagCount <= 0)
        {
            return NotFound("Tag not found");
        }

        return NoContent();
    }
}