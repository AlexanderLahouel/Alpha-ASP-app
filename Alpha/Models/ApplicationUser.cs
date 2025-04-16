using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Alpha.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = null!;
    }
}
