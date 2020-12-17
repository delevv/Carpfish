namespace Carpfish.Web.ViewModels.Rigs
{
    using System.ComponentModel.DataAnnotations;

    using Carpfish.Common;

    public abstract class BaseRigInputModel
    {
        [Required]
        [MaxLength(GlobalConstants.RigNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(GlobalConstants.RigDescriptionMaxLength)]
        public string Description { get; set; }
    }
}
