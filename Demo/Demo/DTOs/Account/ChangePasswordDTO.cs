using System.ComponentModel.DataAnnotations;

namespace Demo.DTOs.Account
{
    public class ChangePasswordDTO
    {
        [Required(ErrorMessage ="This field is required!")]
        public string CurrentPassword {  get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string NewPassword { get; set; } = string.Empty;
        [Required(ErrorMessage = "This field is required!")]
        [Compare("NewPassword", ErrorMessage ="New password and Confirm password do not match!")]
        public string ConfirmPassword { get; set;} = string.Empty;
    }
}
