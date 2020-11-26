namespace Carpfish.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Carpfish.Data.Common.Models;

    public class Country : BaseModel<int>
    {
        public Country()
        {
            this.Lakes = new HashSet<Lake>();
        }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Lake> Lakes { get; set; }
    }
}
