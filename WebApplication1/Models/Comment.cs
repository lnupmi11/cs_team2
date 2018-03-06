using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace xManik.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public DateTime DatePosted { get; set; }
        public string Message { get; set; }
        public virtual Client Author { get; set; }
        public virtual Provider Recipient { get; set; }
    }
}
