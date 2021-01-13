namespace Carpfish.Web.ViewModels.Lakes
{
    using System.Collections.Generic;

    public class LakesListViewModel : PagingViewModel
    {
        public IEnumerable<LakeInListViewModel> Lakes { get; set; }

        public IEnumerable<string> StatusTypes { get; set; }

        public string CurrStatus { get; set; }

        public IEnumerable<string> OrderTypes { get; set; }

        public string CurrOrder { get; set; }

        public string Search { get; set; }
    }
}
