using System.ComponentModel.DataAnnotations;

namespace Demo.DTOs.Account
{
    public class SignUpDTO
    {
        [Required(ErrorMessage = "This field is required!")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "This field is required!")]
        [EmailAddress(ErrorMessage = "Not an email!")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "This field is required!")]
        public string Password { get; set; } = string.Empty;
        [Compare("Password", ErrorMessage ="Password and cofirm password do not match!")]
        public string ConfirmPassword {  get; set; } = string.Empty;

        public string ClientUri { get; set; } = string.Empty;
    }
}
