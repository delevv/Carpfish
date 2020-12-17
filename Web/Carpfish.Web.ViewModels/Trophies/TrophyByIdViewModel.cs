namespace Carpfish.Web.ViewModels.Trophies
{
    using System.Linq;

    using AutoMapper;
    using Carpfish.Data.Models;
    using Carpfish.Services.Mapping;

    public class TrophyByIdViewModel : IMapFrom<Trophy>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public double Weight { get; set; }

        public string BaitDescription { get; set; }

        public string OwnerId { get; set; }

        [IgnoreMap]
        public bool IsUserCreator { get; set; }

        public string OwnerUserName { get; set; }

        public int LakeId { get; set; }

        public int? RigId { get; set; }

        public string LakeName { get; set; }

        public string RigName { get; set; }

        public string MainImage { get; set; }

        public string[] OtherImages { get; set; }

        public double AverageRating { get; set; }

        public int RatersCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Trophy, TrophyByIdViewModel>()
                .ForMember(x => x.AverageRating, opt =>
                    opt.MapFrom(t => t.TrophyVotes.Any() ? t.TrophyVotes.Average(tv => tv.Vote.Value) : 0))
                .ForMember(x => x.RatersCount, opt =>
                    opt.MapFrom(t => t.TrophyVotes.Count()))
                .ForMember(x => x.MainImage, opt =>
                    opt.MapFrom(t => t.TrophyImages.FirstOrDefault(ti => ti.IsMain).Image.Url))
                 .ForMember(x => x.OtherImages, opt =>
                    opt.MapFrom(t => t.TrophyImages.Where(i => !i.IsMain).Select(ti => ti.Image.Url)));
        }
    }
}
