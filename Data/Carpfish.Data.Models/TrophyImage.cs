namespace Carpfish.Data.Models
{
    public class TrophyImage
    {
        public int TrophyId { get; set; }

        public virtual Trophy Trophy { get; set; }

        public string ImageId { get; set; }

        public virtual Image Image { get; set; }

        public bool IsMain { get; set; }
    }
}
