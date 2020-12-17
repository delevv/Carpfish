namespace Carpfish.Web.ViewModels.Trophies
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Carpfish.Common;

    public abstract class BaseTrophyInputModel
    {
        [Range(GlobalConstants.TrophyMinWieght, GlobalConstants.TrophyMaxWeight)]
        public double Weight { get; set; }

        [Required]
        [MaxLength(GlobalConstants.TrophyBaitDescriptionMaxLength)]
        [Display(Name = "Describe Bait")]
        public string BaitDescription { get; set; }

        [Display(Name = "Select Lake")]
        public int LakeId { get; set; }

        [Display(Name = "Select Rig")]
        public int? RigId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> LakesItems { get; set; }

        public IEnumerable<KeyValuePair<string, string>> RigsItems { get; set; }
    }
}
