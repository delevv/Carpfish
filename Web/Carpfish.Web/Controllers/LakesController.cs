namespace Carpfish.Web.Controllers
{
    using System;
    using System.Collections.Generic;
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

        public IActionResult All(string type, int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var lakes = this.lakesService.GetAll<LakeInListViewModel>(id, GlobalConstants.LakesCountPerPage);
            var lakesCount = this.lakesService.GetCount();

            if (type != null)
            {
                id = 1;

                if (type == "All")
                {
                    lakes = this.lakesService.GetAll<LakeInListViewModel>(id, GlobalConstants.LakesCountPerPage);
                    lakesCount = this.lakesService.GetCount();
                }
                else
                {
                    lakes = this.lakesService.GetAll<LakeInListViewModel>(type, id, GlobalConstants.LakesCountPerPage);

                    if (type == "Free")
                    {
                        lakesCount = this.lakesService.GetFreeLakesCount();
                    }
                    else
                    {
                        lakesCount = this.lakesService.GetPaidLakesCount();
                    }
                }

            }

            var viewModel = new LakesListViewModel()
            {
                ItemsPerPage = GlobalConstants.LakesCountPerPage,
                ItemsCount = lakesCount,
                PageNumber = id,
                Lakes = lakes,
                currStatus = type,
                statusTypes = new string[] { "Free", "Paid" },
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
