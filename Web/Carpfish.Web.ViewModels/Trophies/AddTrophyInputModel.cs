namespace Carpfish.Web.ViewModels.Trophies
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Carpfish.Common;
    using Microsoft.AspNetCore.Http;

    public class AddTrophyInputModel
    {
        [Range(GlobalConstants.TrophyMinWieght, GlobalConstants.TrophyMaxWeight)]
        public double Weight { get; set; }

        [Required]
        [MaxLength(GlobalConstants.TrophyBaitDescriptionMaxLength)]
        [Display(Name = "Describe Bait")]
        public string BaitDescription { get; set; }

        [Display(Name = "Select Lake")]
        public int LakeId { get; set; }

        [Required]
        [Display(Name = "Select main image")]
        public IFormFile MainImage { get; set; }

        [Display(Name = "Select other images")]
        public IEnumerable<IFormFile> OtherImages { get; set; }

        public IEnumerable<KeyValuePair<string, string>> LakesItems { get; set; }
    }
}
