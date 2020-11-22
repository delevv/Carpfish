namespace Carpfish.Data.Models
{
    public class LakeImage
    {
        public int LakeId { get; set; }

        public virtual Lake Lake { get; set; }

        public string ImageId { get; set; }

        public virtual Image Image { get; set; }

        public bool IsMain { get; set; }
    }
}
