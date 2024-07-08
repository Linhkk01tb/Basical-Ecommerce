using Demo.Enums;
namespace Demo.Models
{
    public class Order : CommonDate
    {
        /// <summary>
        /// Id đơn hàng
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// Mã đơn hàng
        /// </summary>
        public string OrderCode { get; set; }

        /// <summary>
        /// Tên người nhận
        /// </summary>
        public string ReceivedName { get; set; }
        
        /// <summary>
        /// Địa chỉ nhận hàng
        /// </summary>
        public string ReceivedAddress { get; set; }

        // <summary>
        /// Ngày nhận hàng
        /// </summary>
        public DateTime ReceivedDate { get; set; }

        /// <summary>
        /// Số điện thoại người nhận
        /// </summary>
        public string ReceivedPhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Email người nhận
        /// </summary>
        public string ReceivedEmail { get; set; } = string.Empty;

        /// <summary>
        /// Trạng thái đơn hàng
        /// </summary>
        public OrderStatusCodes OrderStatus { get; set; }

        public string UserId { get; set; }

        public AppUser User { get; set; }

        /// <summary>
        /// Quan hệ 1-N giữa Order và OrderDetail
        /// </summary>
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }


        /// <summary>
        /// Phương thức khởi tạo class Order 1 HashSet OrderDetail
        /// </summary>
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

    }
}
