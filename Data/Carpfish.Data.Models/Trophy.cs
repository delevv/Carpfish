namespace Carpfish.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Carpfish.Common;
    using Carpfish.Data.Common.Models;

    public class Trophy : BaseDeletableModel<int>
    {
        public Trophy()
        {
            this.TrophyImages = new HashSet<TrophyImage>();
            this.TrophyVotes = new HashSet<TrophyVote>();
        }

        public virtual ICollection<TrophyImage> TrophyImages { get; set; }

        [Range(GlobalConstants.TrophyMinWieght, GlobalConstants.TrophyMaxWeight)]
        public double Weight { get; set; }

        [Required]
        [MaxLength(GlobalConstants.TrophyBaitDescriptionMaxLength)]
        public string BaitDescription { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public int LakeId { get; set; }

        public virtual Lake Lake { get; set; }

        public virtual ICollection<TrophyVote> TrophyVotes { get; set; }
    }
}
