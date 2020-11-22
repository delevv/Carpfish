namespace Carpfish.Data.Models
{
    using System.Collections.Generic;

    using Carpfish.Data.Common.Models;

    public class Trophy : BaseDeletableModel<int>
    {
        public Trophy()
        {
            this.TrophyImages = new HashSet<TrophyImage>();
        }

        public virtual ICollection<TrophyImage> TrophyImages { get; set; }

        public double Weight { get; set; }

        public string BaitDescription { get; set; }

        public string OwnerId { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public int LakeId { get; set; }

        public virtual Lake Lake { get; set; }
    }
}
