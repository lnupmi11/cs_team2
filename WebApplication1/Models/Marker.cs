using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace xManik.Models
{
    public class Marker
    { 
        [Key]
        public int MarkerId { get; set; }
        public string Adress { get; set; }
        public string Latitude { get; set; }
        public string Longtitude { get; set; }
    }
}
