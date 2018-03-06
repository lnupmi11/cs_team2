using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace xManik.Models
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Duration { get; set; }
        public virtual Provider Provider { get; set; }
    }
}
