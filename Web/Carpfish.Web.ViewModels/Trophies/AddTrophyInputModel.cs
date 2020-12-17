namespace Carpfish.Web.ViewModels.Trophies
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class AddTrophyInputModel : BaseTrophyInputModel
    {
        [Required]
        [Display(Name = "Select main image")]
        public IFormFile MainImage { get; set; }

        [Display(Name = "Select other images")]
        public IEnumerable<IFormFile> OtherImages { get; set; }
    }
}
