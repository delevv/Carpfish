namespace Carpfish.Web.Controllers
{
    using System.Diagnostics;

    using Carpfish.Services.Data;
    using Carpfish.Web.ViewModels;
    using Carpfish.Web.ViewModels.Home;
    using Carpfish.Web.ViewModels.Lakes;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ILakesService lakesService;

        public HomeController(ILakesService lakesService)
        {
            this.lakesService = lakesService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                MapLakes = this.lakesService.GetAll<LakeInIndexMapViewModel>(),
            };
            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
