using System;

namespace xManik.Models
{
    public class Service
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Duration { get; set; }
        public bool IsPromoted { get; set; }
        public DateTime DatePublished { get; set; }
        public virtual Provider Provider { get; set; }
    }
}
