namespace Carpfish.Web.Controllers
{
    using Carpfish.Services.Data;
    using Carpfish.Web.ViewModels.Lakes;
    using Microsoft.AspNetCore.Mvc;

    public class LakesController : Controller
    {
        private readonly ICountriesService countriesService;

        public LakesController(ICountriesService countriesService)
        {
            this.countriesService = countriesService;
        }

        public IActionResult Add()
        {
            var viewModel = new AddLakeInputModel();
            viewModel.CountriesItems = this.countriesService.GetAllAsKeyValuePairs();
            return this.View(viewModel);
        }
    }
}
