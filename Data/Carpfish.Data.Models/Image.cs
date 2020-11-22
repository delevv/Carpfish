namespace Carpfish.Data.Models
{
    using System;

    using Carpfish.Data.Common.Models;

    public class Image : BaseModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string OwnerId { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public string Extension { get; set; }

        public string Url { get; set; }
    }
}
