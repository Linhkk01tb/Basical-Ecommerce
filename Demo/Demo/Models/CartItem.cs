namespace Demo.Models
{
    public class CartItem
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int BuyQuanlity {  get; set; }
        public double Total {  get; set; }
        public Product Product { get; set; }
        public Cart Cart { get; set; }
    }
}
