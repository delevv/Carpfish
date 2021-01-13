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

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult All(LakesAllInputModel input)
        {
            if (input.Page <= 0)
            {
                return this.NotFound();
            }

            input.ItemsPerPage = GlobalConstants.LakesCountPerPage;

            var lakes = this.lakesService.GetAll<LakeInListViewModel>(input);
            var totalLakesCount = this.lakesService.GetCount(input);

            var viewModel = new LakesListViewModel()
            {
                ItemsPerPage = GlobalConstants.LakesCountPerPage,
                ItemsCount = totalLakesCount,
                PageNumber = input.Page,
                Lakes = lakes,
                Search = input.Search,
                CurrStatus = input.Type ?? "Type",
                StatusTypes = new string[] { "Type", "Free", "Paid" },
                CurrOrder = input.Order ?? "Order By",
                OrderTypes = new string[] { "Order By", "Rating ASC", "Rating DESC", "Trophies Count ASC", "Trophies Count DESC" },
            };

            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var viewModel = this.lakesService.GetById<LakeByIdViewModel>(id);

            if (this.User.Identity.IsAuthenticated)
            {
                var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                viewModel.IsUserCreator = currentUserId == viewModel.OwnerId;
            }

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var lakeOwnerId = this.lakesService.GetLakeOwnerId(id);

            var isAdministrator = this.User.IsInRole(GlobalConstants.AdministratorRoleName);

            if (currentUserId != lakeOwnerId && !isAdministrator)
            {
                return this.NotFound();
            }

            var inputModel = this.lakesService.GetById<EditLakeInputModel>(id);
            inputModel.CountriesItems = this.countriesService.GetAllAsKeyValuePairs();

            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, EditLakeInputModel input)
        {
            var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var lakeOwnerId = this.lakesService.GetLakeOwnerId(id);

            var isAdministrator = this.User.IsInRole(GlobalConstants.AdministratorRoleName);

            if (currentUserId != lakeOwnerId && !isAdministrator)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                input.CountriesItems = this.countriesService.GetAllAsKeyValuePairs();

                return this.View(input);
            }

            await this.lakesService.UpdateAsync(id, input);

            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var lakeOwnerId = this.lakesService.GetLakeOwnerId(id);

            var isAdministrator = this.User.IsInRole(GlobalConstants.AdministratorRoleName);

            if (currentUserId != lakeOwnerId && !isAdministrator)
            {
                return this.NotFound();
            }

            await this.lakesService.DeleteAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
