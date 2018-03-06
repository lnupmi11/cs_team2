using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace xManik.Models
{
    public class Artwork
    {
        [Key]
        public int ID { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
    }
}
