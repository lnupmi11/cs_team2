using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace xManik.Models.ManageViewModels
{
    public class PortfolioViewModel
    {
        public string StatusMessage { get; set; }

        public ICollection<Artwork> Images{ get; set; }

        [Display(Name = "Додайте короткий опис")]
        public string Description { get; set; }
    }
}
