namespace Carpfish.Web.ViewModels.Lakes
{
    using System.Collections.Generic;

    public class LakesListViewModel : PagingViewModel
    {
        public IEnumerable<LakeInListViewModel> Lakes { get; set; }

        public IEnumerable<string> statusTypes { get; set; }

        public string currStatus { get; set; }
    }
}
