namespace Carpfish.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using Carpfish.Web.ViewModels.Lakes;
    using Carpfish.Web.ViewModels.Rigs;
    using Carpfish.Web.ViewModels.Trophies;

    public class IndexViewModel
    {
        public IEnumerable<LakeInIndexMapViewModel> MapLakes { get; set; }

        public IEnumerable<TopRigInIndexViewModel> Rigs { get; set; }

        public IEnumerable<TopTrophiesInIndexViewModel> Trophies { get; set; }
    }
}
