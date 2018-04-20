using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xManik.Models.BloggerViewModels
{
    public class BloggerViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ImageName { get; set; }
        public DateTime DateRegistered { get; set; }

        public virtual ICollection<Chanel> Chanels { get; set; }
    }
}
