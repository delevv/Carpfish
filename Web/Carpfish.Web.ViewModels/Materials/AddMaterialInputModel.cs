namespace Carpfish.Web.ViewModels.Materials
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Carpfish.Common;

    public class AddMaterialInputModel
    {
        [Range(GlobalConstants.MaterialNumberMinValue, GlobalConstants.MaterialNumberMaxValue)]
        public int MaterialNumber { get; set; }

        [Required]
        [MaxLength(GlobalConstants.MaterialDescriptionMaxLength)]
        public string Description { get; set; }
    }
}
