using System;
using System.ComponentModel.DataAnnotations;

namespace xManik.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public DateTime DatePosted { get; set; }
        public string Message { get; set; }
        public virtual Client Author { get; set; }
        public virtual Provider Recipient { get; set; }
    }
}
