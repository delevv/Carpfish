namespace Carpfish.Data.Models
{
    using System.Collections.Generic;

    using Carpfish.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Items = new HashSet<Item>();
        }

        public string Name { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
