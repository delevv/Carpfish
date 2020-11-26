namespace Carpfish.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Carpfish.Common;
    using Carpfish.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Items = new HashSet<Item>();
        }

        [Required]
        [MaxLength(GlobalConstants.CategoryNameMaxLength)]
        public string Name { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
