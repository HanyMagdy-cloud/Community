using Community.Repository.Entities;
using Community.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Community.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IComment _commentRepo;

        public CommentController(IComment commentRepo)
        {
            _commentRepo = commentRepo;
        }

        // Create a new comment
        [HttpPost("CreateComment")]
        public IActionResult CreateComment(Comment comment)
        {
            // Check if the user is commenting on their own post
            if (_commentRepo.IsCommentingOnOwnPost(comment.UserId, comment.PostId))
            {
                // Return a 400 Bad Request response 
                return BadRequest("You cannot comment on your own post.");
            }

            // Validate comment data
            if (comment == null || comment.PostId <= 0 || comment.UserId <= 0 || string.IsNullOrWhiteSpace(comment.Text))
            {
                return BadRequest("Invalid comment data.");
            }

            // Add the comment using the repository
            var createdComment = _commentRepo.CreateComment(comment);

            return Created("", createdComment);
        }

        // Get all comments for a specific post
        [HttpGet("GetCommentsByPostId/{postId}")]
        public IActionResult GetCommentsByPostId(int postId)
        {
            var comments = _commentRepo.GetCommentsByPostId(postId);
            return Ok(comments);
        }

        ////  DeleteComment 
        [HttpDelete("DeleteComment/{commentId}")]
        public IActionResult DeleteComment(int commentId, int userId)
        {
            bool isDeleted = _commentRepo.DeleteComment(commentId, userId);

            if (isDeleted)
                return Ok("Comment deleted successfully.");
            else
                return NotFound(" You are not allowed to delete this comment !!");
        }
    }
}
