namespace Carpfish.Web.Controllers
{
    using System.Diagnostics;

    using Carpfish.Services.Data;
    using Carpfish.Web.ViewModels;
    using Carpfish.Web.ViewModels.Home;
    using Carpfish.Web.ViewModels.Lakes;
    using Carpfish.Web.ViewModels.Rigs;
    using Carpfish.Web.ViewModels.Trophies;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ILakesService lakesService;
        private readonly IRigsService rigsService;
        private readonly ITrophiesService trophiesService;

        public HomeController(
            ILakesService lakesService,
            IRigsService rigsService,
            ITrophiesService trophiesService)
        {
            this.lakesService = lakesService;
            this.rigsService = rigsService;
            this.trophiesService = trophiesService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                MapLakes = this.lakesService
                .GetAll<LakeInIndexMapViewModel>(),
                Rigs = this.rigsService
                .GetFiveRigsWithMostTrophies<TopRigInIndexViewModel>(),
                Trophies = this.trophiesService
                .GetTopFiveBiggestTrophies<TopTrophiesInIndexViewModel>(),
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
