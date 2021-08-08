using System.ComponentModel.DataAnnotations;

namespace CustomerManagementSystem.Services.CompanyB.Models
{
    public class GSMValidationModel
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string GSM { get; set; }
    }
}
