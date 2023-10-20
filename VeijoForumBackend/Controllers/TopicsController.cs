using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeijoForumBackend.Models;
using VeijoForumBackend.Models.Dto;
using VeijoForumBackend.Models.Dto.TopicDtos;
using VeijoForumBackend.Services.Interfaces;

namespace VeijoForumBackend.Controllers
{
    [Route("v1/topics")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicService _topicsService;

        public TopicsController(ITopicService topicsService)
        {
            _topicsService = topicsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TopicDto>>> GetTopics([FromQuery] TopicQuery topicQuery)
        {
            var topics = await _topicsService.GetTopicsAsync(topicQuery);

            if (topics == null || !topics.Any())
            {
                return NotFound();
            }

            return Ok(topics);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TopicDto>> GetTopicById(int id)
        {
            var topic = await _topicsService.GetTopicByIdAsync(id);

            if (topic == null)
            {
                return NotFound();
            }

            return Ok(topic);
        }

        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutTopic(int id, Topic topic)
        {
            if (id != topic.Id)
            {
                return BadRequest();
            }

            _context.Entry(topic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TopicExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Topic>> CreateTopic(CreateTopicDto createTopicDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _topicsService.CreateTopicAsync(createTopicDto);

            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteTopic(int id)
        {
            var result = _topicsService.DeleteTopicById(id);

            return Ok(result);
        }
    }
}
