namespace Carpfish.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using Carpfish.Web.ViewModels.Lakes;

    public class IndexViewModel
    {
        public IEnumerable<LakeInIndexMapViewModel> MapLakes { get; set; }
    }
}
