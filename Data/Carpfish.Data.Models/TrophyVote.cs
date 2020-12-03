namespace Carpfish.Data.Models
{
    using Carpfish.Data.Common.Models;

    public class TrophyVote : BaseModel<int>
    {
        public int TrophyId { get; set; }

        public virtual Trophy Trophy { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int VoteId { get; set; }

        public virtual Vote Vote { get; set; }
    }
}
