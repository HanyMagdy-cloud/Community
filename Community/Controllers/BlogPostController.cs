using Community.Repository.Entities;
using Community.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Community.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPost _blogPostRepo;

        public BlogPostController(IBlogPost blogPostRepo)
        {
            _blogPostRepo = blogPostRepo;
        }



        // Create a new blog post
        [HttpPost("CreateBlogPost")]
        public IActionResult CreateBlogPost(int userId, BlogPost post)
        {
            if (!IsUserValid(userId))
                return Unauthorized("Only registered users can create posts.");

            if (post == null || post.UserId <= 0)
                return BadRequest("Invalid post data.");

            var createdPost = _blogPostRepo.CreateBlogPost(post);
            return Created("", createdPost);
        }

        // Update a blog post
        [HttpPut("UpdateBlogPost/{userId}/{postId}")]
        public IActionResult UpdateBlogPost(int userId, BlogPost post)
        {
            // Check if the user is registered in the database
            if (!_blogPostRepo.IsUserRegistered(userId))
                return Unauthorized("Only registered users can update posts.");

            // Ensure the post data is valid
            if (post == null || post.PostId <= 0)
                return BadRequest("Invalid post data.");

            try
            {
                // Check ownership and update the blog post
                var updatedPost = _blogPostRepo.UpdateBlogPost(userId, post);
                if (updatedPost == null)
                    return NotFound($"Post with ID {post.PostId} not found.");

                return Ok(updatedPost);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Return unauthorized response if the user does not own the post
                return Unauthorized(ex.Message);
            }
        }

        // Delete a blog post
        [HttpDelete("DeleteBlogPost/{postId}")]
        public IActionResult DeleteBlogPost(int userId, int postId)
        {
            try
            {
                if (!_blogPostRepo.DeleteBlogPost(userId, postId))
                    return NotFound($"Post with ID {postId} not found.");

                return Ok($"Post with ID {postId} deleted successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        // Get all blog posts
        [HttpGet("GetAllBlogPosts")]
        public IActionResult GetAllBlogPosts()
        {
            var posts = _blogPostRepo.GetAllBlogPosts();
            return Ok(posts);
        }

        // Get a blog post by ID
        [HttpGet("GetBlogPostById/{postId}")]
        public IActionResult GetBlogPostById(int postId)
        {
            var post = _blogPostRepo.GetBlogPostById(postId);
            if (post == null)
                return NotFound($"Post with ID {postId} not found.");
            return Ok(post);
        }

        // Search blog posts by keyword
        [HttpGet("SearchBlogPosts/{keyword}")]
        public IActionResult SearchBlogPosts(string keyword)
        {
            var posts = _blogPostRepo.SearchBlogPosts(keyword);
            return Ok(posts);
        }

        // Helper method to validate user
        private bool IsUserValid(int userId)
        {
            return _blogPostRepo.IsUserRegistered(userId);
        }
    }
}
