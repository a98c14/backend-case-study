using System.ComponentModel.DataAnnotations;

namespace CustomerManagementSystem.Services.CompanyC.Models
{
    public class EmailValidationModel
    {
        [Required]
        public string Token { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
