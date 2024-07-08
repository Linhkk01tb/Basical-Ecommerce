using Demo.Models;

namespace Demo.DTOs.Product
{
    public class ProductDTO : CommonDate
    {
        /// <summary>
        /// Mã sản phẩm
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Tên sản phẩm
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Số lượng sản phẩm
        /// </summary>
        public int ProductQuantity { get; set; }

        /// <summary>
        /// Giá sản phẩm
        /// </summary>
        public double ProductPrice { get; set; }

        /// <summary>
        /// Mô tả sản phẩm
        /// </summary>
        public string? ProductDescription { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public ICollection<ImageDTO> Images { get; set; } = new List<ImageDTO>();
    }
}
