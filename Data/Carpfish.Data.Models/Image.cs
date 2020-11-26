namespace Carpfish.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Carpfish.Data.Common.Models;

    public class Image : BaseModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string OwnerId { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        [Required]
        public string Url { get; set; }
    }
}
