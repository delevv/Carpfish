namespace Carpfish.Web.ViewModels.Votes
{
    public class PostLakeVoteResponseModel
    {
        public double AverageVote { get; set; }

        public int RatersCount { get; set; }

        public int OneStarReatersCount { get; set; }

        public int TwoStarReatersCount { get; set; }

        public int ThreeStarReatersCount { get; set; }

        public int FourStarReatersCount { get; set; }

        public int FiveStarReatersCount { get; set; }
    }
}
