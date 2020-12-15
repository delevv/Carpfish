namespace Carpfish.Web.ViewModels.Trophies
{
    using System.Linq;

    using AutoMapper;
    using Carpfish.Data.Models;
    using Carpfish.Services.Mapping;

    public class TrophyInRigViewModel : IMapFrom<Trophy>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public double Weight { get; set; }

        public string MainImage { get; set; }

        public int LakeId { get; set; }

        public string LakeName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Trophy, TrophyInRigViewModel>()
                .ForMember(x => x.MainImage, opt =>
                  opt.MapFrom(t => t.TrophyImages.Where(ti => ti.IsMain).FirstOrDefault().Image.Url));
        }
    }
}
