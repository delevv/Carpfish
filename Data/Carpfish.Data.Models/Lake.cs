namespace Carpfish.Data.Models
{
    using System.Collections.Generic;

    using Carpfish.Data.Common.Models;

    public class Lake : BaseDeletableModel<int>
    {
        public Lake()
        {
            this.Trophies = new HashSet<Trophy>();
            this.LakeImages = new HashSet<LakeImage>();
        }

        public string Name { get; set; }

        public virtual ICollection<LakeImage> LakeImages { get; set; }

        public string Country { get; set; }

        public string Region { get; set; }

        public string WebsiteUrl { get; set; }

        public bool IsFree { get; set; }

        public virtual ICollection<Trophy> Trophies { get; set; }
    }
}
