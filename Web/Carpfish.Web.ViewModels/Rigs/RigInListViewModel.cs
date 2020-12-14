namespace Carpfish.Web.ViewModels.Rigs
{
    using Carpfish.Data.Models;
    using Carpfish.Services.Mapping;

    public class RigInListViewModel : IMapFrom<Rig>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int TrophiesCount { get; set; }

        public int StepsCount { get; set; }

        public int MaterialsCount { get; set; }
    }
}
