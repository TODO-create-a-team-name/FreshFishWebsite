using System.ComponentModel.DataAnnotations;

namespace FreshFishWebsite.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Ім'я")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Прізвище")]
        public string Surname { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name="Підприємство")]
        public string Company { get; set; }

        [Required]
        [Display(Name = "Адреса підприємства")]
        public string CompanyAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        [DataType(DataType.Password)]
        [Display(Name = "Підтвердити пароль")]
        public string PasswordConfirm { get; set; }
    }
}
