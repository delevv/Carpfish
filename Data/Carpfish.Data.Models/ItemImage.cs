namespace Carpfish.Data.Models
{
    public class ItemImage
    {
        public int ItemId { get; set; }

        public virtual Item Item { get; set; }

        public string ImageId { get; set; }

        public virtual Image Image { get; set; }

        public bool IsMain { get; set; }
    }
}
