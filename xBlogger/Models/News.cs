using System;

namespace xBlogger.Models
{
    public class News
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public DateTime DatePublished { get; set; }
    }
}
