namespace Carpfish.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Carpfish.Common;
    using Carpfish.Services.Data;
    using Carpfish.Web.ViewModels.Trophies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class TrophiesController : Controller
    {
        private readonly ILakesService lakesService;
        private readonly ITrophiesService trophiesService;
        private readonly IRigsService rigsService;

        public TrophiesController(
            ILakesService lakesService,
            ITrophiesService trophiesService,
            IRigsService rigsService)
        {
            this.lakesService = lakesService;
            this.trophiesService = trophiesService;
            this.rigsService = rigsService;
        }

        [Authorize]
        public IActionResult Add()
        {
            var viewModel = new AddTrophyInputModel
            {
                LakesItems = this.lakesService.GetAllAsKeyValuePairs(),
                RigsItems = this.rigsService.GetAllAsKeyValuePairs(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddTrophyInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.LakesItems = this.lakesService.GetAllAsKeyValuePairs();
                input.RigsItems = this.rigsService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                await this.trophiesService.AddAsync(input, userId);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            return this.RedirectToAction("All");
        }

        public IActionResult All(int page = 1)
        {
            if (page <= 0)
            {
                return this.NotFound();
            }

            var viewModel = new TrophiesListViewModel()
            {
                ItemsPerPage = GlobalConstants.TrophiesCountPerPage,
                ItemsCount = this.trophiesService.GetCount(),
                PageNumber = page,
                Trophies = this.trophiesService.GetAll<TrophyInListViewModel>(page, GlobalConstants.TrophiesCountPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var viewModel = this.trophiesService.GetById<TrophyByIdViewModel>(id);

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
            var trophyOwnerId = this.trophiesService.GetTrophyOwnerId(id);

            var isAdministrator = this.User.IsInRole(GlobalConstants.AdministratorRoleName);

            if (currentUserId != trophyOwnerId && !isAdministrator)
            {
                return this.NotFound();
            }

            var inputModel = this.trophiesService.GetById<EditTrophyInputModel>(id);
            inputModel.LakesItems = this.lakesService.GetAllAsKeyValuePairs();
            inputModel.RigsItems = this.rigsService.GetAllAsKeyValuePairs();

            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, EditTrophyInputModel input)
        {
            var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var trophyOwnerId = this.trophiesService.GetTrophyOwnerId(id);

            var isAdministrator = this.User.IsInRole(GlobalConstants.AdministratorRoleName);

            if (currentUserId != trophyOwnerId && !isAdministrator)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                input.LakesItems = this.lakesService.GetAllAsKeyValuePairs();
                input.RigsItems = this.rigsService.GetAllAsKeyValuePairs();

                return this.View(input);
            }

            await this.trophiesService.UpdateAsync(id, input);

            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var trophyOwnerId = this.trophiesService.GetTrophyOwnerId(id);

            var isAdministrator = this.User.IsInRole(GlobalConstants.AdministratorRoleName);

            if (currentUserId != trophyOwnerId && !isAdministrator)
            {
                return this.NotFound();
            }

            await this.trophiesService.DeleteAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
