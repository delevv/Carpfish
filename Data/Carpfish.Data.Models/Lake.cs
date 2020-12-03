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
            this.LakeVotes = new HashSet<LakeVote>();
        }

        [Required]
        [MaxLength(GlobalConstants.LakeNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        [Required]
        [Range(GlobalConstants.LakeMinArea, GlobalConstants.LakeMaxArea)]
        public double Area { get; set; }

        public virtual ICollection<LakeImage> LakeImages { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public string Coordinates { get; set; }

        public string WebsiteUrl { get; set; }

        public bool IsFree { get; set; }

        public virtual ICollection<Trophy> Trophies { get; set; }

        public virtual ICollection<LakeVote> LakeVotes { get; set; }
    }
}
