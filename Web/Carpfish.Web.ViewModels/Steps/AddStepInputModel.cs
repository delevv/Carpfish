namespace Carpfish.Web.ViewModels.Steps
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Carpfish.Common;
    using Microsoft.AspNetCore.Http;

    public class AddStepInputModel
    {
        [Range(GlobalConstants.StepNumberMinValue, GlobalConstants.StepNumberMaxValue)]
        public int StepNumber { get; set; }

        [Required]
        [Display(Name = "Select step image")]
        public IFormFile Image { get; set; }

        [Required]
        [MaxLength(GlobalConstants.StepDescriptionMaxLength)]
        public string Description { get; set; }
    }
}
