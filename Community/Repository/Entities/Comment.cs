namespace Community.Repository.Entities
{
    public class Comment
    {
        public int CommentId { get; set; } // Change to camelCase
        public int PostId { get; set; }    // Change to camelCase
        public int UserId { get; set; }    // Change to camelCase
        public string Text { get; set; }
    }
}
