using System;

namespace xManik.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public DateTime DatePosted { get; set; }
        public string Message { get; set; }
        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
        public string RecipentId { get; set; }
        public ApplicationUser Recipent { get; set; }
    }
}
