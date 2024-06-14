using Demo.Models;

namespace Demo.DTOs.OrderDetailDTO
{
    public class OrderDetailDTO
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
        /// Số lượng mua
        /// </summary>
        public int BuyQuantity { get; set; }

        /// <summary>
        /// Đơn giá sản phẩm
        /// </summary>
        public double ProductPrice { get; set; }

        public double Total { get; set; }

    }
}
