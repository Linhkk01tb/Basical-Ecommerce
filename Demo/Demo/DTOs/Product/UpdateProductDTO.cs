using Demo.Models;
using System.ComponentModel.DataAnnotations;

namespace Demo.DTOs.Product
{
    public class UpdateProductDTO
    {
        /// <summary>
        /// Tên sản phẩm
        /// </summary>
        [Required(ErrorMessage = "This field is required!")]
        [MaxLength(100, ErrorMessage ="Not over 100 characters!")]
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Số lượng sản phẩm
        /// </summary>
        [Required(ErrorMessage = "This field is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "Minimum is 1 unit!")]
        public int ProductQuantity { get; set; } = 1;

        /// <summary>
        /// Giá sản phẩm
        /// </summary>
        [Required(ErrorMessage = "This field is required!")]
        [Range(1000, double.MaxValue,ErrorMessage ="Minimum is 1000 dong!")]
        public double ProductPrice { get; set; }

        /// <summary>
        /// Mô tả sản phẩm
        /// </summary>
        [MaxLength(int.MaxValue)]
        public string? ProductDescription { get; set; }
    }
}
