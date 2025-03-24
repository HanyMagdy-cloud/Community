namespace Community.Repository.Entities
{
    public class BlogPost
    {
        public int PostId { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public int CategoryId { get; set; }
    }
}
