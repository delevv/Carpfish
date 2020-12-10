namespace Carpfish.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Carpfish.Common;
    using Carpfish.Data.Common.Models;

    public class Rig : BaseDeletableModel<int>
    {
        public Rig()
        {
            this.Materials = new HashSet<Material>();
            this.Steps = new HashSet<Step>();
            this.Trophies = new HashSet<Trophy>();
        }

        [Required]
        [MaxLength(GlobalConstants.RigNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(GlobalConstants.RigDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        [Required]
        public Image Image { get; set; }

        public virtual ICollection<Material> Materials { get; set; }

        public virtual ICollection<Step> Steps { get; set; }

        public virtual ICollection<Trophy> Trophies { get; set; }
    }
}
