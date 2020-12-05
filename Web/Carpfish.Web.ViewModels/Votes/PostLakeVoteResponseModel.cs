namespace Carpfish.Web.ViewModels.Votes
{
    public class PostLakeVoteResponseModel
    {
        public double AverageVote { get; set; }

        public int RatersCount { get; set; }

        public int OneStarRatersCount { get; set; }

        public int TwoStarRatersCount { get; set; }

        public int ThreeStarRatersCount { get; set; }

        public int FourStarRatersCount { get; set; }

        public int FiveStarRatersCount { get; set; }
    }
}
