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

        public TrophiesController(ILakesService lakesService, ITrophiesService trophiesService)
        {
            this.lakesService = lakesService;
            this.trophiesService = trophiesService;
        }

        [Authorize]
        public IActionResult Add()
        {
            var viewModel = new AddTrophyInputModel
            {
                LakesItems = this.lakesService.GetAllAsKeyValuePairs(),
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

            // TODO: Redirect to lake info page
            return this.Redirect("/");
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var viewModel = new TrophiesListViewModel()
            {
                ItemsPerPage = GlobalConstants.TrophiesCountPerPage,
                ItemsCount = this.lakesService.GetCount(),
                PageNumber = id,
                Trophies = this.trophiesService.GetAll<TrophyInListViewModel>(id, GlobalConstants.TrophiesCountPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var viewModel = this.trophiesService.GetById<TrophyByIdViewModel>(id);
            return this.View(viewModel);
        }
    }
}
