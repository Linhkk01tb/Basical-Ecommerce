using Demo.Enums;
using Demo.Models;

namespace Demo.DTOs.Order
{
    public class OrderDTO : CommonDate
    {
        /// <summary>
        /// Id đơn hàng
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// Mã đơn hàng
        /// </summary>
        public string OrderCode { get; set; } = string.Empty;

        /// <summary>
        /// Tên người nhận
        /// </summary>
        public string ReceivedName { get; set; } = string.Empty;

        /// <summary>
        /// Địa chỉ người nhận
        /// </summary>
        public string ReceivedAddress { get; set; } = string.Empty;

        // <summary>
        /// Ngày nhận hàng
        /// </summary>
        public DateTime ReceivedDate { get; set; }

        /// <summary>
        /// Số điện thoại người nhận
        /// </summary>
        public string ReceivedPhoneNumber { get; set; }= string.Empty;

        /// <summary>
        /// Email người nhận
        /// </summary>
        public string ReceivedEmail { get; set; }=string.Empty;

        /// <summary>
        /// Trạng thái đơn hàng
        /// </summary>
        public OrderStatusCodes OrderStatus { get; set; }

        public List<OrderDetailDTO> OrderDetails { get; set; } = new List<OrderDetailDTO>();
    }
}
