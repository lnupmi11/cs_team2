using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xBlogger.Models
{
    public class DealViewModel
    {
        public string DealId { get; set; }
        public string AssigmentId { get; set; }
        public string ClientId { get; set; }
        public string BloggerId { get; set; }
        public bool IsReadByBlogger { get; set; }
        public bool IsReadByClient { get; set; }
        public bool IsConfirmed { get; set; }

        public Assigment Assigment { get; set; }
    }
}
