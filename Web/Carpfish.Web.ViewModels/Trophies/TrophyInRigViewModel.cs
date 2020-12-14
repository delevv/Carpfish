namespace Carpfish.Web.ViewModels.Trophies
{
    using Carpfish.Data.Models;
    using Carpfish.Services.Mapping;

    public class TrophyInRigViewModel : IMapFrom<Trophy>
    {
        public int Id { get; set; }

        public double Weight { get; set; }

        public string MainImage { get; set; }

        public string LakeName { get; set; }
    }
}
