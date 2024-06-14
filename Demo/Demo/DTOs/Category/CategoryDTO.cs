using Demo.DTOs.Product;
using Demo.Models;

namespace Demo.DTOs.Category
{
    public class CategoryDTO : CommonDate
    {
        /// <summary>
        /// Mã loại
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Tên loại
        /// </summary>
        public string CategoryName { get; set; } = string.Empty;

        public ICollection<ProductDTO> Products { get; set; }
    }
}
