using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using xManik.Models;

namespace WebApplication1.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public UserRole Role { get; set; }
        public string UserName { get; set; }
    }
}
