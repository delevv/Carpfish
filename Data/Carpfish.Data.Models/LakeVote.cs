namespace Carpfish.Data.Models
{
    using Carpfish.Data.Common.Models;

    public class LakeVote : BaseModel<int>
    {
        public int LakeId { get; set; }

        public virtual Lake Lake { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int VoteId { get; set; }

        public virtual Vote Vote { get; set; }
    }
}
