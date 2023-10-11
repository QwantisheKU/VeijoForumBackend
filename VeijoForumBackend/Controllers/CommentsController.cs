using Microsoft.AspNetCore.Mvc;
using VeijoForumBackend.Models.Dto.CommentDtos;
using VeijoForumBackend.Services.Interfaces;

namespace VeijoForumBackend.Controllers
{
    [Route("v1/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("{parentId}/replies")]
        public async Task<ActionResult<List<CommentDto>>> GetCommentsByParent(int parentId)
        {
            var comments = await _commentService.GetCommentsByParent(parentId);

            if (comments == null)
            {
                return NotFound();
            }

            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDto>> GetCommentById(int id)
        {
            var comment = await _commentService.GetCommentById(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        [HttpPost]
        public async Task<ActionResult<bool>> CreateComment(CreateCommentDto createCommentDto)
        {
            var result = _commentService.CreateComment(createCommentDto);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteComment(int id)
        {
            var result = _commentService.DeleteCommentById(id);

            return Ok(result);
        }
    }
}
