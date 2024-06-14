using System.ComponentModel.DataAnnotations;

namespace Demo.DTOs.Account
{
    public class SignInDTO
    {
        [Required(ErrorMessage = "This field is required!")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "This field is required!")]
        public string Password { get; set; } = string.Empty;
    }
}