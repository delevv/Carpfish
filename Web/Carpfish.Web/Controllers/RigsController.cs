namespace Carpfish.Web.Controllers
{
    using Carpfish.Web.ViewModels.Rigs;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class RigsController : Controller
    {
        [Authorize]
        public IActionResult Add()
        {
            var viewModel = new AddRigInputModel();
            return this.View(viewModel);
        }
    }
}
