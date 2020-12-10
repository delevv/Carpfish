namespace Carpfish.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Carpfish.Common;
    using Carpfish.Data.Common.Models;

    public class Material : BaseModel<int>
    {
        [Range(GlobalConstants.MaterialNumberMinValue, GlobalConstants.MaterialNumberMaxValue)]
        public int MaterialNumber { get; set; }

        [Required]
        [MaxLength(GlobalConstants.MaterialDescriptionMaxLength)]
        public string Description { get; set; }

        [Range(GlobalConstants.MaterialRigIdMinValue, GlobalConstants.MaterialRigIdMaxValue)]
        public int RigId { get; set; }

        public virtual Rig Rig { get; set; }
    }
}
