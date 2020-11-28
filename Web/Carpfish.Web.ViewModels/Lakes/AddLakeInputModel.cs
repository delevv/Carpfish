namespace Carpfish.Web.ViewModels.Lakes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Carpfish.Common;
    using Microsoft.AspNetCore.Http;

    public class AddLakeInputModel
    {
        [Required]
        [MaxLength(GlobalConstants.LakeNameMaxLength)]
        [Display(Name = "Lake Name")]
        public string Name { get; set; }

        [Required]
        [Range(GlobalConstants.LakeMinArea, GlobalConstants.LakeMaxArea)]
        public double Area { get; set; }

        [Display(Name = "Select Country")]
        public int CountryId { get; set; }

        public string Coordinates { get; set; }

        [Display(Name = "Website Url")]
        public string WebsiteUrl { get; set; }

        [Required]
        public bool IsFree { get; set; } = true;

        [Required]
        [Display(Name = "Select main image")]
        public IFormFile MainImage { get; set; }

        [Display(Name = "Select other images")]
        public IEnumerable<IFormFile> OtherImages { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CountriesItems { get; set; }
    }
}
