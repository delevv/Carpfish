namespace Carpfish.Web.ViewModels.Trophies
{
    using System.Collections.Generic;

    public class TrophiesListViewModel : PagingViewModel
    {
        public IEnumerable<TrophyInListViewModel> Trophies { get; set; }
    }
}
