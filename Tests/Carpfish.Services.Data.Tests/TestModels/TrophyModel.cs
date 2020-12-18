namespace Carpfish.Services.Data.Tests.TestModels
{
    using Carpfish.Data.Models;
    using Carpfish.Services.Mapping;

    public class TrophyModel : IMapFrom<Trophy>
    {
        public int Id { get; set; }

        public double Weight { get; set; }

        public string BaitDescription { get; set; }

        public int LakeId { get; set; }

        public int? RigId { get; set; }
    }
}
