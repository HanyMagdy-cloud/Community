using Community.Repository.Entities;

namespace Community.Repository.Interfaces
{
    public interface IComment
    {
        // Create a comment
        public Comment CreateComment(Comment comment);

        // Add the DeleteComment method to the interface
        //bool DeleteComment(int commentId, int userId);

        // Get all comments for a specific post
        public List<Comment> GetCommentsByPostId(int postId);

        // Check if the user is commenting on their own post
        public bool IsCommentingOnOwnPost(int userId, int postId);

        // Method to delete a comment
        bool DeleteComment(int commentId, int userId);
    }
}
