namespace Carpfish.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Carpfish.Common;
    using Carpfish.Data.Common.Models;

    public class Lake : BaseDeletableModel<int>
    {
        public Lake()
        {
            this.Trophies = new HashSet<Trophy>();
            this.LakeImages = new HashSet<LakeImage>();
        }

        [Required]
        [MaxLength(GlobalConstants.LakeNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [Range(GlobalConstants.LakeMinArea, GlobalConstants.LakeMaxArea)]
        public double Area { get; set; }

        public virtual ICollection<LakeImage> LakeImages { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public string Coordinates { get; set; }

        public string WebsiteUrl { get; set; }

        public bool IsFree { get; set; }

        public double Rating { get; set; }

        public int RatersCount { get; set; }

        public virtual ICollection<Trophy> Trophies { get; set; }
    }
}
