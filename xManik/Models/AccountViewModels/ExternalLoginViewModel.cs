using System.ComponentModel.DataAnnotations;

namespace xManik.Models.AccountViewModels
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
