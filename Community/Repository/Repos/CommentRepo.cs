using Community.Repository.Entities;
using Community.Repository.Interfaces;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Community.Repository.Repos
{

    //The CommentRepo class implements the IComment interface .
    public class CommentRepo : IComment
    {
        private readonly string _connString;

        // Constructor for dependency injection
        public CommentRepo(IConfiguration config)
        {
            _connString = config.GetConnectionString("Community");
        }

        // Create a comment
        public Comment CreateComment(Comment comment)
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                // Ensure the user is not commenting on their own post
                if (IsCommentingOnOwnPost(comment.UserId, comment.PostId))
                    throw new UnauthorizedAccessException("You cannot comment on your own post.");

                var parameters = new DynamicParameters();
                parameters.Add("@PostID", comment.PostId);
                parameters.Add("@UserID", comment.UserId);
                parameters.Add("@Text", comment.Text);

                comment.CommentId = db.QuerySingle<int>(
                    "CreateComment",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return comment;
            }
        }
        // Delete a comment based on the commentId and userid
        public bool DeleteComment(int commentId, int userId)
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@CommentID", commentId);
                parameters.Add("@UserID", userId);

                // Execute the stored procedure 
                return db.Execute("DeleteComment", parameters, commandType: CommandType.StoredProcedure) > 0;
            }
        }

        // Get all comments for a specific post
        public List<Comment> GetCommentsByPostId(int postId)
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PostID", postId);

                return db.Query<Comment>(
                    "GetCommentsByPostId",
                    parameters,
                    commandType: CommandType.StoredProcedure).AsList();
            }
        }

        // Check if the user is commenting on their own post
        public bool IsCommentingOnOwnPost(int userId, int postId)
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserID", userId);
                parameters.Add("@PostID", postId);

                return db.ExecuteScalar<bool>(
                    "CheckCommentingOnOwnPost",
                    parameters,
                    commandType: CommandType.StoredProcedure);
                
            }
        }
    }
}
