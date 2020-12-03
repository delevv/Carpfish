// ReSharper disable VirtualMemberCallInConstructor
namespace Carpfish.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Carpfish.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Items = new HashSet<Item>();
            this.Lakes = new HashSet<Lake>();
            this.Trophies = new HashSet<Trophy>();
            this.LakeVotes = new HashSet<LakeVote>();
            this.TrophyVotes = new HashSet<TrophyVote>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Item> Items { get; set; }

        public virtual ICollection<Lake> Lakes { get; set; }

        public virtual ICollection<Trophy> Trophies { get; set; }

        public virtual ICollection<LakeVote> LakeVotes { get; set; }

        public virtual ICollection<TrophyVote> TrophyVotes { get; set; }
    }
}
