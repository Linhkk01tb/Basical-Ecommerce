namespace Demo.Models
{
    public class OrderDetail
    {
        /// <summary>
        /// Mã đơn hàng
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// Mã sản phẩm
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Số lượng mua
        /// </summary>
        public int BuyQuantity { get; set; }

        /// <summary>
        /// Đơn giá sản phẩm
        /// </summary>
        public double Total {  get; set; }

        #region Relationship with Product and Order
        public Product Product { get; set; }

        public Order Order { get; set; }
        #endregion
    }
}
