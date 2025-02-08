using IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using NewsAPI.Models;
using Service;


namespace Controller 
{
    [ApiController]
    [Route("api/")]
    public class CommentController : ControllerBase
    {
        private readonly IRepository<Comment> _dbComment;
        private readonly TokenService _tokenService;

        public CommentController(IRepository<Comment> dbComment, TokenService tokenService)
        {
            _dbComment = dbComment;
            _tokenService = tokenService;
        }
        
        [HttpPost("createComment")]
        [Authorize]
        public async Task<IActionResult> GetNews([FromQuery] int id, string value)
        {
            var userid = _tokenService.GetUserIdFromToken();
            var useridInt = int.Parse(userid);
            var comment = new Comment()
            {
                ArticleId = id,
                UserId = useridInt,
                Text = value,
                CreatedAt = DateTime.Now
            };

             _dbComment.Create(comment);
            await _dbComment.Save();
            return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment);

        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetCommentById(int id)
        {
            var comment = await _dbComment.Get(u => u.Id == id); 
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }
    }
}

