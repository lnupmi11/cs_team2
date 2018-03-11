using System.ComponentModel.DataAnnotations;

namespace xManik.Models.ManageViewModels
{

    public class IndexViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }

        public byte[] ProfileImage { get; set; }
    }
}
