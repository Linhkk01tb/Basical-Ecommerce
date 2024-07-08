using System.ComponentModel.DataAnnotations;

namespace Demo.DTOs.Account
{
    public class ForgotPasswordDTO
    {
        [Required(ErrorMessage ="This field is required!")]
        [EmailAddress(ErrorMessage ="Not an email!")]
        public string Email {  get; set; }
        public string ClientUri {  get; set; } = string.Empty;
    }
}
