namespace Carpfish.Web.ViewModels.Rigs
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Carpfish.Common;
    using Carpfish.Web.ViewModels.Materials;
    using Carpfish.Web.ViewModels.Steps;
    using Microsoft.AspNetCore.Http;

    public class AddRigInputModel
    {
        [Required]
        [MaxLength(GlobalConstants.RigNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(GlobalConstants.RigDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Select image")]
        public IFormFile Image { get; set; }

        public IEnumerable<AddStepInputModel> Steps { get; set; }

        public ICollection<AddMaterialInputModel> Materials { get; set; }
    }
}
