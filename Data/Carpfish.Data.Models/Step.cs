namespace Carpfish.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Carpfish.Common;
    using Carpfish.Data.Common.Models;

    public class Step : BaseModel<int>
    {
        [Range(GlobalConstants.StepNumberMinValue, GlobalConstants.StepNumberMaxValue)]
        public int StepNumber { get; set; }

        [Required]
        public string ImgUrl { get; set; }

        [Required]
        [MaxLength(GlobalConstants.StepDescriptionMaxLength)]
        public string Description { get; set; }

        [Range(GlobalConstants.StepRigIdMinValue, GlobalConstants.StepRigIdMaxValue)]
        public int RigId { get; set; }

        public virtual Rig Rig { get; set; }
    }
}
