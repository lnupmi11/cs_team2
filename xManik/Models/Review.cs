using System;

namespace xManik.Models
{
    public class Review
    {
        public string ReviewId { get; set; }
        public DateTime DatePosted { get; set; }
        public string Message { get; set; }
        public double Rating { get; set; }
        public string ClientId { get; set; }
        public virtual Provider Provider { get; set; }
    }
}
