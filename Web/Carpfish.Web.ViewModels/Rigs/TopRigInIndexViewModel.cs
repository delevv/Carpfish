namespace Carpfish.Web.ViewModels.Rigs
{
    using Carpfish.Data.Models;
    using Carpfish.Services.Mapping;

    public class TopRigInIndexViewModel : IMapFrom<Rig>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int TrophiesCount { get; set; }
    }
}
