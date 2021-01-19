namespace Carpfish.Web.ViewModels.Rigs
{
    using System.Collections.Generic;

    public class RigsListViewModel : PagingViewModel
    {
        public IEnumerable<RigInListViewModel> Rigs { get; set; }

        public IEnumerable<string> OrderTypes { get; set; }

        public string CurrOrder { get; set; }

        public string Search { get; set; }
    }
}
