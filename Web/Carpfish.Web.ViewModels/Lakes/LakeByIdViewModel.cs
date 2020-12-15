namespace Carpfish.Web.ViewModels.Lakes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using AutoMapper;
    using Carpfish.Data.Models;
    using Carpfish.Services.Mapping;
    using Carpfish.Web.ViewModels.Location;
    using Carpfish.Web.ViewModels.Trophies;

    public class LakeByIdViewModel : IMapFrom<Lake>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string OwnerUserName { get; set; }

        public double Area { get; set; }

        public string CountryName { get; set; }

        public string Coordinates { get; set; }

        public string WebsiteUrl { get; set; }

        public string IsFree { get; set; }

        public string MainImg { get; set; }

        public string[] OtherImages { get; set; }

        public IEnumerable<TrophyInListViewModel> Trophies { get; set; }

        public double AverageRating { get; set; }

        public int RatersCount { get; set; }

        public int OneStarRatersCount { get; set; }

        public int TwoStarRatersCount { get; set; }

        public int ThreeStarRatersCount { get; set; }

        public int FourStarRatersCount { get; set; }

        public int FiveStarRatersCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Lake, LakeByIdViewModel>()
               .ForMember(x => x.MainImg, opt =>
                 opt.MapFrom(l => l.LakeImages
                                     .FirstOrDefault(li => li.IsMain)
                                     .Image.Url))
               .ForMember(x => x.Coordinates, opt =>
                  opt.MapFrom(l => $"({l.Location.Latitude}, {l.Location.Longitude})"))
               .ForMember(x => x.IsFree, opt =>
                   opt.MapFrom(l => l.IsFree ? "Free" : "Paid"))
               .ForMember(x => x.OtherImages, opt =>
                 opt.MapFrom(l => l.LakeImages.Where(i => !i.IsMain).Select(li => li.Image.Url)))
               .ForMember(x => x.AverageRating, opt =>
                 opt.MapFrom(l => l.LakeVotes.Any() ? l.LakeVotes.Average(lv => lv.Vote.Value) : 0))
               .ForMember(x => x.RatersCount, opt =>
                 opt.MapFrom(l => l.LakeVotes.Count()))
               .ForMember(x => x.OneStarRatersCount, opt =>
                 opt.MapFrom(l => l.LakeVotes.Any() ? l.LakeVotes.Where(lv => lv.Vote.Value == 1).Count() : 0))
                  .ForMember(x => x.TwoStarRatersCount, opt =>
                 opt.MapFrom(l => l.LakeVotes.Any() ? l.LakeVotes.Where(lv => lv.Vote.Value == 2).Count() : 0))
                  .ForMember(x => x.ThreeStarRatersCount, opt =>
                 opt.MapFrom(l => l.LakeVotes.Any() ? l.LakeVotes.Where(lv => lv.Vote.Value == 3).Count() : 0))
                  .ForMember(x => x.FourStarRatersCount, opt =>
                 opt.MapFrom(l => l.LakeVotes.Any() ? l.LakeVotes.Where(lv => lv.Vote.Value == 4).Count() : 0))
                  .ForMember(x => x.FiveStarRatersCount, opt =>
                 opt.MapFrom(l => l.LakeVotes.Any() ? l.LakeVotes.Where(lv => lv.Vote.Value == 5).Count() : 0));
        }
    }
}
