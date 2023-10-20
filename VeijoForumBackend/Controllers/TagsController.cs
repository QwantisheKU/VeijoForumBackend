using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeijoForumBackend.Models.Dto.TagDtos;
using VeijoForumBackend.Services.Interfaces;

namespace VeijoForumBackend.Controllers
{
    [Route("api/tags")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TagDto>>> GetTags()
        {
            var tags = await _tagService.GetTags();

            if (tags == null)
            {
                return NotFound();
            }

            return Ok(tags);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TagDto>> GetTagById(int id)
        {
            var tag = await _tagService.GetTagById(id);

            if (tag == null)
            {
                return NotFound();
            }

            return Ok(tag);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<bool>> CreateTag(CreateTagDto createTagDto)
        {
            if (createTagDto == null)
            {
                return BadRequest();
            }

            var result = _tagService.CreateTag(createTagDto);

            return Ok(result);
        }
    }
}
