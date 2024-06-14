namespace Demo.DTOs
{
    public class ImageDTO
    {
        public Guid ImageId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string ImageName { get; set; } = string.Empty;
        public bool IsAvatar { get; set; } = false;
    }
}
