namespace Carpfish.Web.ViewModels.Trophies
{
    using System.Linq;

    using AutoMapper;
    using Carpfish.Data.Models;
    using Carpfish.Services.Mapping;

    public class TopTrophiesInIndexViewModel : IMapFrom<Trophy>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public double Weight { get; set; }

        public int LakeId { get; set; }

        public string LakeName { get; set; }

        public string Image { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Trophy, TopTrophiesInIndexViewModel>()
                .ForMember(x => x.Image, opt =>
                opt.MapFrom(t => t.TrophyImages
                                    .FirstOrDefault(ti => ti.IsMain)
                                    .Image.Url));
        }
    }
}
