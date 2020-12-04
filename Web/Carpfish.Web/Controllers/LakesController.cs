namespace Carpfish.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Carpfish.Common;
    using Carpfish.Services.Data;
    using Carpfish.Web.ViewModels.Lakes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class LakesController : Controller
    {
        private readonly ICountriesService countriesService;
        private readonly ILakesService lakesService;

        public LakesController(
            ICountriesService countriesService,
            ILakesService lakesService)
        {
            this.countriesService = countriesService;
            this.lakesService = lakesService;
        }

        [Authorize]
        public IActionResult Add()
        {
            var viewModel = new AddLakeInputModel
            {
                CountriesItems = this.countriesService.GetAllAsKeyValuePairs(),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddLakeInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CountriesItems = this.countriesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                await this.lakesService.AddAsync(input, userId);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            // TODO: Redirect to lake info page
            return this.Redirect("/");
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var viewModel = new LakesListViewModel()
            {
                ItemsPerPage = GlobalConstants.LakesCountPerPage,
                ItemsCount = this.lakesService.GetCount(),
                PageNumber = id,
                Lakes = this.lakesService.GetAll<LakeInListViewModel>(id, GlobalConstants.LakesCountPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var viewModel = this.lakesService.GetById<LakeByIdViewModel>(id);

            return this.View(viewModel);
        }
    }
}
