namespace Carpfish.Web.ViewModels.Lakes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Carpfish.Web.ViewModels.Location;
    using Microsoft.AspNetCore.Http;

    public class AddLakeInputModel : BaseLakeInputModel
    {
        public LocationLakeInputModel Location { get; set; }

        [Required]
        [Display(Name = "Select main image")]
        public IFormFile MainImage { get; set; }

        [Display(Name = "Select other images")]
        public IEnumerable<IFormFile> OtherImages { get; set; }
    }
}
