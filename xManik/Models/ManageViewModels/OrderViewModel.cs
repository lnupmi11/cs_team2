using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xManik.Models.ManageViewModels
{
    public class OrderViewModel
    {
        public string UserName { get; set; }
        public virtual Service Service{get;set;}
        public virtual Order Order { get; set; }
    }
}
