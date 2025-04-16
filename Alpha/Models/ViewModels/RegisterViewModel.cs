using System.ComponentModel.DataAnnotations;


namespace Alpha.Models.ViewModels;

    public class RegisterViewModel
    {
    [Required(ErrorMessage = "You must enter your full name")]
    public string FullName { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "You must enter your email adress.")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "You must enter a valid email address.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "You must enter a password")]
    [RegularExpression(@"^(?=.*[A-Ö])(?=.*[a-ö])(?=.*\d)(?=.*[\W_])[A-Za-z\d\W_]{8,}$", ErrorMessage = "Password must be at least 8 characters long, include at least one uppercase letter, one lowercase letter, one number, and one special character.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Display(Name = "Confirm Password")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    [Required(ErrorMessage = "You must confirm your password.")]
    public string ConfirmPassword { get; set; } = null!;

    [Required(ErrorMessage = "You must accept the terms.")]
    [Display(Name = "Accept Terms")]
    public bool AcceptTerms { get; set; }
}


