using Community.Repository.Entities;

namespace Community.Repository.Interfaces
{
    public interface IBlogPost
    {
        // Create a new blog post
        public BlogPost CreateBlogPost(BlogPost post);

        // Get all blog posts
        public List<BlogPost> GetAllBlogPosts();

        // Get a blog post by ID
        public BlogPost GetBlogPostById(int postId);

        // Search blog posts by keyword
        public List<BlogPost> SearchBlogPosts(string keyword);

        // Delete a blog post by ID
        public bool DeleteBlogPost(int userId, int postId);

        // Update a blog post
        public BlogPost UpdateBlogPost(int userId, BlogPost post);

        public bool IsUserRegistered(int userId);
    }
}
