namespace Demo.Models
{
    public class Image
    {
        public Guid ImageId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string ImageName { get; set; } = string.Empty;
        public bool IsAvatar {  get; set; } = false;
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
