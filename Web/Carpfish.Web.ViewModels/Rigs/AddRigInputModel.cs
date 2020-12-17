namespace Carpfish.Web.ViewModels.Rigs
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Carpfish.Web.ViewModels.Materials;
    using Carpfish.Web.ViewModels.Steps;
    using Microsoft.AspNetCore.Http;

    public class AddRigInputModel : BaseRigInputModel
    {
        [Required]
        [Display(Name = "Select image")]
        public IFormFile Image { get; set; }

        public List<AddStepInputModel> Steps { get; set; }

        public List<AddMaterialInputModel> Materials { get; set; }
    }
}
