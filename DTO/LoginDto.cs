using System.ComponentModel.DataAnnotations;

namespace CIt_Demo_Project.DTO
{
    public class LoginDto
    {
        [Required (ErrorMessage = "Email Required"), EmailAddress (ErrorMessage = "Invalid Email"),  MaxLength(100)]
        public string email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password Required"), MaxLength(100)]
        public string password { get; set; } = string.Empty;
    }
}
