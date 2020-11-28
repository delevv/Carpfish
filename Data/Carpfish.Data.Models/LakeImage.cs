namespace Carpfish.Data.Models
{
    using Carpfish.Data.Common.Models;

    public class LakeImage : BaseDeletableModel<int>
    {
        public virtual Lake Lake { get; set; }

        public string ImageId { get; set; }

        public virtual Image Image { get; set; }

        public bool IsMain { get; set; }
    }
}
