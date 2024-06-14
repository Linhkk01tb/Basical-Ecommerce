using Demo.Enums;
using Demo.Models;
using System.ComponentModel.DataAnnotations;

namespace Demo.DTOs.Order
{
    public class CreateOrderDTO
    {
        /// <summary>
        /// Tên người nhận
        /// </summary>
        [Required(ErrorMessage = "This field is required!")]
        [MaxLength(100, ErrorMessage = "Not over 100 characters!")]
        public string ReceivedName { get; set; } = string.Empty;

        /// <summary>
        /// Số điện thoại người nhận
        /// </summary>
        [Required(ErrorMessage = "This field is required!")]
        [MaxLength(20, ErrorMessage = "Not over 20 characters!")]
        [Phone(ErrorMessage ="Not a phone number!")]
        public string ReceivedPhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Email người nhận
        /// </summary>

        [Required(ErrorMessage = "This field is required!")]
        [MaxLength(100, ErrorMessage = "Not over 100 characters!")]
        [EmailAddress(ErrorMessage ="Not an email!")]
        public string ReceivedEmail { get; set; } = string.Empty;

    }
}
