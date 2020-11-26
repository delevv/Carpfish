namespace Carpfish.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Carpfish.Common;
    using Carpfish.Data.Common.Models;
    using Carpfish.Data.Models.Enumerations;

    public class Item : BaseModel<int>
    {
        public Item()
        {
            this.ItemImages = new HashSet<ItemImage>();
        }

        [Required]
        [MaxLength(GlobalConstants.ItemTitleMaxLength)]
        public string Title { get; set; }

        public virtual ICollection<ItemImage> ItemImages { get; set; }

        [Required]
        [MaxLength(GlobalConstants.ItemDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public ItemCondition Condition { get; set; }

        [Range(GlobalConstants.ItemMinPrice, GlobalConstants.ItemMaxPrice)]
        public decimal Price { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
