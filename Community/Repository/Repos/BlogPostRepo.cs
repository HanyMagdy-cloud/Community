using Community.Repository.Entities;
using Community.Repository.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Community.Repository.Repos
{
    public class BlogPostRepo : IBlogPost
    {
        private readonly string _connString;

        public BlogPostRepo(IConfiguration config)
        {
            _connString = config.GetConnectionString("Community");
        }

        // Create a new blog post
        public BlogPost CreateBlogPost(BlogPost post)
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Title", post.Title);
                parameters.Add("@Content", post.Content);
                parameters.Add("@UserId", post.UserId);
                parameters.Add("@CategoryId", post.CategoryId);

                db.Execute("CreateBlogPost", parameters, commandType: CommandType.StoredProcedure);
                return post;
            }
        }
        // Delete a blog post
        public bool DeleteBlogPost(int userId, int postId)
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                // Validate ownership
                if (!IsUserOwnerOfPost(userId, postId))
                    throw new UnauthorizedAccessException("You do not have permission to delete this blog post.");

                var parameters = new DynamicParameters();
                parameters.Add("@PostId", postId);

                // Execute stored procedure to delete the blog post
                int rowsAffected = db.Execute("DeleteBlogPost", parameters, commandType: CommandType.StoredProcedure);

                // Return true if the blog post was successfully deleted
                return rowsAffected > 0;
            }
        }

        // Get all blog posts
        public List<BlogPost> GetAllBlogPosts()
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                return db.Query<BlogPost>("GetAllBlogPosts", commandType: CommandType.StoredProcedure).AsList();
            }
        }
        // Get a blog post by ID
        public BlogPost GetBlogPostById(int postId)
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PostId", postId);

                return db.QuerySingleOrDefault<BlogPost>("GetBlogPostById", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        // Search blog posts by keyword
        public List<BlogPost> SearchBlogPosts(string keyword)
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Keyword", keyword);

                return db.Query<BlogPost>("SearchBlogPosts", parameters, commandType: CommandType.StoredProcedure).AsList();
            }
        }


        // Update a blog post
        public BlogPost UpdateBlogPost(int userId, BlogPost post)
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                // Ensure the user is the owner of the blog post
                if (!IsUserOwnerOfPost(userId, post.PostId))
                    throw new UnauthorizedAccessException("You are not the owner of this blog post.");

                // Execute the update
                var parameters = new DynamicParameters();
                parameters.Add("@PostId", post.PostId);
                parameters.Add("@Title", post.Title);
                parameters.Add("@Content", post.Content);
                parameters.Add("@CategoryId", post.CategoryId);

                db.Execute("UpdateBlogPost", parameters, commandType: CommandType.StoredProcedure);

                return post; // Return the updated post
            }
        }

        // method to check if the user is registered in the user table or not
        public bool IsUserRegistered(int userId)
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", userId);

                // Execute the stored procedure and return the result
                return db.ExecuteScalar<bool>("CheckUserExistsById", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        //method to check if the provided userId is the owner of a given blogPostId.

        // Check if a user owns a specific blog post
        public bool IsUserOwnerOfPost(int userId, int postId)
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", userId);
                parameters.Add("@PostId", postId);

                // Execute stored procedure to verify ownership
                return db.ExecuteScalar<bool>("CheckBlogPostOwnership", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        
    }
}
