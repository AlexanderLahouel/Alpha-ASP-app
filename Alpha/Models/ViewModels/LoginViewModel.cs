namespace Alpha.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

    public class LoginViewModel
    {
        [Required, EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }

        public bool RememberMe { get; set; }
    }


