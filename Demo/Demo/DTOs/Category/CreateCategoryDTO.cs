using Demo.Models;
using System.ComponentModel.DataAnnotations;

namespace Demo.DTOs.Category
{
    public class CreateCategoryDTO
    {
        /// <summary>
        /// Tên loại
        /// </summary>
        [Required(ErrorMessage = "This field is required!")]
        [MaxLength(100, ErrorMessage = "Not over 100 characters!")]
        public string CategoryName { get; set; } = string.Empty;

    }
}
