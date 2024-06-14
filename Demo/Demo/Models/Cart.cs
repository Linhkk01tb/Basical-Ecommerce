namespace Demo.Models
{
    public class Cart
    {
        public Guid CartId { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();
    }
}
