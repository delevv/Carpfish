namespace Carpfish.Web.ViewModels.Rigs
{
    using System.Collections.Generic;

    public class RigsListViewModel : PagingViewModel
    {
        public IEnumerable<RigInListViewModel> Rigs { get; set; }
    }
}
