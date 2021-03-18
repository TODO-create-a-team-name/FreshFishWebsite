
using System.ComponentModel.DataAnnotations;

namespace FreshFishWebsite.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
