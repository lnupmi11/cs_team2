using System;
using System.Collections.Generic;
using System.Text;

namespace xManik.DAL.Entities
{
    public class Service
    {
        public string Id { get; set; }
        public string ProviderId { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime Duration { get; set; }
        public DateTime DatePublished { get; set; }
        public bool IsPromoted { get; set; }
        ICollection<Order> Orders { get; set; }
    }
}
