namespace Carpfish.Data.Models
{
    using System.Collections.Generic;

    using Carpfish.Data.Common.Models;
    using Carpfish.Data.Models.Enumerations;

    public class Item : BaseModel<int>
    {
        public Item()
        {
            this.ItemImages = new HashSet<ItemImage>();
        }

        public string Title { get; set; }

        public virtual ICollection<ItemImage> ItemImages { get; set; }

        public string Description { get; set; }

        public ItemCondition Condition { get; set; }

        public decimal Price { get; set; }

        public string OwnerId { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
