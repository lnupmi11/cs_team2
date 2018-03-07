using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace xManik.Models.ManageViewModels
{
    public class PortfolioViewModel
    {
        public string StatusMessage { get; set; }

        public ICollection<Artwork> Images{ get; set; }

        [Display(Name = "Додайте короткий опис")]
        public string Descriprion { get; set; }
    }
}
