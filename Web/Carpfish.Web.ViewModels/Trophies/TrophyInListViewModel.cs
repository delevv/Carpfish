namespace Carpfish.Web.ViewModels.Trophies
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using AutoMapper;
    using Carpfish.Data.Models;
    using Carpfish.Services.Mapping;

    public class TrophyInListViewModel : IMapFrom<Trophy>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public double Weight { get; set; }

        public string OwnerId { get; set; }

        public string OwnerUserName { get; set; }

        public int LakeId { get; set; }

        public string LakeName { get; set; }

        public string MainImage { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Trophy, TrophyInListViewModel>()
                .ForMember(x => x.MainImage, opt =>
                opt.MapFrom(t => t.TrophyImages
                                    .FirstOrDefault(ti => ti.IsMain)
                                    .Image.Url));
        }
    }
}
