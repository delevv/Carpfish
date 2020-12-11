namespace Carpfish.Web.ViewModels.Materials
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Carpfish.Common;

    public class AddMaterialInputModel
    {
        [Required]
        [MaxLength(GlobalConstants.MaterialDescriptionMaxLength)]
        public string Description { get; set; }
    }
}
