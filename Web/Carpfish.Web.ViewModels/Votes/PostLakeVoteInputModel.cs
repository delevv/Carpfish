namespace Carpfish.Web.ViewModels.Votes
{
    using System.ComponentModel.DataAnnotations;

    public class PostLakeVoteInputModel
    {
        public int LakeId { get; set; }

        [Range(1, 5)]
        public byte Value { get; set; }
    }
}
