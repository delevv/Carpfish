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
        public string Name { get; set; }

        [Required]
        [Range(GlobalConstants.LakeMinArea, GlobalConstants.LakeMaxArea)]
        public double Area { get; set; }

        public int CountryId { get; set; }

        public string Coordinates { get; set; }

        public string WebsiteUrl { get; set; }

        [Required]
        public bool IsFree { get; set; } = true;

        public IFormFile MainImage { get; set; }

        public IEnumerable<IFormFile> OtherImages { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CountriesItems { get; set; }
    }
}
