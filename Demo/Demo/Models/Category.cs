namespace Demo.Models
{
    public class Category : CommonDate
    {
        /// <summary>
        /// Mã loại
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Tên loại
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Quan hệ 1-N giữa Category và Product
        /// </summary>
        public virtual ICollection<Product> Products { get; set; }

        /// <summary>
        /// Phương thức khởi tạo Category chứa 1 HashSet Product
        /// </summary>
        public Category()
        {
            Products = new HashSet<Product>();
        }

    }
}
