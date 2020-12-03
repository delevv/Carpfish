namespace Carpfish.Web.ViewModels.Votes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PostTrophyVoteInputModel
    {
        public int TrophyId { get; set; }

        [Range(1, 5)]
        public byte Value { get; set; }
    }
}
