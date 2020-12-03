namespace Carpfish.Web.ViewModels.Lakes
{
    public class LakeRatingViewModel
    {
        public double AverageValue { get; set; }

        public int RatersCount { get; set; }

        public int OneStarRatersCount { get; set; }

        public int OneStarBar => (this.OneStarRatersCount + 1) / (this.RatersCount + 1);

        public int TwoStarRatersCount { get; set; }

        public int TwoStarBar => (this.TwoStarRatersCount + 1) / (this.RatersCount + 1);

        public int ThreeStarRatersCount { get; set; }

        public int ThreeStarBar => (this.ThreeStarRatersCount + 1) / (this.RatersCount + 1);

        public int FourStarRatersCount { get; set; }

        public int FourStarBar => (this.FourStarRatersCount + 1) / (this.RatersCount + 1);

        public int FiveStarRatersCount { get; set; }

        public int FiveStarBar => (this.FiveStarRatersCount + 1) / (this.RatersCount + 1);
    }
}
