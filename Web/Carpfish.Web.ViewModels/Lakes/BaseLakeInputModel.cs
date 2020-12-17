namespace Carpfish.Web.ViewModels.Lakes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Carpfish.Common;

    public abstract class BaseLakeInputModel
    {
        [Required]
        [MaxLength(GlobalConstants.LakeNameMaxLength)]
        [Display(Name = "Lake Name")]
        public string Name { get; set; }

        [Display(Name = "Select Country")]
        public int CountryId { get; set; }

        [Required]
        [Range(GlobalConstants.LakeMinArea, GlobalConstants.LakeMaxArea)]
        [Display(Name = "Area(km2)")]
        public double Area { get; set; }

        [Display(Name = "Website Url")]
        public string WebsiteUrl { get; set; }

        [Required]
        public bool IsFree { get; set; } = true;

        public IEnumerable<KeyValuePair<string, string>> CountriesItems { get; set; }
    }
}
