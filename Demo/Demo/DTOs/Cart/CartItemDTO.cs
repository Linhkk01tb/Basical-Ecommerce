namespace Demo.DTOs.Cart
{
    public class CartItemDTO
    {
        public Guid ProductId {  get; set; }
        public string ProductName { get; set; } = string.Empty;

        public double ProductPrice { get; set; }
        public int BuyQuanlity { get; set; }
        public double Total { get; set; }
    }
}
